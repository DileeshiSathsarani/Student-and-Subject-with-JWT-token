namespace WebApplication1.DTOs.Requests
{
    public class UpdateSubjectRequest : CreateSubjectRequest
    {
        public long subject_code { get; set; }
        public long id { get; set; }
        public string subject_name { get; set; }
    }
}
