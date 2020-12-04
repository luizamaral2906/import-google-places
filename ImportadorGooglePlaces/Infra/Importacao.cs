using Import_Google_Places.DAL;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Import_Google_Places.Utils;
using System.Collections.Immutable;
using Microsoft.Extensions.Configuration;
using APIImportadorGooglePlaces.Model;

namespace Import_Google_Places.EstruturaJson
{

    /**
    * Class:    	Importacao
    * Description:  Classe responsável por realizar o processo de importação das informações
    * Author:    	Luiz F. Amaral 
    * Data:			02/12/2020
    * Website:  	https://luizamaral.dev.br
    * Contact:  	luizfer.amaral@gmail.com
    */
    public class Importacao
    {

        int ID_DOMINGO = 1;
        int ID_SEGUNDA = 2;
        int ID_TERCA = 3;
        int ID_QUARTA = 4;
        int ID_QUINTA = 5;
        int ID_SEXTA = 6;
        int ID_SABADO = 7;

        /**
        * Method:    	IniciaImportacao
        * Description:  Inicia processo de importação das informações
        * Author:    	Luiz F. Amaral 
        * Data:			02/12/2020
        * Website:  	https://luizamaral.dev.br
        * Contact:  	luizfer.amaral@gmail.com
        */
        public void IniciaImportacao()
        {
            new DbConect();

            Places Lugares = LoadJson();
            Console.WriteLine(Lugares.ListaPlaces.Count);

            int contador = 0;
            foreach (var item in Lugares.ListaPlaces)
            {
                contador++;
                SalvarLocal(item, contador);
            }

            Console.WriteLine("Processo de importação finalizada");


        }

        /**
        * Method:    	LoadJson
        * Description:  Carrega arquivo para ser importado
        * Author:    	Luiz F. Amaral 
        * Data:			02/12/2020
        * Website:  	https://luizamaral.dev.br
        * Contact:  	luizfer.amaral@gmail.com
        */
        public Places LoadJson()
        {
            String NomeArquivoImportar = new ConfigurationBuilder()
                             .AddJsonFile("appsettings.json")
                            .Build()
                            .GetSection("Importacao")["nomeArquivo"];

            Places places = new Places();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            using (StreamReader r = new StreamReader(NomeArquivoImportar))
            {
                string json = r.ReadToEnd();

                places.ListaPlaces = JsonConvert.DeserializeObject<List<Place>>(json, settings);
            }

            return places;
        }

        /**
        * Method:    	SalvarLocal
        * Description:  Salva no banco de dados as informações dos Locais da pesquisa(Places)
        * Author:    	Luiz F. Amaral 
        * Data:			02/12/2020
        * Website:  	https://luizamaral.dev.br
        * Contact:  	luizfer.amaral@gmail.com
        */
        public void SalvarLocal(Place local, int count)
        {

            NpgsqlConnection pgsqlConnection = DbConect.GetConection();
            //Abra a conexão com o PgSQL                  
            pgsqlConnection.Open();

            String strSQL = "";
            NpgsqlCommand cmd = null;

            try
            {

                strSQL = " Insert into Local                                          " +
                    " (	titulo,                                                 " +
                    " 	pontuacaoTotal,                                              " +
                    " 	categoriaNome,                                            " +
                    " 	endereco,                                                 " +
                    " 	plusCode,                                                " +
                    " 	website,                                                 " +
                    " 	telefone,                                                   " +
                    " 	temporarioFechado,                                       " +
                    "   fechado,                                       " +
                    " 	ranking,                                                 " +
                    "   placeId,                                                 " +
                    "   url,                                                     " +
                    " 	latitude,                                                " +
                    " 	longitude,                                               " +
                    "   searchString,                                            " +
                    "   HorarioPopularesTxt,                                    " +
                    " 	contagemReviews)                                            " +
                    " Values(@param2, @param3, @param4, @param5, @param6, " +
                    " @param7, @param8, @param9, @param10, @param11, @param12, " +
                    " @param13, @param14, @param15, @param16, @param17, @param18) " +
                    " RETURNING idLocal ";

                cmd = new NpgsqlCommand(strSQL, pgsqlConnection);

                cmd.Parameters.AddWithValue("@param2", Utils.Utils.SafeDbObject(local.title));
                cmd.Parameters.AddWithValue("@param3", Utils.Utils.SafeDbObject(local.totalScore));
                cmd.Parameters.AddWithValue("@param4", Utils.Utils.SafeDbObject(local.categoryName));
                cmd.Parameters.AddWithValue("@param5", Utils.Utils.SafeDbObject(local.address));
                cmd.Parameters.AddWithValue("@param6", Utils.Utils.SafeDbObject(local.plusCode));
                cmd.Parameters.AddWithValue("@param7", Utils.Utils.SafeDbObject(local.website));
                cmd.Parameters.AddWithValue("@param8", Utils.Utils.SafeDbObject(local.phone));
                cmd.Parameters.AddWithValue("@param9", Utils.Utils.SafeDbObject(local.temporarilyClosed));
                cmd.Parameters.AddWithValue("@param10", Utils.Utils.SafeDbObject(local.permanentlyClosed));
                cmd.Parameters.AddWithValue("@param11", Utils.Utils.SafeDbObject(local.rank));
                cmd.Parameters.AddWithValue("@param12", Utils.Utils.SafeDbObject(local.placeId));
                cmd.Parameters.AddWithValue("@param13", Utils.Utils.SafeDbObject(local.url));
                cmd.Parameters.AddWithValue("@param14", Utils.Utils.SafeDbObject(local.location.lat));
                cmd.Parameters.AddWithValue("@param15", Utils.Utils.SafeDbObject(local.location.lng));
                cmd.Parameters.AddWithValue("@param16", Utils.Utils.SafeDbObject(local.searchString));
                cmd.Parameters.AddWithValue("@param17", Utils.Utils.SafeDbObject(local.popularTimesLivePercent));
                cmd.Parameters.AddWithValue("@param18", Utils.Utils.SafeDbObject(local.reviewsCount));

                Int32 idLocal = Convert.ToInt32(cmd.ExecuteScalar());

                pgsqlConnection.Close();

                SalvarHorariosSemanais(local.openingHours, idLocal);

                SalvarHorariosHistogram(local.popularTimesHistogram, idLocal);

                SalvarReviewsUsuarios(local.reviews, idLocal);

                SalvarImagensURLs(local.imageUrls, idLocal);


            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(count);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /**
        * Method:    	SalvarHorariosSemanais
        * Description:  Salva no banco de dados as informações dos horarios semanais
        * Author:    	Luiz F. Amaral 
        * Data:			02/12/2020
        * Website:  	https://luizamaral.dev.br
        * Contact:  	luizfer.amaral@gmail.com
        */
        public void SalvarHorariosSemanais(List<OpeningHour> horarioFuncionamento, Int32 idLocal)
        {
            if (horarioFuncionamento == null) return;

            NpgsqlConnection pgsqlConnection = DbConect.GetConection();
            //Abra a conexão com o PgSQL                  
            pgsqlConnection.Open();

            try
            {

                foreach (var item in horarioFuncionamento)
                {
                    int idDia = 0;

                    if (item.day.Equals("Sunday,")) idDia = ID_DOMINGO;
                    else if (item.day.Equals("Monday,")) idDia = ID_SEGUNDA;
                    else if (item.day.Equals("Tuesday,")) idDia = ID_TERCA;
                    else if (item.day.Equals("Wednesday,")) idDia = ID_QUARTA;
                    else if (item.day.Equals("Thursday,")) idDia = ID_QUINTA;
                    else if (item.day.Equals("Friday,")) idDia = ID_SEXTA;
                    else if (item.day.Equals("Saturday,")) idDia = ID_SABADO;

                    if (idDia == 0)
                    {
                        Console.WriteLine("Erro ao importar horários de funcionamento");
                        continue;
                    }

                    String strSQL = " Insert into HorariosFuncionamento             " +
                            " (	idLocal,                                            " +
                            " 	idDiaSemana,                                        " +
                            " 	intervaloDesc )                                     " +
                            " Values(@param1, @param2, @param3) ";

                    NpgsqlCommand cmd = new NpgsqlCommand(strSQL, pgsqlConnection);

                    cmd.Parameters.AddWithValue("@param1", idLocal);
                    cmd.Parameters.AddWithValue("@param2", idDia);
                    cmd.Parameters.AddWithValue("@param3", Utils.Utils.SafeDbObject(item.hours));


                    cmd.ExecuteNonQuery();

                }

            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }

        /**
        * Method:    	SalvarHorariosHistogram
        * Description:  Salva no banco de dados as informações de  histogram dos horarios de funcionamento do locais
        * Author:    	Luiz F. Amaral 
        * Data:			02/12/2020
        * Website:  	https://luizamaral.dev.br
        * Contact:  	luizfer.amaral@gmail.com
        */
        public void SalvarHorariosHistogram(PopularTimesHistogram horarioHistogram, Int32 idLocal)
        {
            if (horarioHistogram == null) return;

            try
            {
                foreach (var item in horarioHistogram.Su)
                    SalvarHorariosHistogramItem(idLocal, ID_DOMINGO, item.hour, item.occupancyPercent);

                foreach (var item in horarioHistogram.Mo)
                    SalvarHorariosHistogramItem(idLocal, ID_SEGUNDA, item.hour, item.occupancyPercent);

                foreach (var item in horarioHistogram.Tu)
                    SalvarHorariosHistogramItem(idLocal, ID_TERCA, item.hour, item.occupancyPercent);

                foreach (var item in horarioHistogram.We)
                    SalvarHorariosHistogramItem(idLocal, ID_QUARTA, item.hour, item.occupancyPercent);

                foreach (var item in horarioHistogram.Th)
                    SalvarHorariosHistogramItem(idLocal, ID_QUINTA, item.hour, item.occupancyPercent);

                foreach (var item in horarioHistogram.Fr)
                    SalvarHorariosHistogramItem(idLocal, ID_SEXTA, item.hour, item.occupancyPercent);

                foreach (var item in horarioHistogram.Sa)
                    SalvarHorariosHistogramItem(idLocal, ID_SABADO, item.hour, item.occupancyPercent);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /**
        * Method:    	SalvarHorariosHistogramItem
        * Description:  Salva no banco de dados um horario de funcionamento do local
        * Author:    	Luiz F. Amaral 
        * Data:			02/12/2020
        * Website:  	https://luizamaral.dev.br
        * Contact:  	luizfer.amaral@gmail.com
        */
        public void SalvarHorariosHistogramItem(Int32 idLocal, int diaSemana, int hora, int taxa)
        {
            NpgsqlConnection pgsqlConnection = DbConect.GetConection();
            //Abra a conexão com o PgSQL                  
            pgsqlConnection.Open();

            try
            {

                String strSQL = " Insert into HorariosHistoricoHistogram        " +
                        " (	idLocal,                                            " +
                        " 	idDiaSemana,                                        " +
                        " 	hora,                                               " +
                        " 	taxaOcupacao)                                       " +
                        " Values(@param1, @param2, @param3, @param4)            ";

                NpgsqlCommand cmd = new NpgsqlCommand(strSQL, pgsqlConnection);

                cmd.Parameters.AddWithValue("@param1", Utils.Utils.SafeDbObject(idLocal));
                cmd.Parameters.AddWithValue("@param2", Utils.Utils.SafeDbObject(diaSemana));
                cmd.Parameters.AddWithValue("@param3", Utils.Utils.SafeDbObject(hora));
                cmd.Parameters.AddWithValue("@param4", Utils.Utils.SafeDbObject(taxa));


                cmd.ExecuteNonQuery();

            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }

        /**
        * Method:    	SalvarReviewsUsuarios
        * Description:  Salva no banco de dados as revisões/comentarios dos usuários sobre o local
        * Author:    	Luiz F. Amaral 
        * Data:			02/12/2020
        * Website:  	https://luizamaral.dev.br
        * Contact:  	luizfer.amaral@gmail.com
        */
        public void SalvarReviewsUsuarios(List<Review> listaReview, Int32 idLocal)
        {
            if (listaReview == null) return;

            NpgsqlConnection pgsqlConnection = DbConect.GetConection();
            //Abra a conexão com o PgSQL                  
            pgsqlConnection.Open();

            try
            {

                foreach (var item in listaReview)
                {

                    String strSQL = "  Insert into reviewsusuarios " +
                    " ( " +
                    "    idlocal,  " +
                    "    idreview,  " +
                    "    tempopublicacaodataimport, " +
                    "    dataimport, " +
                    "    url, " +
                    "    likes, " +
                    "    pontuacao, " +
                    "    nome, " +
                    "    mensagem, " +
                    "    reviewerid, " +
                    "    reviewerurl, " +
                    "    reviewernumberofreviews,  " +
                    "    islocalguide " +
                    " ) " +
                    " Values(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, " +
                    " @param9, @param10, @param11, @param12, @param13) ";

                    NpgsqlCommand cmd = new NpgsqlCommand(strSQL, pgsqlConnection);

                    cmd.Parameters.AddWithValue("@param1", Utils.Utils.SafeDbObject(idLocal));
                    cmd.Parameters.AddWithValue("@param2", Utils.Utils.SafeDbObject(item.reviewId));
                    cmd.Parameters.AddWithValue("@param3", Utils.Utils.SafeDbObject(item.publishAt));
                    cmd.Parameters.AddWithValue("@param4", DateTime.Now);
                    cmd.Parameters.AddWithValue("@param5", Utils.Utils.SafeDbObject(item.reviewUrl));
                    cmd.Parameters.AddWithValue("@param6", Utils.Utils.SafeDbObject(item.likesCount));
                    cmd.Parameters.AddWithValue("@param7", Utils.Utils.SafeDbObject(item.stars));
                    cmd.Parameters.AddWithValue("@param8", Utils.Utils.SafeDbObject(item.name));
                    cmd.Parameters.AddWithValue("@param9", Utils.Utils.SafeDbObject(item.text));
                    cmd.Parameters.AddWithValue("@param10", Utils.Utils.SafeDbObject(item.reviewerId));
                    cmd.Parameters.AddWithValue("@param11", Utils.Utils.SafeDbObject(item.reviewerUrl));
                    cmd.Parameters.AddWithValue("@param12", Utils.Utils.SafeDbObject(item.reviewerNumberOfReviews));
                    cmd.Parameters.AddWithValue("@param13", Utils.Utils.SafeDbObject(item.isLocalGuide));

                    cmd.ExecuteNonQuery();

                }

            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }

        /**
        * Method:    	SalvarImagensURLs
        * Description:  Salva no banco de dados as URLs de imagens/fotos dos locais
        * Author:    	Luiz F. Amaral 
        * Data:			02/12/2020
        * Website:  	https://luizamaral.dev.br
        * Contact:  	luizfer.amaral@gmail.com
        */
        public void SalvarImagensURLs(List<String> imagens, Int32 idLocal)
        {

            if (imagens == null) return;

            NpgsqlConnection pgsqlConnection = DbConect.GetConection();
            //Abra a conexão com o PgSQL                  
            pgsqlConnection.Open();

            try
            {

                foreach (var url in imagens)
                {

                    String strSQL = " Insert into ImagemURL         " +
                            " (	idLocal,                            " +
                            " 	url    )                             " +
                            " Values(@param1, @param2)              ";

                    NpgsqlCommand cmd = new NpgsqlCommand(strSQL, pgsqlConnection);

                    cmd.Parameters.AddWithValue("@param1", Utils.Utils.SafeDbObject(idLocal));
                    cmd.Parameters.AddWithValue("@param2", Utils.Utils.SafeDbObject(url));


                    cmd.ExecuteNonQuery();
                }

            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                pgsqlConnection.Close();
            }
        }

    }
}
