using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using Games_Func.Domain.DataAccess;
using Games_Func.Domain.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Games_Func.UI
{
    public static class GamesFunc
    {
        [Function("Salvar")]
        public static HttpResponseData Salvar([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)            
        {
            try
            {

                var repo = new RecordRepository();
                string conteudo = new StreamReader(req.Body).ReadToEnd();
                repo.Salvar(JsonSerializer.Deserialize<Record>(conteudo));

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");

                return response;

            }
            catch (System.Exception ex)
            {

                HttpResponseData response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.WriteString(ex.Message);
                return response;
            }
        }

        [Function("Excluir/{id}")]
        public static HttpResponseData Excluir([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, int id)
        {
            try
            {

                var repo = new RecordRepository();               
                repo.Remover(id);

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");

                return response;

            }
            catch (System.Exception ex)
            {

                HttpResponseData response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.WriteString(ex.Message);
                return response;
            }
        }

        [Function("Limpar/{game}")]
        public static HttpResponseData Limpar([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, string game)
        {
            try
            {

                var repo = new RecordRepository();
                repo.Limpar(game);

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");

                return response;

            }
            catch (System.Exception ex)
            {

                HttpResponseData response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.WriteString(ex.Message);
                return response;
            }
        }

        [Function("ObterMin/{game}")]
        public static HttpResponseData ObterMin([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, string game)
        {
            try
            {

                var repo = new RecordRepository();
               int min = repo.ObterMin(game);

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");

                response.WriteString(min.ToString());

                return response;

            }
            catch (System.Exception ex)
            {

                HttpResponseData response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.WriteString(ex.Message);
                return response;
            }
        }

        [Function("ObterPor/{id}")]
        public static HttpResponseData ObterPor([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, int id)
        {
            try
            {

                var repo = new RecordRepository();
                string conteudo = JsonSerializer.Serialize<Record>(repo.ObterPor(id));

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");

                response.WriteString(conteudo);

                return response;
            }
            catch (System.Exception ex)
            {

                HttpResponseData response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.WriteString(ex.Message);
                return response;
            }
        }

        [Function("ObterTodos/{game}")]
        public static HttpResponseData ObterTodos([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, string game)
        {
            try
            {

                var repo = new RecordRepository();
                string conteudo = JsonSerializer.Serialize<List<Record>>(repo.ObterTodos(game));

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "application/json");

                response.WriteString(conteudo);

                return response;

            }
            catch (System.Exception ex)
            {

                HttpResponseData response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.WriteString(ex.Message);
                return response;
            }
        }
    }
}
