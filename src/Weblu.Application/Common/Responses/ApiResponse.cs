namespace Weblu.Application.Common.Responses
{
    public class ApiResponse
    {
        public string Message { get; set; }

        public ApiResponse(string message)
        {
            Message = message;
        }

        public static ApiResponse Success(string message) => new(message);
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public ApiResponse(string message, T data) : base(message)
        {
            Data = data;
        }

        public static ApiResponse<T> Success(string message, T data) => new(message, data);
}
}