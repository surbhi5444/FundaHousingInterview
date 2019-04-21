
using FundaHousing.DAL;
using FundaHousing.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FundaHousing.DataAccessLayer
{
    public class HousingMakelaarData : IHousingMakelaarData
    {
        private readonly ILogger _logger;
        public HousingMakelaarData() { }
        public HousingMakelaarData(ILogger logger)
        {
            _logger = logger;
        }
        public List<HousingMakelaarModel> CalculateTopSellingMakelaarData()
        {
            DataSet dsHousing=null;
            List<HousingMakelaarModel> lstHGSM = new List<HousingMakelaarModel>();
            try
            {
                HousingMakelaarModel objHGSM;
                string sConnectionString = ConfigurationManager.ConnectionStrings["FundaHousesDB"].ConnectionString;
                using (SqlConnection objConn = new SqlConnection(sConnectionString))
                {
                    string sqlQuery = "SELECT top(10) count(MakelaarId) as NumberOfObjectListedForSale,MakelaarId, " +
                            "MakelaarNaam,VerkoopStatus " +
                            "FROM " +
                            "[dbo].[FundaHousingDetails]" +
                            "group by " +
                            "MakelaarId, MakelaarNaam, VerkoopStatus " +
                            "order by " +
                            "NumberOfObjectListedForSale desc";
                    objConn.Open();
                    using (SqlDataAdapter daHousing = new SqlDataAdapter(sqlQuery, objConn))
                    {

                         dsHousing = new DataSet("Housing");
                        daHousing.Fill(dsHousing, "Housing");
                        DataTable tblHousing;
                        tblHousing = dsHousing.Tables["Housing"];

                        foreach (DataRow drCurrent in tblHousing.Rows)
                        {
                            objHGSM = new HousingMakelaarModel();
                            objHGSM.MakelaarId = Convert.ToInt32(drCurrent["MakelaarId"]);
                            objHGSM.MakelaarNaam = drCurrent["MakelaarNaam"].ToString();
                            objHGSM.VerkoopStatus = drCurrent["VerkoopStatus"].ToString();
                            lstHGSM.Add(objHGSM);
                        }
                        objConn.Close();

                    }

                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, "Error happened while connectiong to database");
            }

            _logger.Information("Data has been successfully retrived from database");
            return lstHGSM;
        }
    }
}
