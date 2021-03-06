﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services.Response
{

    [Serializable]
    public class ServiceResponse<T>
    {
        public bool HasExceptionError { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ExceptionMessage { get; set; }

        public IList<T> List { get; set; }

        [JsonProperty]
        public T Entity { get; set; }

        public int Id { get; set; }

        public int Count { get; set; }

        public bool IsValid => !HasExceptionError && string.IsNullOrEmpty(ExceptionMessage);

        public bool IsSuccesful { get; set; }

        public ServiceResponse(HttpContext context)
        {
            List=new List<T>();
        }
    }

    
}
