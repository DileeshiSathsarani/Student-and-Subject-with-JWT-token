using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Data;
using WebApplication1.Models;

namespace Middleware.Helpers.Utils
{
    public static class JwtUtils
    {
        static string secret = "3hO4Lash4CzZfk0Ga6yQhd48208RGTAu";

        public static string GenerateJwtToken(StudentModel student)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(secret);

            List<Claim> claims = new List<Claim>
            {
                new Claim("Id",student.Id.ToString()),
                new Claim("first_name", student.first_name),
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwtToken);
        }


        public static bool ValidateJwtToken(string jwt)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(secret);

                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };

                tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);
                JwtSecurityToken validatedJWT = (JwtSecurityToken)validatedToken;


                long Id = long.Parse(validatedJWT.Claims.First(claim => claim.Type == "Id").Value);
                using (ApplicationDBContext dbContext = new ApplicationDBContext())
                {

                    StudentModel? student = dbContext.Students.FirstOrDefault(u => u.Id == Id);

                    if (student == null)
                    {
                        return false;
                    }
                    else
                    {
                        LoginDetailModel loginDetail = dbContext.LoginDetails.FirstOrDefault(ld => ld.Id == Id);

                        if (loginDetail.token != jwt)
                        {
                            return false;
                        }


                        return true;
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during JWT validation: {ex.Message}");
                return false;
            }
        }


    }
}
