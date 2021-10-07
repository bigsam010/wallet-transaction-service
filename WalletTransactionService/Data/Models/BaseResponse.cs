

namespace API.Data.Models
{
    public class BaseResponse
    {
        public bool Status { set; get; }
        public string Message { set; get; }

        public BaseResponse()
        {
        }
        public BaseResponse(bool Status, string Message)
        {
            this.Status = Status;
            this.Message = Message;
        }

        public class ApiResponse<T>
        {
            public int StatusCode { set; get; }
            public string Message { set; get; }
            public bool IsSuccessful { set; get; }
            public T Data { set; get; }
        }
    }
    public class BaseResponse<T>
    {
        public bool Status { set; get; }
        public string Message { set; get; }
        public T Data { set; get; }
        public BaseResponse(bool Status, string Message, T Data)
        {
            this.Status = Status;
            this.Message = Message;
            this.Data = Data;
        }
        public BaseResponse(bool Status, string Message)
        {
            this.Status = Status;
            this.Message = Message;
            
        }
    }
}
