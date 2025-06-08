using AIUBStudentPortal.BLL.DTO;
using AIUBStudentPortal.BLL.Services;
using AIUBStudentPortal.DAL;
using AIUBStudentPortal.DAL.DataContext;
using AIUBStudentPortal.DAL.Entities;
using AIUBStudentPortal.DAL.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using AIUBStudentPortal.BLL.ViewModels;

namespace AIUBStudentPortal.Controllers
{
    [Authorize]
    public class StudentDashboardController : Controller
    {
        private readonly CourseService _courseService;
        private readonly RegistrationService _registrationService;
        private readonly DropApplicationService _dropApplicationService;
        private readonly StudentRepository _studentRepo;
        private readonly StudentService _studentService;

        // Default constructor: initialize all services including _studentService
        public StudentDashboardController()
        {
            var context = new StudentPortalDbContext();
            var courseRepo = new CourseRepository(context);
            var registrationRepo = new StudentCourseRegistrationRepository(context);
            var dropAppRepo = new DropApplicationRepository(context);
            var studentRepo = new StudentRepository(context);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseDTO>();
                cfg.CreateMap<StudentCourseRegistration, CourseDTO>();
                cfg.CreateMap<DropApplication, DropApplicationDTO>();
                cfg.CreateMap<DropApplicationDTO, DropApplication>();

                // Add mappings for Student to StudentGeneralInfoDTO and vice versa
                cfg.CreateMap<Student, StudentGeneralInfoDTO>();
                cfg.CreateMap<StudentGeneralInfoDTO, Student>();
            });

            var mapper = config.CreateMapper();

            _courseService = new CourseService(courseRepo, mapper);
            _registrationService = new RegistrationService(registrationRepo, courseRepo, mapper);
            _dropApplicationService = new DropApplicationService(dropAppRepo, mapper);
            _studentRepo = studentRepo;

            _studentService = new StudentService(studentRepo);
        }

        // Constructor with DI parameters including StudentService
        public StudentDashboardController(
            CourseService courseService,
            RegistrationService registrationService,
            DropApplicationService dropApplicationService,
            StudentService studentService
        )
        {
            _courseService = courseService;
            _registrationService = registrationService;
            _dropApplicationService = dropApplicationService;
            _studentService = studentService;
        }


        public ActionResult Index()
        {
            string studentIdStr = Session["StudentID"]?.ToString();
            if (string.IsNullOrEmpty(studentIdStr))
                return RedirectToAction("Login", "Account");

            int studentDbId = GetLoggedInStudentDbId();

            ViewBag.StudentFullName = Session["StudentFullName"]?.ToString();

            bool hasRegistered = _registrationService.HasRegistered(studentDbId);
            ViewBag.HasRegistered = hasRegistered;

            List<DailyClassDTO> classSchedule = new List<DailyClassDTO>();

            if (hasRegistered)
            {
                classSchedule = _registrationService.GetClassScheduleByDate(studentDbId);
            }

            return View("~/Views/AIUBportal/Dashboard.cshtml", classSchedule);
        }

        [HttpGet]
        public ActionResult CourseRegistration()
        {
            int studentId = GetLoggedInStudentDbId();

            if (_registrationService.HasRegistered(studentId))
            {
                return RedirectToAction("Index");
            }

            var availableCourses = _courseService.GetAllCourses();
            return View("~/Views/AIUBportal/CourseRegistration.cshtml", availableCourses);
        }

        [HttpPost]
        public ActionResult ConfirmRegistration(List<int> selectedCourseIds)
        {
            if (selectedCourseIds.Count != 5)
            {
                ModelState.AddModelError("", "You must select exactly 5 courses.");
                var courses = _courseService.GetAllCourses();
                return View("~/Views/AIUBportal/CourseRegistration.cshtml", courses);
            }

            int studentId = GetLoggedInStudentDbId();

            var registrationDto = new StudentCourseRegistrationDTO
            {
                StudentId = studentId,
                SelectedCourseIds = selectedCourseIds
            };

            _registrationService.RegisterCourses(registrationDto);

            return RedirectToAction("Index");
        }

        public ActionResult RegisteredCourses()
        {
            int studentId = GetLoggedInStudentDbId();
            var registeredCourses = _registrationService.GetRegisteredCourses(studentId);
            return View("~/Views/AIUBportal/RegisteredCourses.cshtml", registeredCourses);
        }

        // GET Drop Application Page
        public ActionResult DropApplication()
        {
            int studentId = GetLoggedInStudentDbId();

            var registeredCourses = _registrationService.GetRegisteredCourses(studentId);
            var dropRequests = _dropApplicationService.GetDropRequestsByStudent(studentId);

            var model = new DropApplicationViewModel
            {
                RegisteredCourses = registeredCourses,
                DropRequests = dropRequests
            };

            return View("~/Views/AIUBportal/DropApplication.cshtml", model);
        }

        // POST Submit Drop Request
        [HttpPost]
        public ActionResult SubmitDropRequest(int courseId)
        {
            int studentId = GetLoggedInStudentDbId();

            var dropDto = new DropApplicationDTO
            {
                StudentId = studentId,
                CourseId = courseId,
                RequestDate = System.DateTime.Now,
                Status = "Pending"
            };

            _dropApplicationService.SubmitDropRequest(dropDto);

            TempData["Message"] = "Drop request submitted successfully!";
            return RedirectToAction("DropApplication");
        }

        private int GetLoggedInStudentDbId()
        {
            return (int)Session["StudentDbId"];
        }
        // GET Edit General Info
        public ActionResult EditGeneralInfo()
        {
            string studentId = Session["StudentID"]?.ToString();
            var studentDto = _studentService.GetStudentGeneralInfo(studentId);
            return View("~/Views/AIUBportal/EditGeneralInfo.cshtml", studentDto);
        }

        // POST Edit General Info
        [HttpPost]
        public ActionResult EditGeneralInfo(StudentGeneralInfoDTO model)
        {
            if (ModelState.IsValid)
            {
                _studentService.UpdateStudentGeneralInfo(model);
                TempData["Success"] = "Information updated successfully!";
                ViewBag.Success = "Information updated successfully!";
                return View("~/Views/AIUBportal/EditGeneralInfo.cshtml", model);

                // or "Dashboard" if you have that action
            }
            return View("~/Views/AIUBportal/EditGeneralInfo.cshtml", model);
        }

    }
}
