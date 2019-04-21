
using FundaHousing.DAL;
using FundaHousing.DataAccessLayer;
using System.Web.Http;
using WebApiThrottle;

namespace FundaHousing.Controllers
{
    [EnableThrottling(PerMinute = 100)]
    public class KoopController : ApiController
    {
        private readonly IHousingJsonDataExtractor _housingJsonDataExtractor;
        private readonly IHousingData _housingData;
        private readonly IHousingMakelaarData _housingMakelaarData;

        public KoopController() { }
        public KoopController(IHousingJsonDataExtractor housingJsonDataExtractor,IHousingData housingData, IHousingMakelaarData housingMakelaarData)
        {
            _housingJsonDataExtractor = housingJsonDataExtractor;
             _housingData = housingData;
            _housingMakelaarData = housingMakelaarData;
        }
       
        [Route("~/koop/{cityName}/{houseType}")]
        // GET: koop/Amsterdam/tuin
        public IHttpActionResult GetHousing(string cityName, string houseType)
        {
            _housingJsonDataExtractor.ExtractJsonToUpdateDatabase();

            var result = _housingData.GetHousingDetails(cityName, houseType);

            return Ok(result);
        }
       
        [Route("~/koop")]
        // GET: /Koop/
        public IHttpActionResult GetHousesOnSale()
        {
            _housingJsonDataExtractor.ExtractJsonToUpdateDatabase();

            var result = _housingMakelaarData.CalculateTopSellingMakelaarData();

            return Ok(result);
        }
    }
}
