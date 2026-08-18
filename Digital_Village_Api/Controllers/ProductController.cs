using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Digital_Village_Api.Application.DTO;
using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Digital_Village_Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _ProductRepository;
        private readonly IImageService _imageService;
        public ProductController(ILogger<ProductController> logger, IProductRepository ProductRepository, IImageService imageService)
        {
            _ProductRepository = ProductRepository;
            _imageService = imageService;
            _logger = logger;
        }


        // [HttpPost]
        // [Route("AddProduct")]
        // public async Task<ActionResult> AddProdcut([FromForm] ViProducts vp)
        // {
        //     try
        //     {
        //         if (vp != null)
        //         {
        //             string? ImageUrl = null;
        //             if (vp.ProductImage != null)
        //             {
        //                 ImageUrl = await _imageService.SaveImageAsync(vp.ProductImage, "ProdcutImage", vp.RegistrationId);
        //             }

        //             var vir = new ViProduct
        //             {
        //                 ProductName = vp.ProductName,
        //                 ProductQuantity = vp.ProductQuantity,
        //                 ProductImageUrl = ImageUrl,
        //                 ProductPrice = vp.ProductPrice,
        //                 ProductCategory = vp.ProductCategory,
        //                 ProductDiscount = vp.ProductDiscount,
        //                 RegistrationId = Convert.ToInt32(vp.RegistrationId)
        //             };
        //             var result = await _ProductRepository.AddProduct(vir);
        //             if (result == "products added  successful")
        //             {
        //                 return Ok(new
        //                 {
        //                     Message = result,
        //                 });
        //             }

        //             return BadRequest(new
        //             {
        //                 Message = result
        //             });
        //         }

        //         return BadRequest("Unable to add the product.");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, ex.Message);
        //     }
        // }
        [Authorize(Roles = "Seller")]
        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult> AddProdcut([FromForm] ViProducts vp)
        {
            try
            {
                if (vp == null)
                {
                    return BadRequest("Unable to add the product.");
                }
                var productId = await _ProductRepository.addProduct(vp);

                string? imageUrl = null;


                if (vp.ProductImage != null)
                {
                    imageUrl = await _imageService.SaveImageAsync(
                        vp.ProductImage,
                        "ProductImage",
                        productId.ToString()
                    );


                    await _ProductRepository.UpdateProductImage(
                        productId,
                        imageUrl
                    );
                }

                return Ok(new
                {
                    Message = "Product added successfully",
                    ProductId = productId,
                    ProductImageUrl = imageUrl
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Seller")]
        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct([FromForm] ViProducts vp)
        {
            try
            {
                if (vp == null)
                {
                    return BadRequest(new
                    {
                        Message = "Product update failed."
                    });
                }

                // var product = new ViProduct
                // {
                //     ProductId = vp.ProductId,
                //     ProductName = vp.ProductName,
                //     ProductQuantity = vp.ProductQuantity,
                //     ProductPrice = vp.ProductPrice,
                //     ProductCategory = vp.ProductCategory,
                //     ProductDiscount = vp.ProductDiscount,
                //     ProductUnitValue=vp.ProductUnitValue,
                //     IsActive=vp.IsActive
                // };

                var result = await _ProductRepository.UpdateProduct(vp);

                if (result == "Product updated successfully")
                {
                    return Ok(new
                    {
                        Message = result
                    });
                }

                if (result == "Product not found")
                {
                    return NotFound(new
                    {
                        Message = result
                    });
                }

                return BadRequest(new
                {
                    Message = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Seller")]
        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _ProductRepository.DeleteProduct(id);

                if (result == "Product deleted successfully")
                {
                    return Ok(new
                    {
                        Message = result
                    });
                }

                return NotFound(new
                {
                    Message = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts()
        {
            try
            {
                var result = await _ProductRepository.GetAllProduct();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }
        [Authorize(Roles = "Seller,Customer")]
        [HttpGet]
        [Route("GetByIdProduct/{registrationId}")]
        public async Task<ActionResult> GetByIdProduct(int registrationId)
        {
            try
            {
                var result = await _ProductRepository
                    .GetProductsByRegistrationId(registrationId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }
        [Authorize(Roles = "Seller,Customer")]
        [HttpGet]
        [Route("GetCategories")]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                var result = await _ProductRepository.GetCategories();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }

    }
}