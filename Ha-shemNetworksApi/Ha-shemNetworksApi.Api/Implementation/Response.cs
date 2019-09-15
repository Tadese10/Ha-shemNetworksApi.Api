using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ha_shemNetworksApi.Api.Implementation
{
    public class Response<T> where T : class
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}
