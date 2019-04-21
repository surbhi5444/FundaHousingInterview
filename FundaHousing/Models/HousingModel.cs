using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FundaHousing.Models
{
    public class HousingModel
    {
        public string AangebodenSindsTekst { get; set; }
        public int MakelaarId { get; set; }
        public string MakelaarNaam { get; set; }
        public string VerkoopStatus { get; set; }
        public string PromoLabelTagline { get; set; }
        public string Woonplaats { get; set; }
    }
}