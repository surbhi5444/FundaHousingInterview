
using FundaHousing.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundaHousing.DataAccessLayer
{
   public interface IHousingMakelaarData
    {
        List<HousingMakelaarModel> CalculateTopSellingMakelaarData();
    }
}
