using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_village_Api.Infrastructure.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        public readonly IExcelService _IExcelservice;
        public SellerRepository(IExcelService IExcelservice)
        {
            _IExcelservice = IExcelservice;
        }
        public string RegisterSeller(Seller seller)
        {
            try
            {
                if (seller != null)
                {
                    var sl = new Seller()
                    {   SellerId= seller.SellerId,
                        SellerName = seller.SellerName,
                        Mobile = seller.Mobile,
                        Email = seller.Email,
                        Password = seller.Password,
                        ConfirmPassword = seller.ConfirmPassword,
                        ShopName = seller.ShopName,
                        Country = seller.Country,
                        State = seller.State,
                        District = seller.District,
                        Subdistrict = seller.Subdistrict,
                        VillageName = seller.VillageName,
                        ShopImageUrl = seller.ShopImageUrl

                    };
                    var SaveInExcel = _IExcelservice.ExcelSaves(sl, "Sellers.xlsx", "Sellers");
                    if (SaveInExcel)
                    {
                        return "registration is success";
                    }
                    else
                    {
                        return "registration is failed";

                    }
                }
                else
                {
                    return "no data received";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }


}

