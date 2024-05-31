using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Services;
using System.Collections.Generic;

namespace SchoolManagementSystem.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly IStudentService _studentService;

        public IndexModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IEnumerable<Student> Students { get; private set; }

        public void OnGet()
        {
            Students = _studentService.GetAllStudents();
        }
    }
}