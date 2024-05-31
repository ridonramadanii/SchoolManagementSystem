using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Services;

namespace SchoolManagementSystem.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly IStudentService _studentService;

        public DetailsModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public Student Student { get; private set; }

        public void OnGet(int id)
        {
            Student = _studentService.GetStudentById(id);
        }
    }
}