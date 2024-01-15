using Microsoft.AspNetCore.Http;
using Middleware.DTOs.Requests;
using Middleware.Helpers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responses;
using WebApplication1.Models;

namespace WebApplication1.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDBContext context;

        public StudentService(ApplicationDBContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        public BaseResponse CreateStudent(CreateStudentRequest request)
        {
            BaseResponse response;

            try
            {
                StudentModel newStudent = new StudentModel();
                newStudent.first_name = request.first_name;
                newStudent.last_name = request.last_name;
                newStudent.address = request.address;
                newStudent.email = request.email;
                newStudent.contact_number = request.contact_number;

                using (context)
                {
                    context.Add(newStudent);
                    context.SaveChanges();
                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new student" }
                };

                return response;

            }

            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error: " + ex.Message }
                };
                return response;
            }

        }

        public BaseResponse StudentList(int page, int pageSize)
        {
            BaseResponse response;

            try
            {
                List<StudentDTO> students = new List<StudentDTO>();

                using (context)
                {
                    var query = context.Students.AsQueryable();

                    int totalItems = query.Count();
                    int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                    var pagedResults = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    pagedResults.ForEach(student => students.Add(new StudentDTO
                    {
                        first_name = student.first_name,
                        last_name = student.last_name,
                        address = student.address,
                        email = student.email,
                        contact_number = student.contact_number
                    }));

                    var paginationInfo = new
                    {
                        TotalItems = totalItems,
                        TotalPages = totalPages,
                        CurrentPage = page,
                        PageSize = pageSize
                    };

                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { Students = students, PaginationInfo = paginationInfo }
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error: " + ex.Message }
                };

                return response;
            }
        }

        public BaseResponse GetStudentById(long id)
        {
            BaseResponse response;

            try
            {
                StudentDTO student = new StudentDTO();

                using (context)
                {
                    StudentModel filteredStudent = context.Students.Where(s => s.Id == id).FirstOrDefault();

                    if (filteredStudent != null)
                    {
                        student.first_name = filteredStudent.first_name;
                        student.last_name = filteredStudent.last_name;
                        student.address = filteredStudent.address;
                        student.email = filteredStudent.email;
                        student.contact_number = filteredStudent.contact_number;
                    }
                    else
                    {
                        student = null;
                    }
                }

                if (student != null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { student }
                    };
                }
                else
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { message = "No student found" }
                    };
                }

                return response;

            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error:" + ex.Message }
                };

                return response;
            }
        }

        public BaseResponse UpdateStudentById(long id, UpdateStudentRequest request)
        {
            BaseResponse response;

            try
            {

                using (context)
                {
                    StudentModel filteredStudent = context.Students.Where(s => s.Id == id).FirstOrDefault();

                    if (filteredStudent != null)
                    {
                        filteredStudent.first_name = request.first_name;
                        filteredStudent.last_name = request.last_name;
                        filteredStudent.address = request.address;
                        filteredStudent.email = request.email;
                        filteredStudent.contact_number = request.contact_number;

                        context.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Student updated successfully" }
                        };
                    }
                    else
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = "No student found" }
                        };
                    }
                    return response;
                }

            }

            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error:" + ex.Message }

                };

                return response;
            }
        }

        public BaseResponse DeleteStudentById(long id)
        {
            BaseResponse response;

            try
            {
                using (context)
                {

                    StudentModel studentToDelete = context.Students.Where(s => s.Id == id).FirstOrDefault();

                    if (studentToDelete != null)
                    {
                        context.Students.Remove(studentToDelete);
                        context.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Student deleted successfully" }
                        };
                    }
                    else
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = "No student found" }
                        };
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error: " + ex.Message }
                };

                return response;
            }
        }

        public BaseResponse StudentList()
        {
            BaseResponse response;

            try
            {
                List<StudentDTO> Student = new List<StudentDTO>();

                using (context)
                {
                    context.Students.ToList().ForEach(student => Student.Add(new StudentDTO
                    {
                        first_name = student.first_name,
                        last_name = student.last_name,
                        address = student.address,
                        email = student.email,
                        contact_number = student.contact_number

                    }));
                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { Student }
                };
                return response;
            }

            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error : " + ex.Message }
                };

                return response;
            }

        }

        public BaseResponse AuthenticateUser(AuthenticateRequest request)
        {
            BaseResponse response;

            try
            {

                StudentModel student = context.Students.FirstOrDefault(u => u.first_name == request.username && u.password == request.password);


                if (student != null)
                {

                    string jwtToken = JwtUtils.GenerateJwtToken(student);

                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { token = jwtToken }
                    };
                }
                else
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status401Unauthorized,
                        data = new { message = "Invalid username or password" }
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error: " + ex.Message }
                };
                return response;
            }
        }
        public BaseResponse RegisterUser(RegisterUserRequest request)
        {
            BaseResponse response;

            try
            {

                if (context.Students.Any(u => u.first_name == request.user_name || u.email == request.email))
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { message = "Username or email is already taken" }
                    };
                }
                else
                {

                    StudentModel newUser = new StudentModel
                    {
                        first_name = request.user_name,
                        password = request.password,         
                        email = request.email,
                        address = request.address,
                        contact_number = request.contact_number,
                        last_name = request.last_name,


                    };

                    using (context)
                    {
                        context.Students.Add(newUser);
                        context.SaveChanges();
                    }

                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { message = "User registered successfully" }
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DbUpdateException: {ex.Message}");

                // Log the specific details of the inner exception
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine($"Inner Exception: {innerException.Message}");
                    innerException = innerException.InnerException;
                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error: " + ex.Message }
                };
                return response;
            }
        }
    }
}
