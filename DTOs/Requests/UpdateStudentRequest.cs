﻿namespace WebApplication1.DTOs.Requests
{
    public class UpdateStudentRequest : CreateStudentRequest
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string contact_number { get; set; }
    }
}
