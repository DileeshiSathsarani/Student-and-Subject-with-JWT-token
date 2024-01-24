using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs;
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responses;
using WebApplication1.Models;


namespace WebApplication1.Services.StudentService
{
    public interface IStudentService
    {
        BaseResponse CreateStudent(CreateStudentRequest request);

        BaseResponse StudentList();

        BaseResponse GetStudentById(long id);

        BaseResponse UpdateStudentById(long id, UpdateStudentRequest request);

        BaseResponse DeleteStudentById(long id);

        BaseResponse StudentList(int page, int pageSize);

        BaseResponse RegisterUser(RegisterUserRequest request);
        BaseResponse AuthenticateUser(AuthenticateRequest request);


    }
}
