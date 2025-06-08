using AIUBStudentPortal.BLL.DTO;
using AIUBStudentPortal.BLL.Services;
using AIUBStudentPortal.DAL.DataContext;
using AIUBStudentPortal.DAL.Entities;
using AIUBStudentPortal.DAL.Repositories;
using AutoMapper;
using System.Web.Mvc;

namespace AIUBStudentPortal.Controllers
{
    public class AdminController : Controller
    {
        private readonly CourseService _courseService;
        private readonly DropApplicationService _dropService;

        public AdminController()
        {
            // AutoMapper config
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseDTO>();
                cfg.CreateMap<CourseDTO, Course>();
                cfg.CreateMap<DropApplication, DropApplicationDTO>()
                    .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name))
                    .ForMember(dest => dest.CourseSection, opt => opt.MapFrom(src => src.Course.Section));
                cfg.CreateMap<DropApplicationDTO, DropApplication>();
            });

            var mapper = config.CreateMapper();

            var dbContext = new StudentPortalDbContext();

            var courseRepo = new CourseRepository(dbContext);
            _courseService = new CourseService(courseRepo, mapper);

            var dropRepo = new DropApplicationRepository(dbContext);
            _dropService = new DropApplicationService(dropRepo, mapper);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string adminId, string password)
        {
            if (adminId == "12" && password == "123")
            {
                Session["AdminLoggedIn"] = true;
                return RedirectToAction("Dashboard");
            }
            ViewBag.Error = "Invalid credentials!";
            return View();
        }

        public ActionResult Dashboard()
        {
            if (Session["AdminLoggedIn"] == null)
                return RedirectToAction("Login");

            var offeredCourses = _courseService.GetAllCourses(); // returns List<CourseDTO>
            return View(offeredCourses);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult CourseInsert()
        {
            if (Session["AdminLoggedIn"] == null)
                return RedirectToAction("Login");

            return View(new CourseDTO());
        }

        [HttpPost]
        public ActionResult CourseInsert(CourseDTO model)
        {
            if (ModelState.IsValid)
            {
                _courseService.InsertCourse(model);
                ViewBag.Message = "Course inserted successfully!";
                ModelState.Clear(); // Reset form
                return View(new CourseDTO());
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteCourse(int id)
        {
            if (Session["AdminLoggedIn"] == null)
                return RedirectToAction("Login");

            _courseService.DeleteCourse(id);
            return RedirectToAction("Dashboard");
        }

        public ActionResult RegisteredStudents()
        {
            if (Session["AdminLoggedIn"] == null)
                return RedirectToAction("Login");

            var studentRepo = new StudentRepository();
            var students = studentRepo.GetAll();

            return View(students);
        }

        public ActionResult RegisteredCourses(int studentId)
        {
            if (Session["AdminLoggedIn"] == null)
                return RedirectToAction("Login");

            var context = new StudentPortalDbContext();
            var registrationRepo = new StudentCourseRegistrationRepository(context);
            var courseRepo = new CourseRepository(context);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseDTO>();
            });

            var mapper = config.CreateMapper();
            var registrationService = new RegistrationService(registrationRepo, courseRepo, mapper);

            var registeredCourses = registrationService.GetRegisteredCourses(studentId);

            return View(registeredCourses);
        }

        // ✅ Drop Requests View (GET)
        public ActionResult DropRequests()
        {
            if (Session["AdminLoggedIn"] == null)
                return RedirectToAction("Login");

            var requests = _dropService.GetAllDropRequests();
            return View(requests);
        }

        // ✅ Drop Request Status Update (POST)
        [HttpPost]
        public ActionResult UpdateDropStatus(int id, string status)
        {
            if (Session["AdminLoggedIn"] == null)
                return RedirectToAction("Login");

            var dropApp = _dropService.GetDropRequestById(id);
            if (dropApp != null && dropApp.Status == "Pending")
            {
                dropApp.Status = status;
                _dropService.UpdateDropRequest(dropApp);
            }

            return RedirectToAction("DropRequests");
        }







    }
}
