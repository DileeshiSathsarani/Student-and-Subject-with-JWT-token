using System.Net;

namespace WebApplication1.DTOs.Responses
{
    public class BaseResponse
    {
        public int status_code { get; set; }
        public object data { get; set; }

        public BaseResponse()
        {
        }

        public BaseResponse(HttpStatusCode statusCode, object data)
        {
            this.status_code = (int)statusCode;
            this.data = data;
        }

        ///<summery>
        ///</summery>
        ///<param name="statusCode"></param>
        ///<param name="data"></param>

        public void CreateResponse(HttpStatusCode statusCode, Object data)
        {
            status_code = (int) statusCode;
            this.data = data;
        }

        public class MessageResponse
        {
            public string Message { get; set; }
        }

        public class AuthenticateResponse
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class ListStoriesResponse
        {
            public List<StoryResponseItem> Stories { get; set; }
        }

        public class StoryResponseItem
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
    }
}
