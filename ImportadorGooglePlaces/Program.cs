using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Import_Google_Places.EstruturaJson;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace APIImportadorGooglePlaces
{
    /**
    * Class:    	Program
    * Description:  O software tem o objetivo de importar informações de locais do google maps a partir de um arquivo "json"
    * Author:    	Luiz F. Amaral 
    * Data:			02/12/2020
    * Website:  	https://luizamaral.dev.br
    * Contact:  	luizfer.amaral@gmail.com
    */
    public class Program
    {
        public static Guid IdInstancia = Guid.NewGuid();

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
