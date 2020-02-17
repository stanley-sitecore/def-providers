﻿using System;
using System.Web;
using Newtonsoft.Json.Linq;
using Sitecore.DataExchange.DataAccess;

namespace GC.DataExchange.Providers.Json.Readers
{
    public class JsonValueReader : IValueReader
    {
        public string JsonPath { get; private set; }

        public JsonValueReader(string jsonPath)
        {
            if (string.IsNullOrWhiteSpace(jsonPath))
                throw new ArgumentOutOfRangeException(nameof(jsonPath), (object)jsonPath, "JSON path must be specified.");
            this.JsonPath = jsonPath;
        }

        public ReadResult Read(object source, DataAccessContext context)
        {
            if (!(source is JObject jsonObject)) return ReadResult.NegativeResult(DateTime.UtcNow);

            var result = ReadResult.NegativeResult(DateTime.UtcNow);

            var token = jsonObject.SelectToken(this.JsonPath);

            if (token != null)
            {
                result = ReadResult.PositiveResult(HttpUtility.HtmlDecode(token.ToString()), DateTime.UtcNow);
            }

            if (token is JArray tokenArray)
            {
                result = ReadResult.PositiveResult(tokenArray.ToObject<string[]>(), DateTime.UtcNow);
            }

            return result;
        }
    }
}
