using Import_Google_Places.EstruturaJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIImportadorGooglePlaces.Model
{
    /**
    * Class:    	Places
    * Description:  Classe responsável por armazenar a lista de Locais com as informações
    * Author:    	Luiz F. Amaral 
    * Data:			02/12/2020
    * Website:  	https://luizamaral.dev.br
    * Contact:  	luizfer.amaral@gmail.com
    */
    public class Places
    {
        // Lista dos locais obtidos a partir do arquivo
        public List<Place> ListaPlaces { get; set; }
    }
}
