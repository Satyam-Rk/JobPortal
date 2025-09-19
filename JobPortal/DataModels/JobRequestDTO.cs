namespace JobPortal.DataModels
{
    public class JobRequestDTO
    {
        public string SearchKey { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int? LocationId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
