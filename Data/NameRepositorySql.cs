
using Data.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data
{
    public class NameRepositorySql : INameRepository
    {
        private readonly string _connectionString;

        public NameRepositorySql(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddName(Name name)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("dbo.AddPerson", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PrimaryName", name.PrimaryName);
            cmd.Parameters.AddWithValue("@BirthYear", name.BirthYear);
            cmd.Parameters.AddWithValue("@DeathYear", name.DeathYear ?? (object)DBNull.Value);
          
            conn.Open();
            cmd.ExecuteNonQuery();
        }


        public List<Name> GetNames(string name)
        {
            var result = new List<Name>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("dbo.SearchPersonsSorted", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@searchTerm", SqlDbType.NVarChar, 100).Value = name;

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var item = new Name
                {
                    PrimaryName = reader["PrimaryName"].ToString()!,
                    BirthYear = reader["BirthYear"] as int? ?? 0,
                    DeathYear = reader["DeathYear"] as int?
                };

                result.Add(item);
            }

            return result;
        }
    }
}
