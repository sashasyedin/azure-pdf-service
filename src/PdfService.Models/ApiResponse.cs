namespace PdfService.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
        }

        public ApiResponse(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public T Payload { get; set; }
        public ErrorCode ErrorCode { get; }
        public bool HasError => ErrorCode != ErrorCode.None;
    }
}