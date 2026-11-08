using System;
using System.Collections.Generic;

namespace DbEntities.Entities;

public partial class Rating
{
    public int RatingId { get; set; }

    public int? LoginId { get; set; }

    public string FeedBack { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Login? Login { get; set; }
}
