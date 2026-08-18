using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digital_Village_Api.Application.DTO;
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
        // public async Task<string> AddProduct(ViProduct vp)
        // {
        //     try
        //     {
        //         if (vp == null)
        //         {
        //             return "No data received";
        //         }

        //         var Vip = new ViProduct
        //         {
        //             ProductName = vp.ProductName,
        //             ProductQuantity = vp.ProductQuantity,
        //             ProductImageUrl = vp.ProductImageUrl,
        //             ProductPrice = vp.ProductPrice,
        //             ProductCategory = vp.ProductCategory,
        //             ProductDiscount = vp.ProductDiscount,
        //             RegistrationId = vp.RegistrationId
        //         };

        //         // Add record to DbContext
        //         await _villageDbContext.ViProducts.AddAsync(Vip);

        //         // // Save record to SQL Server
        //         var dbResult = await _villageDbContext.SaveChangesAsync();

        //         if (dbResult <= 0)
        //         {
        //             return "prodcut added failed in database";
        //         }

        //         // Save record to Excel
        //         var saveInExcel = _IExcelservice.ExcelSaves(
        //             Vip,
        //             "products.xlsx",
        //             "prodcuts");

        //         if (!saveInExcel)
        //         {
        //             return "Data saved in database, but Excel save failed";
        //         }

        //         return "products added  successful";
        //     }
        //     catch (Exception ex)
        //     {
        //         return $"adding product failed: {ex}";
        //     }
        // }
        public async Task<int> addProduct(ViProducts vp)
        {
            try
            {
                var Vip = new ViProduct
                {
                    ProductName = vp.ProductName,
                    ProductQuantity = vp.ProductQuantity,
                    ProductPrice = vp.ProductPrice,
                    ProductCategory = vp.ProductCategory,
                    ProductDiscount = vp.ProductDiscount,
                    RegistrationId = Convert.ToInt32(vp.RegistrationId),
                    ProductUnitValue = vp.ProductUnitValue,
                    ProductUnit = vp.ProductUnit,
                    IsActive = vp.IsActive

                };

                // Save product into database
                await _villageDbContext.ViProducts.AddAsync(Vip);

                await _villageDbContext.SaveChangesAsync();

                // ProductId is generated here
                int productId = Vip.ProductId;

                // Save product into Excel
                var saveInExcel = _IExcelservice.ExcelSaves(
                    Vip,
                    "products.xlsx",
                    "prodcuts");

                if (!saveInExcel)
                {
                    // Database is already saved, but Excel failed
                    throw new Exception("Product saved in database, but Excel save failed.");
                }

                // Return generated ProductId
                return productId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Adding product failed: {ex.Message}", ex);
            }
        }
        public async Task UpdateProductImage(int productId, string imageUrl)
        {
            var product = await _villageDbContext.ViProducts
                .FirstOrDefaultAsync(x => x.ProductId == productId);

            if (product != null)
            {
                product.ProductImageUrl = imageUrl;

                await _villageDbContext.SaveChangesAsync();
            }
        }
        public async Task<string> UpdateProduct(ViProducts vp)
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
            product.ProductUnitValue = vp.ProductUnitValue;
            product.IsActive = vp.IsActive;


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
        public async Task<List<ViProduct>> GetProductsByRegistrationId(int registrationId)
        {
            var result = await _villageDbContext.ViProducts.Where(x => x.RegistrationId == registrationId)
            .Select(x => new ViProduct
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                ProductQuantity = x.ProductQuantity,
                ProductPrice = x.ProductPrice,
                ProductDiscount = x.ProductDiscount,
                ProductCategory = x.ProductCategory,
                ProductImageUrl = x.ProductImageUrl,
                ProductUnitValue = x.ProductUnitValue,
                ProductUnit = x.ProductUnit,
                IsActive = x.IsActive

            }).ToListAsync();
            return result;
        }
        public async Task<List<ViProductCategory>> GetCategories()
        {

            var result = await _villageDbContext.ViProductCategories
       .Select(x => new ViProductCategory
       {
           CategoryId = x.CategoryId,
           CategoryName = x.CategoryName,
           IsActive = x.IsActive,
       }).ToListAsync();
            return result;

        }
    }
}