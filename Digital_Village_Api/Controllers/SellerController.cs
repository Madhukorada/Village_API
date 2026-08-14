using Digital_Village_Api.Application.DTO;
using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Entities;
using Digitial_Village_Api.Domain.Persistence.Entities;
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
        public async Task<ActionResult> RegisterSeller([FromForm] RegistrationRequest Sr)
        {
            try
            {
                if (Sr != null)
                {
                    string? ImageUrl = null;
                    if (Sr.ShopGovtRegistrationId != null)
                    {
                        ImageUrl = await _imageService.SaveImageAsync(Sr.ShopImage, "ShopImage", Sr.ShopGovtRegistrationId);
                    }

                    var vir = new ViRegistration
                    {
                        FirstName = Sr.FirstName,
                        LastName = Sr.LastName,
                        Mobile = Sr.Mobile,
                        Password = Sr.Password,
                        ConfirmPassword = Sr.ConfirmPassword,
                        Gender = Sr.Gender,
                        Email = Sr.Email,
                        Country = Sr.Country,
                        State = Sr.State,
                        District = Sr.District,
                        Subdistrict = Sr.Subdistrict,
                        VillageName = Sr.VillageName,
                        Pincode = Sr.Pincode,
                        Address = Sr.Address,
                        Role = Sr.Role,
                        ShopName = Sr.ShopName,
                        ShopImage = ImageUrl,
                        ShopGovtRegistrationId = Sr.ShopGovtRegistrationId,
                        CreatedDate = DateTime.UtcNow
                    };
                    var result = await _sellerService.RegisterSeller(vir);
                    if (result == "Registration is successful")
                    {
                        return Ok(new
                        {
                            Message = result,
                            SellerId = Sr.ShopGovtRegistrationId
                        });
                    }

                    return BadRequest(new
                    {
                        Message = result
                    });
                }



                return BadRequest("Unable to register .");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

    }
}
