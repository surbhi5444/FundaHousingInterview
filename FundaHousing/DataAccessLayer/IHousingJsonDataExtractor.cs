using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundaHousing.DataAccessLayer
{
   public interface IHousingJsonDataExtractor
    {
        void ExtractJsonToUpdateDatabase();
    }
}
