using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.DataSchemas;
using Client.Models;

namespace Client.Service
{
    public interface IDataService
    {
        public void FetchRecord(List<UmData> outputDatas,ImgwData imgwData,string zachmurzenie,List<Record> record,int fetchCount);


    }
}