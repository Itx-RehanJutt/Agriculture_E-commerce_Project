namespace My.Project.ViewModel
{
    public class RatingVM
    {
        public int RatingId { get; set; }
        public string RatingName { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
