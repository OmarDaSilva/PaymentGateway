namespace PaymentGateway.Api.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }

        public ApiResponse(T data, string? message = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }

        public ApiResponse(string error)
        {
            Success = false;
            Error = error;
        }
    }
}

