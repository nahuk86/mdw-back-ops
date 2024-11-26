namespace MDW_Back_ops.Models
{
    public class Scanner
    {
        public Guid ScannerId { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public bool Enable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
