using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs.Responses;
using WebApplication1.DTOs.Requests;
using WebApplication1.Services.StudentService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Services.SubjectService;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models;

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


        /* [HttpPost("save")]

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


        [HttpGet("search/{id}")]
        public async Task<IActionResult> SearchProducts(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Search term is required and must be a positive integer.");
                }

                var filteredStudent = await _context.Students
                    .FirstOrDefaultAsync(s => EF.Functions.Like(s.Id.ToString(), $"%{id}%"));

                if (filteredStudent != null)
                {
                    return Ok(filteredStudent);
                }
                else
                {
                    return NotFound($"No student found for the given search term: {id}");
                }
            }
            catch (Exception ex)
            {
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


