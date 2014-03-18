using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WonderfulGames.Model.DAL
{
    /// <summary>
    /// Klass för CRUD-funktionalitet mot tabellen Game.
    /// </summary>
    public class GameDAL : DALBase
    {
        /// <summary>
        /// Hämtar ett specifikt spel.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>Ett Game-objekt med ett spels uppgifter.</returns>
        public Game GetGameById(int gameId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.GetGameById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@GameID", gameId);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var gameIdIndex = reader.GetOrdinal("GameID");
                            var genreidIndex = reader.GetOrdinal("GenreID");
                            var DeveloperidIndex = reader.GetOrdinal("DeveloperID");
                            var titelIndex = reader.GetOrdinal("Title");
                            var relesedIndex = reader.GetOrdinal("Released");
                            var gradeIndex = reader.GetOrdinal("Grade");

                            return new Game
                            {
                                GameID = reader.GetInt32(gameIdIndex),
                                GenreID = reader.GetInt32(genreidIndex),
                                DeveloperID = reader.GetInt32(DeveloperidIndex),
                                Title = reader.GetString(titelIndex),
                                Released = reader.GetDateTime(relesedIndex),
                                Grade = reader.GetInt32(gradeIndex)
                            };
                        }
                    }
                    return null;
                }
                catch
                {
                    throw new ApplicationException();
                }
            }
        }

        /// <summary>
        /// Hämtar ut alla spel och räknar upp hur många rader och sidor det kommer att bli.
        /// </summary>
        /// <param name="maximumRows">Hur många rader som får finnas på varje sida</param>
        /// <param name="startRowIndex">Innehåller 1</param>
        /// <param name="totalRowCount">Innehåller 0 men kommer att innehålla antalet poster som räknats fram</param>
        /// <returns>Alla spelen som från tabellen.</returns>
        public IEnumerable<Game> GetGamesPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var games = new List<Game>(100);

                    SqlCommand cmd = new SqlCommand("appschema.GetGamesPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@GameCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var gameIdIndex = reader.GetOrdinal("GameID");
                        var genreidIndex = reader.GetOrdinal("GenreID");
                        var DeveloperidIndex = reader.GetOrdinal("DeveloperID");
                        var titelIndex = reader.GetOrdinal("Title");
                        var relesedIndex = reader.GetOrdinal("Released");
                        var gradeIndex = reader.GetOrdinal("Grade");

                        while (reader.Read())
                        {
                            games.Add(new Game
                            {
                                GameID = reader.GetInt32(gameIdIndex),
                                GenreID = reader.GetInt32(genreidIndex),
                                DeveloperID = reader.GetInt32(DeveloperidIndex),
                                Title = reader.GetString(titelIndex),
                                Released = reader.GetDateTime(relesedIndex),
                                Grade = reader.GetInt32(gradeIndex)
                            });
                        }
                    }
                    totalRowCount = (int)cmd.Parameters["@GameCount"].Value;
                    games.TrimExcess();
                    return games;
                }
                catch(Exception)
                {
                    throw new ApplicationException();
                }
            }
        }

        /// <summary>
        /// Lägger till ett spel i databasen.
        /// </summary>
        /// <param name="game">Spelets uppgifter som skall sparas</param>
        public void InsertGame(Game game)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appschema.InsertGame", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@GenreID", SqlDbType.Int, 4).Value = game.GenreID;
                    cmd.Parameters.Add("@DeveloperID", SqlDbType.Int, 4).Value = game.DeveloperID;
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar, 50).Value = game.Title;
                    cmd.Parameters.Add("@Released", SqlDbType.Date).Value = game.Released;
                    cmd.Parameters.Add("@Grade", SqlDbType.Int, 4).Value = game.Grade;

                    cmd.Parameters.Add("@GameID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException();
                }
            }
        }

        /// <summary>
        /// Updaterar uppgifterna i Game tabellen i databasen.
        /// </summary>
        /// <param name="game">Spelets uppgifter som skall uppdateras</param>
        public void UpdateGame(Game game)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appschema.UpdateGame", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@GameID", game.GameID);
                    cmd.Parameters.Add("@GenreID", SqlDbType.Int, 4).Value = game.GenreID;
                    cmd.Parameters.Add("@DeveloperID", SqlDbType.Int, 4).Value = game.DeveloperID;
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar, 50).Value = game.Title;
                    cmd.Parameters.Add("@Released", SqlDbType.Date).Value = game.Released;
                    cmd.Parameters.Add("@Grade", SqlDbType.Int, 4).Value = game.Grade;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException();
                }
            }
        }

        /// <summary>
        /// Tar bort ett spel
        /// </summary>
        /// <param name="gameId">Id't på det specifika spelet</param>
        public void DeleteGame(int gameId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appschema.DeleteGame", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GameID", gameId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException();
                }
            }
        }
    }
}
