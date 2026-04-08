namespace Eshop.Shared
{
    public class ApiResponse<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; } = default!;
        public T Data { get; set; }

        public string TraceId { get; set; } = default!;

        public static ApiResponse<T> SuccessResponse(T data, string traceid)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
               TraceId = traceid
            };
        }
        public static ApiResponse<T> FailureResponse(string message, string traceid)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
            };
        }

    }
}
