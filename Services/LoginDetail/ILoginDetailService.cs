
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responses;

namespace WebApplication1.Services.LoginDetail
{
    public interface ILoginDetailService
    {
        BaseResponse CreateLogin(AuthenticateRequest request);

        BaseResponse GetLoginById(int id);
    }
}
