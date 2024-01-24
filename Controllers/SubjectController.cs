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
using WebApplication1.Services.SubjectService;
using WebApplication1.Data;
using WebApplication1.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService subjectService;
        private readonly ApplicationDBContext _context;

        public SubjectController(ISubjectService subjectService, ApplicationDBContext context = null)
        {
            this.subjectService = subjectService;
            _context = context;
        }

        [HttpPost("save")]
        public BaseResponse CreateSubject(CreateSubjectRequest request)
        {
            return subjectService.CreateSubject(request);
        }

        [HttpGet("list")]

        public BaseResponse SubjectList()
        {
            return subjectService.SubjectList();
        }

        [HttpGet("student")]

        public BaseResponse GetStudentById()
        {
            return subjectService.SubjectList();
        }

        [HttpPut("{id}")]
        public BaseResponse UpdateSubjectById(long id, UpdateSubjectRequest request)
        {
            return subjectService.UpdateSubjectById(id, request);
        }

        [HttpDelete("{id}")]
        public BaseResponse DeleteStudentById(long id)
        {
            return subjectService.DeleteSubjectById(id);
        }

        [HttpGet("{id}/subjects")]
        public IEnumerable<StudentDTO> GetStudentSubjects(long id)
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

                    var studentDTO = new StudentDTO
                    {
                        first_name = student.first_name,
                        last_name = student.last_name,
                        address = student.address,
                        email = student.email,
                        contact_number = student.contact_number,
                        
                    };

                    return new List<StudentDTO> { studentDTO };
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

    }
}
