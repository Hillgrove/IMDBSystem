namespace Data.Models
{
    public class Movie
    {
        public required string TitleType { get; set; }
        public required string PrimaryTitle { get; set; }
        public required string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? RuntimeMinutes { get; set; }
    }
}
