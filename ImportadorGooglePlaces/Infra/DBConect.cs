using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Import_Google_Places.DAL
{
    /**
    * Class:    	DbConect
    * Description:  Classe responsável por acessar banco de dados
    * Author:    	Luiz F. Amaral 
    * Data:			02/12/2020
    * Website:  	https://luizamaral.dev.br
    * Contact:  	luizfer.amaral@gmail.com
    */
    public class DbConect
    {

        static NpgsqlConnection PgSqlConnection = null;
        static string connString = null;

        /**
        * Method:    	DbConect
        * Description:  Construtor da classe. Realiza obtenção das configurações
        * Author:    	Luiz F. Amaral 
        * Data:			02/12/2020
        * Website:  	https://luizamaral.dev.br
        * Contact:  	luizfer.amaral@gmail.com
        */
        public DbConect()
        {

            IConfigurationSection config = new ConfigurationBuilder()
                                                .AddJsonFile("appsettings.json")
                                                .Build()
                                                .GetSection("ConnectionStrings");

            string serverName   = config["serverName"];       //localhost
            string port         = config["port"];             //porta default
            string userName     = config["userName"];         //nome do administrador
            string password     = config["password"];         //senha do administrador
            string databaseName = config["databaseName"];     //nome do banco de dados

            connString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                                        serverName, port, userName, password, databaseName);

            PgSqlConnection = new NpgsqlConnection(connString);
        }

        public static NpgsqlConnection GetConection()
        {
            return PgSqlConnection;
        }

    }
}
