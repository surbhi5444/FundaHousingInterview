
using FundaHousing.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundaHousing.DAL
{
   public interface IHousingData
    {
        //  List<HousingModel> GetHousingDetails(string cityName, string housingKey);
        DataSet GetHousingDetails(string cityName, string housingKey);
    }
}
