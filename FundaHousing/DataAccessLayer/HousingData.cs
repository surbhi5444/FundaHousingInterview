
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
    public class HousingData : IHousingData
    {
        private readonly ILogger _logger;
        public HousingData() { }
        public HousingData(ILogger logger)
        {
            _logger = logger;
        }
        public DataSet GetHousingDetails(string cityName, string housingKey)
        {
            DataSet dsHousing = null;
           // DataTable tblHousing = null;
           //List<HousingModel> lstHGSM = new List<HousingModel>();
            try
            {
               // HousingModel objHGSM;
                string sConnectionString = ConfigurationManager.ConnectionStrings["FundaHousesDB"].ConnectionString;
                using (SqlConnection objConn = new SqlConnection(sConnectionString))
                {
                    string sqlQuery = "select *" +
                       "from[dbo].[FundaHousingDetails]" +
                       " where Woonplaats = '" + cityName + "' and PromoLabelTagline " +
                       "like '%" + housingKey + "%'";

                    objConn.Open();
                    using (SqlDataAdapter daHousing = new SqlDataAdapter(sqlQuery, objConn))
                    {
                        dsHousing = new DataSet("Housing");
                        daHousing.Fill(dsHousing, "Housing");

                        //tblHousing = dsHousing.Tables["Housing"];

                        //foreach (DataRow drCurrent in tblHousing.Rows)
                        //{
                        //    objHGSM = new HousingModel();
                        //    objHGSM.AangebodenSindsTekst = drCurrent["AangebodenSindsTekst"].ToString();
                        //    objHGSM.MakelaarId = Convert.ToInt32(drCurrent["MakelaarId"]);
                        //    objHGSM.MakelaarNaam = drCurrent["MakelaarNaam"].ToString();
                        //    objHGSM.VerkoopStatus = drCurrent["VerkoopStatus"].ToString();
                        //    objHGSM.PromoLabelTagline = drCurrent["PromoLabelTagline"].ToString();
                        //    objHGSM.Woonplaats = drCurrent["Woonplaats"].ToString();
                        //    lstHGSM.Add(objHGSM);
                        //}
                    }
                    objConn.Close();
                }
            }

            catch (Exception ex)
            {
                _logger.Error(ex.Message, "Error happened while connectiong to database");
            }

            _logger.Information("Data has been successfully retrived from database");

            return dsHousing;
        }
    }
}