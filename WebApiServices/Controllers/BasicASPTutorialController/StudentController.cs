using Microsoft.AspNetCore.Mvc;
using SharedService.DBContext;
using SharedService.Models.Student;

namespace WebApiServices.Controllers.BasicASPTutorialController
{
    public class StudentController : Controller
    {
        private readonly DatasContext _datasContext;

        public StudentController(DatasContext datasContext)
        {
            _datasContext = datasContext;
        }

        public IActionResult Index()
        {
            IEnumerable<StudentModel> allStudent = _datasContext.Students;
            return View(allStudent);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentModel obj)
        {
            if (ModelState.IsValid)
            {
                _datasContext.Students.Add(obj);
                _datasContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id ==0)
                return NotFound();

            var student = _datasContext.Students.Find(id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StudentModel obj)
        {
            if (ModelState.IsValid)
            {
                _datasContext.Students.Update(obj);
                _datasContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var student = _datasContext.Students.Find(id);
            if (student == null)
                return NotFound();

            _datasContext.Students.Remove(student);
            _datasContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
