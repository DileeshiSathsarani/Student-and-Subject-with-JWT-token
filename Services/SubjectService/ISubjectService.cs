using WebApplication1.DTOs;
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responses;
using WebApplication1.Models;

namespace WebApplication1.Services.SubjectService
{
    public interface ISubjectService
    {
        BaseResponse CreateSubject (CreateSubjectRequest request) ;

        BaseResponse SubjectList();

        BaseResponse GetSubjectById(long id);

        BaseResponse UpdateSubjectById(long id, UpdateSubjectRequest request);

        BaseResponse DeleteSubjectById(long id);
    }
}
