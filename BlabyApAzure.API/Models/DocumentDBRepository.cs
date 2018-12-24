
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blabyap.Common.Model;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
namespace BlabyApAzure.API.Models
{
    public static class DocumentDBRepository<T> where T : class
    {
        private static readonly string DatabaseId = ConfigurationManager.AppSettings["DocumentDbName"];
        private static readonly string CollectionId = ConfigurationManager.AppSettings["DocumentDbCollectionName"];
        private static DocumentClient client;
        public static async Task<T> GetDataAsync(string id)
        {
            try
            {
              Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
                return (T)(dynamic)document; 
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }
        public static async Task<T> GetDataSqlAsync(string sql)
        {
            try
            {
                var queryOptions = new FeedOptions { MaxItemCount = -1 };
                var link = UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);
                Trace.TraceInformation($"Running query: {sql}");
                var dquery = client.CreateDocumentQuery<T>(link, sql, queryOptions)
                         .AsDocumentQuery();
                  List<T> results = new List<T>();
                while (dquery.HasMoreResults)
                {
                    results.AddRange(await dquery.ExecuteNextAsync<T>());
                }
                  return (T)(dynamic)results.FirstOrDefault();
                 
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }
        public static async Task<IEnumerable<T>> GetDatasAsync(Expression<Func<T, bool>> predicate=null, Expression<Func<T, bool>> predicate2 = null)
        {
            IDocumentQuery<T> query = null;
            if (predicate != null)
            {
                query = client.CreateDocumentQuery<T>(
               UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
               new FeedOptions { MaxItemCount = -1 })
               .Where(predicate)
               .AsDocumentQuery();
            }
            else
            {
               query =client.CreateDocumentQuery<T>(
              UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
              new FeedOptions { MaxItemCount = -1 })
              .AsDocumentQuery();
            }

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }
        public static async Task<IEnumerable<T>> GetDataCount(string specificFields = null,string filter ="",string orderBy = null)
        {
            if (specificFields == null)
                specificFields = "*";
            var queryOptions = new FeedOptions { MaxItemCount = -1 }; 
            var link = UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);
            var query = $"SELECT {specificFields} FROM {CollectionId} a WHERE {filter}";
            if (!string.IsNullOrEmpty(orderBy))
                query += $" ORDER BY {orderBy}";
            Trace.TraceInformation($"Running query: {query}");
           var dquery =   client.CreateDocumentQuery<T>(link, query, queryOptions)
                    .AsDocumentQuery();
             var page = await dquery.ExecuteNextAsync<T>();
            var queryResult = page.ToList();
            List<T> results = new List<T>();
            while (dquery.HasMoreResults)
            {
                results.AddRange(await dquery.ExecuteNextAsync<T>());
            }
            return results;
        }

        public static async Task<IEnumerable<T>> QueryAndContinue(
     int curPage,
    int PageSize,
    Expression<Func<T, IEnumerable<T>>> predicate = null,
    Expression<Func<T, IEnumerable<T>>> predicate1 = null,
    string specificFields = null,
    string orderBy = null,
    string partitionKey = null) 
        {
            try
            {
                if (specificFields == null)
                    specificFields = "*";

                var queryOptions = new FeedOptions { MaxItemCount = PageSize, EnableCrossPartitionQuery = true };

                IDocumentQuery<T> query = null;

                //if (!string.IsNullOrEmpty(continuationToken))
                //queryOptions.RequestContinuation = continuationToken;

                string continuationToken = "";
               int iResCur = 0;
                List<T> results = new List<T>();
                do
                {
                    ++iResCur;
                    if (predicate != null)
                    {
                        query = client.CreateDocumentQuery<T>(
                       UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                       new FeedOptions { MaxItemCount = PageSize, RequestContinuation = continuationToken }) 
                       .SelectMany(predicate)
                       
                .AsDocumentQuery(); 
                    }
                    else
                    {
                        query = client.CreateDocumentQuery<T>(
                       UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                       new FeedOptions { MaxItemCount = PageSize })
                       .AsDocumentQuery();
                    }
                    
                    var page = await query.ExecuteNextAsync<T>();
                    results = page.ToList();
                    //var queryResult = page.ToList();
                    if (query.HasMoreResults)
                    {
                        continuationToken = page.ResponseContinuation;
                    }
                    else
                        break;

                }
                while (iResCur < curPage);

                if (iResCur < curPage)
                    results = null; 
                return results;
            }
            catch (Exception)
            {
                return null;
            }     
        }
        public static async Task<IEnumerable<T>> QueryAndContinue(
     int curPage,
    int PageSize,
    string squery,
    Expression<Func<T, IEnumerable<T>>> predicate1 = null,
    string specificFields = null,
    string orderBy = null,
    string partitionKey = null)
        {
            try
            {

                var queryOptions = new FeedOptions { MaxItemCount = PageSize, EnableCrossPartitionQuery = true };

                var link = UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId);

                IDocumentQuery<T> query = null;

                int iResCur = 0;
                List<T> results = new List<T>();
                string continuationToken = "";

                do
                {
                    ++iResCur;
                    if (squery != "")
                    {
                        var dquery = client.CreateDocumentQuery<T>(link, squery, queryOptions)
                            .AsDocumentQuery();

                        query = client.CreateDocumentQuery<T>(
                       UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                       new FeedOptions { MaxItemCount = PageSize, RequestContinuation = continuationToken })
                    .AsDocumentQuery();
                    }
                    else
                    {
                        query = client.CreateDocumentQuery<T>(
                       UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                       new FeedOptions { MaxItemCount = PageSize })
                       .AsDocumentQuery();
                    }

                    var page = await query.ExecuteNextAsync<T>();
                    results = page.ToList();
                    //var queryResult = page.ToList();
                    if (query.HasMoreResults)
                    {
                        continuationToken = page.ResponseContinuation;
                    }
                    else
                        break;
                }
                while (iResCur < curPage);

                if (iResCur < curPage)
                    results = null;

                return results;

            }
            catch (Exception)
            {
                return null;
            }

        }

        public static async Task<IEnumerable<T>> SelectMany(Expression<Func<T, IEnumerable<T>>> predicate)
        {
            IEnumerable<T> result = new List<T>();
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                new FeedOptions { MaxItemCount = -1 }).SelectMany(predicate)
                .AsDocumentQuery();
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        //public static async Task<Document> GetDocIds()
        //{
        //    IEnumerable<T> result = new List<T>();
        //    IDocumentQuery<T> query = client.CreateDocumentQuery<SalesOrder>(collectionLink)
        //            //                        .Where(so => so.AccountNumber == "10-4020-000510")
        //            //                        .AsEnumerable()
        //            //                        .FirstOrDefault();

        //    IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
        //        UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId)).Where().AsEnumerable()
        //            //                        .FirstOrDefault();
       //    List<T> results = new List<T>();
        //    while (query.HasMoreResults)
        //    {
        //        results.AddRange(await query.ExecuteNextAsync<T>());
        //    }

        //    return results;

        //}
        public static async Task<Document> CreateDataAsync(T Data)
        {
            return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), Data);
        }

        public static async Task<Document> UpdateDataAsync(string id, T Data)
        {
           
            return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id), Data);
        }
        public static async Task<Document> UpsertDataAsync( T Data)
        {
            return await client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), Data);
        }

        public static async Task DeleteDataAsync(string id)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
        }

        public static void Initialize()
        {
            client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["DocumentDbEndPoint"]), ConfigurationManager.AppSettings["DocumentDbAuthKey"]);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        private static async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private static async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}