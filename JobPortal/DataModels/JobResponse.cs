namespace JobPortal.DataModels
{
    public class JobResponse
    {
        public int Total { get; set; }
        public List<JobDTO> Data { get; set; }
        public string Message { get; set; }
    }
}
