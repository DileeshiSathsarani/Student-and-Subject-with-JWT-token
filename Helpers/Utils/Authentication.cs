using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs.Requests;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Middleware.Helpers.Utils;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.DTOs;
using WebApplication1.Data;
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responses;
using WebApplication1.Models;
using WebApplication1.Helpers.Utils;

namespace Middleware.Helpers.Utils
{
    public class AuthService
    {
        private readonly ApplicationDBContext DbContext;

        public AuthService(ApplicationDBContext dbContext)
        {
            DbContext = dbContext;
        }

        public BaseResponse Authenticate(AuthenticateRequest request)
        {
            try
            {
                StudentModel? student = DbContext.Students.Where(u => u.user_name == request.username).FirstOrDefault();

                if (student == null)
                {
                    return new BaseResponse(HttpStatusCode.Unauthorized, new MessageDTO("Invalid username or password"));
                }

                string md5Password = Supports.GenerateMD5(request.password);

                if (student.password == md5Password)
                {
                    string jwt = JwtUtils.GenerateJwtToken(student);

                    LoginDetailModel? loginDetail = DbContext.LoginDetails.Where(ld => ld.Id == student.Id).FirstOrDefault();

                    if (loginDetail == null)
                    {
                        loginDetail = new LoginDetailModel();
                        loginDetail.Id = student.Id;
                        loginDetail.token = jwt;

                        DbContext.LoginDetails.Add(loginDetail);
                    }
                    else
                    {
                        loginDetail.token = jwt;
                    }

                    DbContext.SaveChanges();

                    return new BaseResponse(HttpStatusCode.OK, new { token = jwt });
                }
                else
                {
                    return new BaseResponse(HttpStatusCode.Unauthorized, new MessageDTO("Invalid username or password"));
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse { status_code = StatusCodes.Status500InternalServerError, data = new MessageDTO(ex.Message) };
            }
        }
    }
}


