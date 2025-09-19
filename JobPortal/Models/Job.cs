using System;
using System.Collections.Generic;

namespace JobPortal.Models;

public partial class Job
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int LocationId { get; set; }

    public int DepartmentId { get; set; }

    public DateTime PostedDate { get; set; }

    public DateTime ClosingDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;
}
