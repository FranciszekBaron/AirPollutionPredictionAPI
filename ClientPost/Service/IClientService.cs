using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Service
{
    public interface IClientService
    {
        HttpClient CreateClient(Uri adress);

        Task<List<UmData>> GetUmData(HttpClient client);

        Task<ImgwData> GetImgwData(HttpClient client);

        string GetDataFromHtml();

        string GetValueFromFile(string filename);

        Task<int> FetchRecordsList(List<UmData> outputDatas, ImgwData imgwData, string zachmurzenie,int fetchCount,HttpClient client);
    }
}