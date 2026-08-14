using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Persistence;
using Digitial_Village_Api.Domain.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Digital_village_Api.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly VillageDbContext _villageDbContext;
        public readonly IExcelService _IExcelservice;
        public ProductRepository(VillageDbContext villageDbContext, IExcelService IExcelservice)
        {
            _villageDbContext = villageDbContext;
            _IExcelservice = IExcelservice;
        }
        public async Task<string> AddProduct(ViProduct vp)
        {
            try
            {
                if (vp == null)
                {
                    return "No data received";
                }

                var Vip = new ViProduct
                {
                    ProductName = vp.ProductName,
                    ProductQuantity = vp.ProductQuantity,
                    ProductImageUrl = vp.ProductImageUrl,
                    ProductPrice = vp.ProductPrice,
                    ProductCategory = vp.ProductCategory,
                    ProductDiscount = vp.ProductDiscount,
                    RegistrationId = vp.RegistrationId
                };

                // Add record to DbContext
                await _villageDbContext.ViProducts.AddAsync(Vip);

                // // Save record to SQL Server
                var dbResult = await _villageDbContext.SaveChangesAsync();

                if (dbResult <= 0)
                {
                    return "prodcut added failed in database";
                }

                // Save record to Excel
                var saveInExcel = _IExcelservice.ExcelSaves(
                    Vip,
                    "products.xlsx",
                    "prodcuts");

                if (!saveInExcel)
                {
                    return "Data saved in database, but Excel save failed";
                }

                return "products added  successful";
            }
            catch (Exception ex)
            {
                return $"adding product failed: {ex}";
            }
        }

        public async Task<string> UpdateProduct(ViProduct vp)
        {
            var product = await _villageDbContext.ViProducts
        .FirstOrDefaultAsync(x => x.ProductId == vp.ProductId);

            if (product == null)
            {
                return "prodcut not found";
            }

            product.ProductName = vp.ProductName;
            product.ProductQuantity = vp.ProductQuantity;
            product.ProductPrice = vp.ProductPrice;
            product.ProductCategory = vp.ProductCategory;
            product.ProductDiscount = vp.ProductDiscount;


            await _villageDbContext.SaveChangesAsync();

            return "Product updated successfully";


        }

        public async Task<string> DeleteProduct(int id)
        {
            var product = await _villageDbContext.ViProducts
        .FirstOrDefaultAsync(x => x.ProductId == id);


            if (product == null)
            {
                return "Product not found";

            }

            _villageDbContext.ViProducts.Remove(product);

            await _villageDbContext.SaveChangesAsync();

            return "Product deleted successfully";

        }

        public async Task<List<ViProduct>> GetAllProduct()
        {
            var result = await _villageDbContext.ViProducts
            .Select(x => new ViProduct
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductQuantity = x.ProductQuantity,
                ProductPrice = x.ProductPrice,
                ProductDiscount = x.ProductDiscount,
                ProductCategory = x.ProductCategory,
                ProductImageUrl = x.ProductImageUrl
            }).ToListAsync();
            return result;

        }
    }
}