namespace Data.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
