
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace FundaHousing.DataAccessLayer
{
    public class HousingJsonDataExtractor : IHousingJsonDataExtractor
    {
        private readonly ILogger _logger;
        public HousingJsonDataExtractor() { }
        public HousingJsonDataExtractor(ILogger logger)
        {
            _logger = logger;
        }
        public void ExtractJsonToUpdateDatabase()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["FundaHousesDB"].ConnectionString;

                string fundaJsonFilePath = HttpContext.Current.Server.MapPath(@"/JsonFile/download.json");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand("SELECT COUNT(MakelaarId) FROM dbo.FundaHousingDetails", conn);
                    var count = (Int32) comm.ExecuteScalar();
                    if (count == 0)
                    {
                        // 1.  create a command object identifying the stored procedure
                        SqlCommand cmd = new SqlCommand("sp_GetFundaHousingJsonData", conn);

                        // 2. set the command object so it knows to execute a stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        //// 3. add parameter to command, which will be passed to the stored procedure
                        cmd.Parameters.Add(new SqlParameter("@FilePath", fundaJsonFilePath));

                        // execute the command

                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, "Error happened while Updating to database");
            }
            _logger.Information("Database has been successfully updated");

        }
    }
}