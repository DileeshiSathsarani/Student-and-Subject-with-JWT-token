﻿namespace Middleware.DTOs.Requests
{
    public class RegisterUserRequest
    {
        public string user_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public string address { get; set; }
        public string contact_number { get; set; }

        public string last_name { get; set; }
    }
}
