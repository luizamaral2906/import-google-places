using Import_Google_Places.EstruturaJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIImportadorGooglePlaces.Model
{

    /**
    * Class:    	Place
    * Description:  Classe responsável por armazenar as informações do Local
    * Author:    	Luiz F. Amaral 
    * Data:			02/12/2020
    * Website:  	https://luizamaral.dev.br
    * Contact:  	luizfer.amaral@gmail.com
    */
    public class Place
    {
        public string title { get; set; }
        public double totalScore { get; set; }
        public string categoryName { get; set; }
        public string address { get; set; }
        public object locatedIn { get; set; }
        public string plusCode { get; set; }
        public string website { get; set; }
        public string phone { get; set; }
        public bool temporarilyClosed { get; set; }
        public bool permanentlyClosed { get; set; }
        public int rank { get; set; }
        public string placeId { get; set; }
        public string url { get; set; }
        public Location location { get; set; }
        public string searchString { get; set; }
        public string popularTimesLiveText { get; set; }
        public object popularTimesLivePercent { get; set; }
        public PopularTimesHistogram popularTimesHistogram { get; set; }
        public List<OpeningHour> openingHours { get; set; }
        public List<object> peopleAlsoSearch { get; set; }
        public int reviewsCount { get; set; }
        public List<Review> reviews { get; set; }
        public List<string> imageUrls { get; set; }
    }
}
