using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responses;
using WebApplication1.Services;
using WebApplication1.Services.StudentService;
using WebApplication1.DTOs.Responses;
using Middleware.Services.LoginDetail;
using Microsoft.AspNetCore.Authorization;

namespace Middleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
        
    {
        private readonly ILoginDetailService LoginDetailService;

        public AuthenticationController(ILoginDetailService LoginDetailService)
        {

            this.LoginDetailService = LoginDetailService;

        }

       /* [HttpPost("authenticate")]
        public IActionResult CreateLogin(AuthenticateRequest request)
        {
            BaseResponse response = LoginDetailService.CreateLogin(request);
            return StatusCode(response.status_code, response);
        }*/
    }
}
