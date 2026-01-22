using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service
{
    public interface IClientService
    {
        HttpClient CreateClient(Uri adress);

        Task<HttpStatusCode> DeleteData(HttpClient client);
    }
}