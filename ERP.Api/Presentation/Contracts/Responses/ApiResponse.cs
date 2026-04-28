namespace ERP.Api.Presentation.Contracts.Responses
{
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public int status_code { get; set; }
        public string message { get; set; }
        public T? data { get; set; }
        public object? errors { get; set; }
    }
}
