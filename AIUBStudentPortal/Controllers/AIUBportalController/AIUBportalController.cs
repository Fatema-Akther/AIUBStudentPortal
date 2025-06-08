using AIUBStudentPortal.BLL.DTO;
using AIUBStudentPortal.BLL.DTOs;
using AIUBStudentPortal.BLL.Services;
using AIUBStudentPortal.DAL.DataContext;
using AIUBStudentPortal.DAL.Entities;
using AIUBStudentPortal.DAL.Repositories;
using AutoMapper;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;



namespace AIUBStudentPortal.UI.Controllers
{
    public class AIUBportalController : Controller
    {
        private readonly StudentService _studentService;
        private readonly RegistrationService _registrationService;

        public AIUBportalController()
        {
            // StudentService initialization
            var studentRepository = new StudentRepository();
            _studentService = new StudentService(studentRepository);

            // RegistrationService initialization
            var context = new StudentPortalDbContext();
            var registrationRepo = new StudentCourseRegistrationRepository(context);
            var courseRepo = new CourseRepository(context);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseDTO>();
                cfg.CreateMap<StudentCourseRegistration, CourseDTO>();
            });
            var mapper = config.CreateMapper();

            _registrationService = new RegistrationService(registrationRepo, courseRepo, mapper);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(StudentDTO student)
        {
            if (ModelState.IsValid)
            {
                student.RegistrationDate = DateTime.Now;

                // Hash password
                student.Password = Convert.ToBase64String(
                    SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(student.Password))
                );

                _studentService.RegisterStudent(student);
                ViewBag.Message = "Registration Successful";
                return RedirectToAction("Login", "AIUBportal");
            }

            return View(student);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(StudentDTO student)
        {
            var students = _studentService.GetAllStudents();

            // Hash input password
            string hashedInput = Convert.ToBase64String(
                SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(student.Password))
            );

            var studentFromDb = students.FirstOrDefault(s => s.StudentID == student.StudentID);
            if (studentFromDb == null || studentFromDb.Password != hashedInput)
            {
                ViewBag.Error = "Invalid Student ID or Password.";
                return View(student);
            }

            Session["StudentDbId"] = studentFromDb.Id;
            Session["StudentID"] = studentFromDb.StudentID;
            Session["StudentFullName"] = studentFromDb.FullName; // ✅ Add this line


            System.Web.Security.FormsAuthentication.SetAuthCookie(studentFromDb.StudentID, false);

            //return RedirectToAction("Dashboard", "AIUBportal");
            return RedirectToAction("Index", "StudentDashboard");
        }
        /*
                public ActionResult Dashboard()
                {
                    if (Session["StudentDbId"] == null)
                        return RedirectToAction("Login", "AIUBportal");

                    int studentId = Convert.ToInt32(Session["StudentDbId"]);

                    var context = new StudentPortalDbContext();
                    var registrationRepo = new StudentCourseRegistrationRepository(context);
                    var courseRepo = new CourseRepository(context);

                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Course, CourseDTO>();
                        cfg.CreateMap<StudentCourseRegistration, CourseDTO>();
                    });

                    var mapper = config.CreateMapper();
                    var registrationService = new RegistrationService(registrationRepo, courseRepo, mapper);

                    bool hasRegistered = registrationService.HasRegistered(studentId);

                    ViewBag.HasRegistered = hasRegistered;

                    //return View();
                    //class schedule

                    var classSchedule = registrationService.GetClassScheduleByDate(studentId);
                    return View(classSchedule);



                }*/



        public ActionResult Dashboard()
        {
            if (Session["StudentDbId"] == null)
                return RedirectToAction("Login", "AIUBportal");

            int studentId = Convert.ToInt32(Session["StudentDbId"]);

            bool hasRegistered = _registrationService.HasRegistered(studentId);
            ViewBag.HasRegistered = hasRegistered;

            var classSchedule = _registrationService.GetClassScheduleByDate(studentId);

            return View(classSchedule);
        }


    }
}
