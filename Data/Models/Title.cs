namespace Data.Models
{
    public class Title
    {
        public string Tconst { get; set; } = string.Empty;
        public required IMDBType Type { get; set; }
        public required string PrimaryTitle { get; set; }
        public required string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? RuntimeMinutes { get; set; }
        public List<Genre> Genres { get; set; } = new();


        public override string ToString()
        {
            string adultFlag = IsAdult ? " (Adult)" : "";
            string runtime = RuntimeMinutes.HasValue ? $"{RuntimeMinutes} min" : "N/A";
            string startYear = StartYear.HasValue ? StartYear.Value.ToString() : "Unknown";
            string endYear = EndYear.HasValue ? $" - {EndYear.Value}" : "";
            string genreList = string.Join(", ", Genres.Select(g => g.Name));
            string typeName = Type.Name;

            return $"{PrimaryTitle} [{typeName}]{adultFlag} ({startYear}{endYear}) - {runtime} | Genres: {genreList}";
        }
    }
}
