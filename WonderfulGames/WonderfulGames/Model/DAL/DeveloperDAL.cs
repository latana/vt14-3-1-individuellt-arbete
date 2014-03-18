using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WonderfulGames.Model.DAL
{
    /// <summary>
    /// Klass för CRUD-funktionalitet mot tabellen Developer.
    /// </summary>
    public class DeveloperDAL : DALBase
    {
        /// <summary>
        /// Hämtar en specifik utvecklare.
        /// </summary>
        /// <param name="developerId">En utvecklares id.</param>
        /// <returns>Ett Developer-objekt med en utvecklarens namn.</returns>
        public Developer GetDeveloperById(int developerId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.GetDeveloperById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DeveloperID", developerId);

                    conn.Open();
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var developerIdIndex = reader.GetOrdinal("DeveloperID");
                            var developerNameIndex = reader.GetOrdinal("DeveloperName");

                            return new Developer
                            {
                                DeveloperID = reader.GetInt32(developerIdIndex),
                                DeveloperName = reader.GetString(developerNameIndex),
                            };
                        }
                    }
                    return null;
                }
                catch
                {
                    throw new ApplicationException("Ett fel har inträffat i dataåtkomstlagret");
                }
            }
        }

        /// <summary>
        /// Hämtar alla utvecklare
        /// </summary>
        /// <returns>Ett Developer-objekt med alla utvecklare</returns>
        public IEnumerable<Developer> GetDeveloperName()
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var developer = new List<Developer>();

                    SqlCommand cmd = new SqlCommand("appschema.Simple_DeveloperLista", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var developeridIndex = reader.GetOrdinal("DeveloperID");
                        var developernameIndex = reader.GetOrdinal("DeveloperName");

                        while (reader.Read())
                        {
                            developer.Add(new Developer
                            {
                                DeveloperID = reader.GetInt32(developeridIndex),
                                DeveloperName = reader.GetString(developernameIndex)
                            });
                        }
                    }

                    // reglerar så att kapaciteten är beroende på listans längd.
                    developer.TrimExcess();

                    return developer;
                }
                catch (Exception)
                {
                    throw new ApplicationException();
                }
            }
        }

        /// <summary>
        /// Lägger till en utvecklare i databasen.
        /// </summary>
        /// <param name="developer">Utvecklarens namn som skall sparas</param>
        public void InsertDeveloper(Developer developer)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appschema.InsertDeveloper", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@DeveloperName", SqlDbType.VarChar, 100).Value = developer.DeveloperName;
                    cmd.Parameters.Add("@DeveloperID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    developer.DeveloperID = (int)cmd.Parameters["@DeveloperID"].Value;
                }
                catch
                {
                    throw new ApplicationException();
                }
            }
        }

        /// <summary>
        /// Updaterar namnet i Developer tabellen i databasen.
        /// </summary>
        /// <param name="developer">Utvecklarens namn som skall uppdateras</param>
        public void UpdateDeveloper(Developer developer)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appschema.UpdateDeveloper", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DeveloperID", developer.DeveloperID);
                    cmd.Parameters.Add("@DeveloperName", SqlDbType.VarChar, 100).Value = developer.DeveloperName;

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
        /// Tar bort en utvecklare
        /// </summary>
        /// <param name="developerId">Id't på den specifika utvecklaren</param>
        public void DeleteDeveloper(int developerId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appschema.DeleteDeveloper", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@DeveloperID", developerId);

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
        /// Hämtar ut alla utvecklare och räknar upp hur många rader och sidor det kommer att bli.
        /// </summary>
        /// <param name="maximumRows">Hur många rader som får finnas på varje sida</param>
        /// <param name="startRowIndex">Innehåller 1</param>
        /// <param name="totalRowCount">Innehåller inte någonting men kommer att innehålla antalet poster som skrivs ut</param>
        /// <returns></returns>
        public IEnumerable<Developer> GetDevelopersPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var developer = new List<Developer>();

                    SqlCommand cmd = new SqlCommand("appschema.GetDevelopersPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Tar reda på vad de olika kolumnerna heter och vad de innehåller.
                    // Det vill säga tar reda på hur mycket data vi måste hämta ut och hur lång listan blir
                    // för att sedan anpassa sidan.
                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@DeveloperCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var developerIdIndex = reader.GetOrdinal("DeveloperID");
                        var developerNameIndex = reader.GetOrdinal("DeveloperName");

                        while (reader.Read())
                        {
                            developer.Add(new Developer
                            {
                                
                                DeveloperID = reader.GetInt32(developerIdIndex),
                                DeveloperName = reader.GetString(developerNameIndex),

                            });
                        }
                    }
                    // Tilldelar totalRowCount innehållet från @DeveloperCount som har räknat upp hur många poster som kommer att skrivas ut.
                    totalRowCount = (int)cmd.Parameters["@DeveloperCount"].Value;

                    // reglerar så att kapaciteten är beroende på listans längd.
                    developer.TrimExcess();

                    return developer;
                }
                catch (Exception)
                {
                    
                    throw new ApplicationException();
                }
               
            }
        }
    }
}