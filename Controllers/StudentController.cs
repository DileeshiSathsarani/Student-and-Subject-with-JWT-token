using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs.Responses;
using WebApplication1.DTOs.Requests;
using WebApplication1.Services.StudentService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Services.SubjectService;
using Microsoft.AspNetCore.Authorization;
using Middleware.DTOs.Requests;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly ApplicationDBContext _context;

        public StudentController(IStudentService studentService, ApplicationDBContext context = null)
        {
            this.studentService = studentService;
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterUserRequest request)
        {
            BaseResponse response = studentService.RegisterUser(request);

            return StatusCode(response.status_code, response);
        }

        [HttpPost("login")]
        public IActionResult AuthenticateUser(AuthenticateRequest request)
        {
            BaseResponse response = studentService.AuthenticateUser(request);

            return StatusCode(response.status_code, response);
        }


      /*  [HttpPost("save")]

        public BaseResponse CreateStudent(CreateStudentRequest request)
        {
            return studentService.CreateStudent(request);
        }*/


        [HttpGet("id")]

        public BaseResponse GetStudentById()
        {
            return studentService.StudentList();
        }

        [HttpPut("{id}")]
        public BaseResponse UpdateStudentById(long id, UpdateStudentRequest request)
        {
            return studentService.UpdateStudentById(id, request);
        }

        [HttpDelete("{id}")]
        public BaseResponse DeleteStudentById(long id)
        {
            return studentService.DeleteStudentById(id);
        }


        // GET: api/product/search?productName=yourSearchTerm
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string studentName)
        {
            try
            {
                if (string.IsNullOrEmpty(studentName))
                {
                    return BadRequest("Search term is required.");
                }

                var filteredProducts = await _context.Students
                    .Where(s => EF.Functions.Like(s.first_name, $"%{studentName}%"))
                    .ToListAsync();

                if (filteredProducts.Any())
                {
                    return Ok(filteredProducts);
                }
                else
                {
                    return NotFound("No products found for the given search term.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}/subjects")]
        public IEnumerable<SubjectDTO> GetStudentSubjects(long id)
        {
           
            try
            {
                var student = _context.Students
                    .Include(s => s.Subjects)
                    .FirstOrDefault(s => s.Id == id);

                if (student != null)
                {
                    var subjects = student.Subjects.Select(sub => new SubjectDTO
                    {
                        subject_code = sub.subject_code,
                        subject_name = sub.subject_name
                    }).ToList();

                    return subjects;
                }
                else
                {
                    throw new InvalidOperationException("Student not found");
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("list")]
        public BaseResponse StudentList([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return studentService.StudentList(page, pageSize);
        }

    }
}


