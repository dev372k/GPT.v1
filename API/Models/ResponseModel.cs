namespace API.Models
{
    public class ResponseModel
    {
        public bool Status { get; set; } = true;
        public int StatusCode { get; set; } = StatusCodes.Status200OK;
        public string Message { get; set; } = string.Empty;
        public object Data { get; set; }
    }
}
