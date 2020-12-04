using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIImportadorGooglePlaces.Model
{
    /**
    * Class:    	OpeningHour
    * Description:  Classe responsável por armazenar os horários de funcionamento do local
    * Author:    	Luiz F. Amaral 
    * Data:			02/12/2020
    * Website:  	https://luizamaral.dev.br
    * Contact:  	luizfer.amaral@gmail.com
    */
    public class OpeningHour
    {
        public string day { get; set; }
        public string hours { get; set; }
    }
}
