using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIImportadorGooglePlaces.Model
{
    /**
    * Class:    	Review
    * Description:  Classe responsável por armazenar a revisão/comentário do usuário
    * Author:    	Luiz F. Amaral 
    * Data:			02/12/2020
    * Website:  	https://luizamaral.dev.br
    * Contact:  	luizfer.amaral@gmail.com
    */
    public class Review
    {
        public string name { get; set; }
        public string text { get; set; }
        public string publishAt { get; set; }
        public object likesCount { get; set; }
        public string reviewId { get; set; }
        public string reviewUrl { get; set; }
        public string reviewerId { get; set; }
        public string reviewerUrl { get; set; }
        public int reviewerNumberOfReviews { get; set; }
        public bool isLocalGuide { get; set; }
        public int stars { get; set; }
    }
}
