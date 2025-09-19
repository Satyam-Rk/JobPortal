using System;
using System.Collections.Generic;

namespace JobPortal.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
