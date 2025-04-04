using Data.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data
{
    public class TitleRepositorySql : ITitleRepository
    {
        private readonly string _connectionString;

        public TitleRepositorySql(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddTitle(Title title)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("dbo.AddMovie", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PrimaryTitle", title.PrimaryTitle);
            cmd.Parameters.AddWithValue("@OriginalTitle", title.OriginalTitle);
            cmd.Parameters.AddWithValue("@TitleType", title.Type.Name);
            cmd.Parameters.AddWithValue("@IsAdult", title.IsAdult);
            cmd.Parameters.AddWithValue("@StartYear", title.StartYear ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@EndYear", title.EndYear ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@RuntimeMinutes", title.RuntimeMinutes ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Genres", title.Genres.Any() ? string.Join(",", title.Genres.Select(g => g.Name)) : (object)DBNull.Value);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteTitle(Title title)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("dbo.DeleteMovie", conn); // Brug din valgte sletteprocedure
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Tconst", title.Tconst );

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Title> GetTitles(string title, int offset, int pageSize, IMDBType? type)
        {
            var titles = new List<Title>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("dbo.SearchTitlesSorted", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SearchTerm", title);
            cmd.Parameters.AddWithValue("@Offset", offset);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            cmd.Parameters.AddWithValue("@TitleType", (object?)type?.Name ?? DBNull.Value);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var genres = reader["Genres"]?.ToString()?.Split(", ") ?? Array.Empty<string>();

                titles.Add(new Title
                {
                    Tconst = reader["Tconst"].ToString()!,
                    Type = new IMDBType { Name = reader["TitleType"].ToString()! },
                    PrimaryTitle = reader["PrimaryTitle"].ToString()!,
                    OriginalTitle = reader["OriginalTitle"].ToString()!,
                    IsAdult = (bool)reader["IsAdult"],
                    StartYear = reader["StartYear"] as int?,
                    EndYear = reader["EndYear"] as int?,
                    RuntimeMinutes = reader["RuntimeMinutes"] as int?,
                    Genres = genres.Select(g => new Genre { Name = g }).ToList()
                });
            }

            return titles;
        }

        public void UpdateTitle(Title original, Title updated)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("dbo.UpdateMovie", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Tconst", original.Tconst);
            cmd.Parameters.AddWithValue("@TitleType", updated.Type.Name);
            cmd.Parameters.AddWithValue("@PrimaryTitle", updated.PrimaryTitle);
            cmd.Parameters.AddWithValue("@OriginalTitle", updated.OriginalTitle);
            cmd.Parameters.AddWithValue("@IsAdult", updated.IsAdult);
            cmd.Parameters.AddWithValue("@StartYear", updated.StartYear ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@EndYear", updated.EndYear ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@RuntimeMinutes", updated.RuntimeMinutes ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Genres", updated.Genres.Any() ? string.Join(",", updated.Genres.Select(g => g.Name)) : (object)DBNull.Value);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<IMDBType> GetTypes()
        {
            var types = new List<IMDBType>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT Id, Name FROM dbo.AllTypes", conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                types.Add(new IMDBType
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()!
                });
            }

            return types;
        }

        public List<Genre> GetGenres()
        {
            var genres = new List<Genre>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT Id, Name FROM dbo.AllGenres", conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                genres.Add(new Genre
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()!
                });
            }

            return genres;
        }
    }
}
