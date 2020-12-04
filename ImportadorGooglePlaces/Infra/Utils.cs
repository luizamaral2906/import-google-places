using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Import_Google_Places.Utils
{
    public class Utils
    {

        /**
        * Method:    	SafeDbObject
        * Description:  Verifica se a variavel e nula/vazia
        * Author:    	Luiz F. Amaral 
        * Data:			02/12/2020
        * Website:  	https://luizamaral.dev.br
        * Contact:  	luizfer.amaral@gmail.com
        */
        public static Object SafeDbObject(Object input)
        {
            if (input == null)
            {
                return DBNull.Value;
            }
            else
            {
                return input;
            }
        }
    }
}
