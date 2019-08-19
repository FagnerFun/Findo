using System;
using System.Collections.Generic;
using System.Text;

namespace Findo.Framework.Http.Proxies
{
    /// <summary>
    /// Classe responsável por gerenciar requisições Synchronous
    /// </summary>
    public static partial class GenericProxies
    {
        /// <summary>
        /// Método responsável por fazer requisições GET 
        /// </summary>
        /// <typeparam name="TR">Tipo de objeto a ser retornado</typeparam>
        /// <param name="urlMethod">URL do metodo na API</param>
        /// <param name="configuration">Configurações da requisição</param>
        /// <returns>Retorna resultado da requisição </returns>
        public static TR RestGet<TR>(string urlMethod, ClientConfiguration configuration, string accessToken = null)
        {

            var clientConfig = configuration ?? DefaultConfiguration;
            var request = CreateRequest(urlMethod, clientConfig, accessToken);
            request.Accept = clientConfig.Accept;
            request.Method = "GET";
            return ReceiveData<TR>(request, clientConfig);
        }

        /// <summary>
        /// Método responsável por fazer requisições GET usando DefaultConfiguration
        /// </summary>
        /// <typeparam name="TR">Tipo de objeto a ser retornado</typeparam>
        /// <param name="url">URL do metodo na API</param>
        /// <returns>Retorna resultado da requisição </returns>
        public static TR RestGet<TR>(string url, string accessToken = null)
        {
            return RestGet<TR>(url, DefaultConfiguration, accessToken);
        }


        /// <summary>
        /// Método responsável por fazer requisições RestGetNonQuery
        /// </summary>
        /// <param name="urlMethod">URL do metodo na API</param>
        /// <param name="configuration">Configurações da requisição</param>
        public static void RestGetNonQuery(string urlMethod, ClientConfiguration configuration)
        {
            var clientConfig = configuration ?? DefaultConfiguration;
            var request = CreateRequest(urlMethod, clientConfig);
            request.Method = "GET";

            request.GetResponse().Close();
        }

        /// <summary>
        /// Método responsável por fazer requisições RestGetNonQuery usando DefaultConfiguration
        /// </summary>
        /// <param name="url">URL da API</param>
        public static void RestGetNonQuery(string url)
        {
            RestGetNonQuery(url, DefaultConfiguration);
        }

        /// <summary>
        /// Método responsável por fazer requisições POST 
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto para retorno (POCO)</typeparam>
        /// <typeparam name="TI">Tipo de Objeto local</typeparam>
        /// <param name="urlMethod">URL da API</param>
        /// <param name="data">Dados para requisição</param>
        /// <param name="configuration">Configurações da requisição</param>
        /// <returns>Retorno da Requisição</returns>
        public static TR RestPost<TR, TI>(string urlMethod, TI data, ClientConfiguration configuration)
        {
            var clientConfig = configuration ?? DefaultConfiguration;
            var request = CreateRequest(urlMethod, clientConfig);
            request.ContentType = clientConfig.ContentType;
            request.Accept = clientConfig.Accept;
            request.Method = "POST";

            PostData(request, clientConfig, data);
            return ReceiveData<TR>(request, clientConfig);
        }

        /// <summary>
        /// Método responsável por fazer requisições POST usando DefaultConfiguration
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto para retorno (POCO)</typeparam>
        /// <typeparam name="TI">Tipo de Objeto local</typeparam>
        /// <param name="url">URL da API</param>
        /// <param name="data">Dados para requisição</param>
        /// <returns>Retorno da Requisição</returns>
        public static TR RestPost<TR, TI>(string url, TI data)
        {
            return RestPost<TR, TI>(url, data, DefaultConfiguration);
        }

        /// <summary>
        /// Método responsável por fazer requisições POST
        /// </summary>
        /// <typeparam name="TI">Tipo de Objeto local</typeparam>
        /// <param name="configuration">Configurações da requisição</param>
        /// <param name="urlMethod">URL do metodo na API</param>
        /// <param name="data">Dados para requisição</param>
        /// <returns>Retorno da Requisição</returns>
        public static void RestPostNonQuery<TI>(string urlMethod, TI data, ClientConfiguration configuration)
        {
            var clientConfig = configuration ?? DefaultConfiguration;
            var request = CreateRequest(urlMethod, clientConfig);
            request.ContentType = clientConfig.ContentType;
            request.Accept = clientConfig.Accept;
            request.Method = "POST";

            PostData(request, clientConfig, data);
            request.GetResponse().Close();
        }

        /// <summary>
        /// Método responsável por fazer requisições POST usando DefaultConfiguration
        /// </summary>
        /// <typeparam name="TI">Tipo de Objeto local</typeparam>
        /// <param name="url">URL do metodo na API</param>
        /// <param name="data">Dados para requisição</param>
        /// <returns>Retorno da Requisição</returns>
        public static void RestPostNonQuery<TI>(string url, TI data)
        {
            RestPostNonQuery(url, data, DefaultConfiguration);
        }

        /// <summary>
        /// Métodos resposnsavel por fazer requisições da API
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto para retorno (POCO)</typeparam>
        /// <typeparam name="TI">Tipo de Objeto local</typeparam>
        /// <param name="urlMethod">URL do método na API </param>
        /// <param name="data">Dados para requisição</param>
        /// <param name="method"> Tipo do Método: "GET,POST,PUT,DELETE"</param>
        /// <param name="configuration">Configurações da requisição</param>
        /// <returns>Retorno da requisição </returns>
        public static TR Rest<TR, TI>(string urlMethod, TI data, string method, ClientConfiguration configuration, string token = null)
        {
            var clientConfig = configuration ?? DefaultConfiguration;
            var request = CreateRequest(urlMethod, clientConfig, token);
            request.ContentType = clientConfig.ContentType;
            request.Accept = clientConfig.Accept;
            request.Method = method.ToUpper();

            PostData(request, clientConfig, data);
            return ReceiveData<TR>(request, clientConfig);
        }
        /// <summary>
        /// Métodos resposnsavel por fazer requisições da API usando DefaultConfiguration
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto para retorno (POCO)</typeparam>
        /// <typeparam name="TI">Tipo de Objeto local</typeparam>
        /// <param name="url">URL do método na API </param>
        /// <param name="data">Dados para requisição</param>
        /// <param name="method"> Tipo do Método: "GET,POST,PUT,DELETE"</param>
        /// <returns>Retorno da requisição </returns>
        public static TR Rest<TR, TI>(string url, TI data, string method, string token = null)
        {
            return Rest<TR, TI>(url, data, method, DefaultConfiguration, token);
        }
        /// <summary>
        /// Métodos resposnsavel por fazer requisições da API
        /// </summary>
        /// <typeparam name="TI">Tipo de Objeto local</typeparam>
        /// <param name="urlMethod">URL do método na API </param>
        /// <param name="data">Dados para requisição</param>
        /// <param name="method"> Tipo do Método: "GET,POST,PUT,DELETE"</param>
        /// <param name="configuration">Configurações da requisição</param>
        public static void RestNonQuery<TI>(string urlMethod, TI data, string method, ClientConfiguration configuration)
        {
            var clientConfig = configuration ?? DefaultConfiguration;
            var request = CreateRequest(urlMethod, clientConfig);
            request.ContentType = clientConfig.ContentType;
            request.Accept = clientConfig.Accept;
            request.Method = method.ToUpper();

            PostData(request, clientConfig, data);
            request.GetResponse().Close();
        }
        /// <summary>
        /// Métodos resposnsavel por fazer requisições da API usando DefaultConfiguration
        /// </summary>
        /// <param name="url">URL do método na API </param>
        /// <param name="data">Dados para requisição</param>
        /// <param name="method"> Tipo do Método: "GET,POST,PUT,DELETE"</param>
        public static void RestNonQuery<TI>(string url, TI data, string method)
        {
            RestNonQuery(url, data, method, DefaultConfiguration);
        }

    }
}
