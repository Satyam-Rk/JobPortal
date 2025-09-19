using System.Net;

namespace JobPortal.DataModels
{
    public class ResultDTO
    {
        public bool Result { get; set; }
        public string? ResultMessage { get; set; }
        public dynamic Details { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
