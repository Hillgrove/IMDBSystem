namespace Data.Models
{
    public class Name
    {
        public required string PrimaryName { get; set; }
        public int BirthYear { get; set; }
        public int? DeathYear { get; set; }

        public override string ToString()
        {
            string birth = BirthYear > 0 ? BirthYear.ToString() : "Unknown";
            string death = DeathYear.HasValue ? DeathYear.Value.ToString() : "N/A";
            return $"{PrimaryName} (Born: {birth}, Died: {death})";
        }
    }
}
