using System.Net.NetworkInformation;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responses;
using WebApplication1.Models;

namespace WebApplication1.Services.SubjectService
{
    public class SubjectService : ISubjectService 
    {
        private readonly ApplicationDBContext context;

        public SubjectService(ApplicationDBContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        public BaseResponse CreateSubject(CreateSubjectRequest request)
        {
            BaseResponse response;

            try
            {
                SubjectModel newSubject = new SubjectModel();
                newSubject.subject_code = request.subject_code;
                newSubject.subject_name = request.subject_name;

                using (context)
                {
                    context.Add(newSubject);
                    context.SaveChanges();
                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new subject" }
                };

                return response;

            }

            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error :" + ex.Message }
                };
                return response;
            }

        }

        public BaseResponse SubjectList()
        {
            BaseResponse response;

            try
            {
                List<SubjectDTO> Subject = new List<SubjectDTO>();

                using (context)
                {
                    context.Subjects.ToList().ForEach(subject => Subject.Add(new SubjectDTO
                    {
                        subject_code =subject.subject_code,
                        subject_name =subject.subject_name,


                    }));
                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { Subject }
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

        public BaseResponse GetSubjectById(long id)
        {
            BaseResponse response;

            try
            {
                SubjectDTO subject = new SubjectDTO();

                using (context)
                {
                    SubjectModel filteredSubject = context.Subjects.Where(subject => subject.subject_code == id).FirstOrDefault();

                    if (filteredSubject != null)
                    {
                        subject.subject_code = filteredSubject.subject_code;
                        subject.subject_name = filteredSubject.subject_name;
                        
                    }

                    else
                    {
                        subject = null;
                    }
                }

                if (subject != null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { subject }
                    };
                }

                else
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { message = "No subject found" }
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

        public BaseResponse UpdateSubjectById(long id, UpdateSubjectRequest request)
        {
            BaseResponse response;

            try
            {

                using (context)
                {
                    SubjectModel filteredSubject = context.Subjects.Where(subject => subject.subject_code == id).FirstOrDefault();

                    if (filteredSubject != null)
                    {
                        filteredSubject.subject_code = request.subject_code;
                        filteredSubject.subject_name = request.subject_name;
                           

                        context.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Subject updated successfully" }
                        };
                    }
                    else
                    {
                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status400BadRequest,
                            data = new { message = "No subject found" }
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

        public BaseResponse DeleteSubjectById(long id)
        {
            BaseResponse response;

            try
            {
                using (context)
                {

                    SubjectModel subjectToDelete = context.Subjects.Where(subject => subject.subject_code == id).FirstOrDefault();

                    if (subjectToDelete != null)
                    {
                        context.Subjects.Remove(subjectToDelete);
                        context.SaveChanges();

                        response = new BaseResponse
                        {
                            status_code = StatusCodes.Status200OK,
                            data = new { message = "Subject deleted successfully" }
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
                    data = new { message = "Internal server error :" + ex.Message }
                };

                return response;
            }
        }
    }
}
