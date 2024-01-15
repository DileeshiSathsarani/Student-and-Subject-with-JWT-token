using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Data;
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responses;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace Middleware.Services.LoginDetail
{
    public class LoginDetailService : ILoginDetailService
    {
        private readonly ApplicationDBContext context;

        public LoginDetailService(ApplicationDBContext applicationDbContext)
        {
            context = applicationDbContext;
        }


        public BaseResponse CreateLogin(AuthenticateRequest request)
        {
            BaseResponse response;

            try
            {
                StudentModel authenticatedUser = AuthenticateUser(request.username, request.password);

                if (authenticatedUser != null)
                {

                    LoginDetailModel newLogin = new LoginDetailModel
                    {
                        Id = authenticatedUser.Id,
                    };

                    using (var dbContext = context)
                    {
                        dbContext.Add(newLogin);
                        dbContext.SaveChanges();
                    }

                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { message = "Successfully created the new Login" }
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

        private StudentModel AuthenticateUser(string username, string password)
        {
            if (username == "validUser" && password == "validPassword")
            {
                return new StudentModel
                {
                    Id = 1,

                };
            }

            return null;
        }

        public BaseResponse GetLoginById(int id)
        {
            BaseResponse response;

            try
            {
                LoginDTO logins = new LoginDTO();

                using (context)
                {
                    LoginDetailModel filteredLogin = context.LoginDetails.Where(logins => logins.Id == id).FirstOrDefault();

                    if (filteredLogin != null)
                    {
                        logins.username = filteredLogin.username;
                        logins.password = filteredLogin.password;

                    }

                    else
                    {
                        logins = null;
                    }
                }

                if (logins != null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { logins }
                    };
                }

                else
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { message = "No log found" }
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

    }
    }
