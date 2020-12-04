using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIImportadorGooglePlaces.Model
{
    /**
    * Class:    	PopularTimesHistogram
    * Description:  Classe responsável por armazenar o histogram de funcionamento do local
    * Author:    	Luiz F. Amaral 
    * Data:			02/12/2020
    * Website:  	https://luizamaral.dev.br
    * Contact:  	luizfer.amaral@gmail.com
    */
    public class PopularTimesHistogram
    {
        public List<Su> Su { get; set; }
        public List<Mo> Mo { get; set; }
        public List<Tu> Tu { get; set; }
        public List<We> We { get; set; }
        public List<Th> Th { get; set; }
        public List<Fr> Fr { get; set; }
        public List<Sa> Sa { get; set; }
    }

    public class Su
    {
        public int hour { get; set; }
        public int occupancyPercent { get; set; }
    }

    public class Sa
    {
        public int hour { get; set; }
        public int occupancyPercent { get; set; }
    }
    public class Mo
    {
        public int hour { get; set; }
        public int occupancyPercent { get; set; }
    }

    public class Tu
    {
        public int hour { get; set; }
        public int occupancyPercent { get; set; }
    }

    public class We
    {
        public int hour { get; set; }
        public int occupancyPercent { get; set; }
    }

    public class Th
    {
        public int hour { get; set; }
        public int occupancyPercent { get; set; }
    }

    public class Fr
    {
        public int hour { get; set; }
        public int occupancyPercent { get; set; }
    }



}
