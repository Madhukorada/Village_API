using ClosedXML.Excel;
using Digital_Village_Api.Application.Interface;
using Digitial_Village_Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digital_village_Api.Infrastructure.ExternalServices
{
    public class ExcelService:IExcelService
    {
        
        public bool ExcelSave(Citizen citizen)
        {
            try
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
                if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var filePath = Path.Combine(folderPath, "citizens.xlsx");
            
              
                if (!File.Exists(filePath))
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Citizens");

                       
                        worksheet.Cell(1, 1).Value = "Id";
                        worksheet.Cell(1, 2).Value = "FirstName";
                        worksheet.Cell(1, 3).Value = "LastName";
                        worksheet.Cell(1, 4).Value = "Age";
                        worksheet.Cell(1, 5).Value = "Gender";
                        worksheet.Cell(1, 6).Value = "Mobile";
                        worksheet.Cell(1, 7).Value = "FamilyHead";
                        worksheet.Cell(1, 8).Value = "Villagecode";
                        //worksheet.Cell(1, 9).Value = "Password";
                        //worksheet.Cell(1, 10).Value = "ConfirmPassword";


                        workbook.SaveAs(filePath);
                    }
                }
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("Citizens");
                    worksheet.Cell(1, 9).Value = "Password";
                    worksheet.Cell(1, 10).Value = "ConfirmPassword";

                    int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 1;

                    int newId = 1;

                    if (lastRow > 1)
                    {
                        var lastId = worksheet.Cell(lastRow, 1).GetValue<int>();
                        newId = lastId + 1;
                    }

                    int newRow = lastRow + 1;
                    worksheet.Cell(newRow, 1).Value = newId;
                    worksheet.Cell(newRow, 2).Value = citizen.FirstName;
                    worksheet.Cell(newRow, 3).Value = citizen.LastName;
                    worksheet.Cell(newRow, 4).Value = citizen.Age;
                    worksheet.Cell(newRow, 5).Value = citizen.Gender;
                    worksheet.Cell(newRow, 6).Value = citizen.Mobile;
                    worksheet.Cell(newRow, 7).Value = citizen.FamilyHead;
                    worksheet.Cell(newRow, 8).Value = citizen.Villagecode;
                    worksheet.Cell(newRow, 9).Value = citizen.Password;
                    worksheet.Cell(newRow, 10).Value = citizen.ConfirmPassword;

                    workbook.Save();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while saving Excel: " + ex.Message);
                return false;
            }

        }


    }
}

