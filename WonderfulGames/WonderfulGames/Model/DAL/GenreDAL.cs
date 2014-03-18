using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WonderfulGames.Model.DAL
{
    /// <summary>
    /// Klass för CRUD-funktionalitet mot tabellen Genre.
    /// </summary>
    public class GenreDAL : DALBase
    {
        /// <summary>
        /// Hämtar alla genrar
        /// </summary>
        /// <returns>Ett Genre-objekt med alla genrar</returns>
        public IEnumerable<Genre> GetGenreType()
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var genre = new List<Genre>(100);

                    SqlCommand cmd = new SqlCommand("appschema.Simple_GenreLista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var genreIdIndex = reader.GetOrdinal("GenreID");
                        var genreTypeIndex = reader.GetOrdinal("GenreType");


                        while (reader.Read())
                        {
                            genre.Add(new Genre
                            {
                                GenreID = reader.GetInt32(genreIdIndex),
                                GenreType = reader.GetString(genreTypeIndex)
                            });
                        }
                    }
                    genre.TrimExcess();
                    return genre;
                }
                catch (Exception)
                {
                    throw new ApplicationException();
                }
            }
        }
    }
}