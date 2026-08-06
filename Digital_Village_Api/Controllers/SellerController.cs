using Digital_Village_Api.Application.DTO;
using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Entities;
using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Digital_Village_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;
        private readonly IImageService _imageService;
        public SellerController(ISellerService sellerservice, IImageService imageService)
        {
            _sellerService = sellerservice;
            _imageService = imageService;
        }

        [HttpPost]
        [Route("RegisterSeller")]
        public async Task<ActionResult>  RegisterSeller([FromForm] SellerRegistrationRequest Sr)
        {
            try
            {


                Sr.SellerId = Guid.NewGuid();
                string imageurl = await _imageService.SaveImageAsync(Sr.ShopImage, "ShopImage", Sr.SellerId);
                Seller seller = new Seller()
                {
                    SellerId = Sr.SellerId,
                    SellerName = Sr.SellerName,
                    Mobile = Sr.Mobile,
                    Email = Sr.Email,
                    Password = Sr.Password,
                    ConfirmPassword = Sr.ConfirmPassword,
                    ShopName = Sr.ShopName,
                    Country = Sr.Country,
                    State = Sr.State,
                    District = Sr.District,
                    Subdistrict = Sr.Subdistrict,
                    VillageName = Sr.VillageName,
                    ShopImageUrl = imageurl

                };
                var result = _sellerService.RegisterSeller(seller);
                if (result !=null)
                {
                    return Ok(new
                    {
                        Message = "Seller registered successfully.",
                        SellerId = seller.SellerId
                    });
                }

                return BadRequest("Unable to register seller.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }
    
    }
}
