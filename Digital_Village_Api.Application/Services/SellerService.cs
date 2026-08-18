using Digital_Village_Api.Application.DTO;
using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Entities;
using Digitial_Village_Api.Domain.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_Village_Api.Application.Services
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepository _sellerRepository;
        public SellerService(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }

       

        public async Task<string> RegisterSeller(ViRegistration vi)
        {
            var result = await _sellerRepository.RegisterSeller(vi);
            return result;
        }
         public  async Task<List<ShopResponseDto>> Getshops()
        {
            var result = await _sellerRepository.GetShops();
            return result;
        }
        

       
    }
}
