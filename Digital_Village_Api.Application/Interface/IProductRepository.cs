using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digital_Village_Api.Application.DTO;
using Digitial_Village_Api.Domain.Persistence.Entities;

namespace Digital_Village_Api.Application.Interface
{
    public interface IProductRepository
    {
        //public Task<string> AddProduct( ViProduct vp);
        public Task<string> UpdateProduct(ViProducts vp);
        public Task<string> DeleteProduct(int id);
        public Task<List<ViProduct>> GetAllProduct();
        public Task<List<ViProduct>> GetProductsByRegistrationId(int registrationId);
        public Task<int> addProduct( ViProducts vp);
        public Task<List<ViProductCategory>> GetCategories();
      public Task UpdateProductImage(int productId, string imageUrl);
    }
}