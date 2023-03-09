using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Dtos
{
    public class NetworkDto
    {
        public int Id{get;set;}
        public string Name{get; set;}
        public IList<CardDto> Cards{get; set;}
        public IList<CategoryDto> Categories{get; set;}
    }

    public class CreateNetworkRequestModel
    {
        public string Name{get; set;}
    }

    public class UpdateNetworkRequestModel
    {
        public string Name{get; set;}
    }
}