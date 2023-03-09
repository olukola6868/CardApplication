using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HarnyCardApplication.Dtos
{
    public class CategoryDto
    {
        public int Id{get; set;}
        public string Name{get; set;}
        public double Price{get; set;}
        public IList<CardDto> Cards{get; set;}
        public IList<NetworkDto> Networks{get; set;}
    }

    public class CreateCategoryRequestModel
    {
        public string Name{get; set;}
        public double Price{get; set;}
    }

    public class UpdateCategoryRequestModel
    {
        public string Name{get; set;}
        public double Price{get; set;}
    }
}