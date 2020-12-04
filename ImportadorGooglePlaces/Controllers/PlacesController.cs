using System;
using System.Collections.Generic;
using APIImportadorGooglePlaces;
using Import_Google_Places.EstruturaJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiServiceOne.Controllers
{
    /**
    * Class:    	PlacesController
    * Description:  Classe responsável por expor metodos para acessos externos
    * Author:    	Luiz F. Amaral 
    * Data:			02/12/2020
    * Website:  	https://luizamaral.dev.br
    * Contact:  	luizfer.amaral@gmail.com
    */
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {

        private readonly ILogger<PlacesController> _logger;

        public PlacesController(ILogger<PlacesController> logger)
        {
            _logger = logger;
        }

        /**
        * Method:    	ObterHostName
        * Description:  Metodo responsável por disponibilizar nome da API e da maquina
        * Author:    	Luiz F. Amaral 
        * Data:			02/12/2020
        * Website:  	https://luizamaral.dev.br
        * Contact:  	luizfer.amaral@gmail.com
        */
        [HttpGet("hostname")]
        public string ObterHostName()
        {
            return Environment.MachineName + " - APIImportadorGooglePlaces " + Program.IdInstancia.ToString();
        }

        /**
        * Method:    	ExecutaImportacaoGooglePlaces
        * Description:  Metodo responsável iniciar processo de importação 
        * Author:    	Luiz F. Amaral 
        * Data:			02/12/2020
        * Website:  	https://luizamaral.dev.br
        * Contact:  	luizfer.amaral@gmail.com
        */
        [HttpGet("ExecutaImportacaoGooglePlaces")]
        public String ExecutaImportacaoGooglePlaces()
        {
            new Importacao().IniciaImportacao();

            return "Importação realizada com sucesso!";
        }

    }
}
