using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digitial_Village_Api.Domain.Persistence.Entities;

namespace Digital_Village_Api.Application.Interface
{
    public interface IProductRepository
    {
        public Task<string> AddProduct( ViProduct vp);
        public Task<string> UpdateProduct(ViProduct vp);
        public Task<string> DeleteProduct(int id);
        public Task<List<ViProduct>> GetAllProduct();
      
    }
}