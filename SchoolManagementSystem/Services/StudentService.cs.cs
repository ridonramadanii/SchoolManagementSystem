using System.Collections.Generic;
using System.Linq;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly List<Student> _students = new List<Student>
        {
            new Student { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
            new Student { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
        };

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

        public Student GetStudentById(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public void AddStudent(Student student)
        {
            student.Id = _students.Max(s => s.Id) + 1;
            _students.Add(student);
        }

        public void UpdateStudent(Student student)
        {
            var existingStudent = GetStudentById(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
            }
        }

        public void DeleteStudent(int id)
        {
            var student = GetStudentById(id);
            if (student != null)
            {
                _students.Remove(student);
            }
        }
    }
}