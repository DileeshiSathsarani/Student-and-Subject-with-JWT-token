using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.DTOs.Responses;
using WebApplication1.Middlewares;
using static WebApplication1.Helpers.Utils.AuthService;
using WebApplication1.DTOs;
using System.Net;
using WebApplication1.Helpers.Utils;

namespace WebApplication1.Middlewares
{

    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            string? token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ")[1];


            if (token == null)
            {
                if (IsEnabledUnauthorizedRoute(httpContext))
                {
                    return _next(httpContext);
                }

                BaseResponse response = new BaseResponse(HttpStatusCode.Unauthorized, new MessageDTO("Unauthorized"));
                httpContext.Response.StatusCode = response.status_code;
                httpContext.Response.ContentType = "application/json";
                return httpContext.Response.WriteAsJsonAsync(response);
            }

            else
            {
                if (JwtUtils.ValidateJwtToken(token))
                {
                    return _next(httpContext);

                }

                else
                {

                    BaseResponse response = new BaseResponse(HttpStatusCode.Unauthorized, new MessageDTO("Unauthorized"));
                    httpContext.Response.StatusCode = response.status_code;
                    httpContext.Response.ContentType = "application/json";
                    return httpContext.Response.WriteAsJsonAsync(response);
                }




            }

        }




        ///<summary>
        ///</summary>
        ///<param name="httpContext"></param>
        /// <returns></returns>

        private bool IsEnabledUnauthorizedRoute(HttpContext httpContext)
        {
            List<string> enabledRoute = new List<string>
        {
            "/api/Student/register",
            "/api/Authentication/authenticate",
            "/api/Student/login",
            "/api/Story/add",
            "/api/Story/list",



        };

            bool isEnableAuthorizedRoute = false;

            if (httpContext.Request.Path.Value is not null)
            {
                isEnableAuthorizedRoute = enabledRoute.Contains(httpContext.Request.Path.Value);
            }

            return isEnableAuthorizedRoute;
        }

    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();

        }
    }

}