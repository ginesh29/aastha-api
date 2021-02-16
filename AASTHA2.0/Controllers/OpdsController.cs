using AASTHA2.Common;
using AASTHA2.DTO;
using AASTHA2.Models;
using AASTHA2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OpdsController : ControllerBase
    {
        private static OpdService _OpdService;
        public OpdsController(ServicesWrapper ServicesWrapper)
        {
            _OpdService = ServicesWrapper.OpdService;
        }
        // GET: api/Opds
        [HttpGet]
        public ActionResult GetOpds([FromQuery]FilterModel filterModel)
        {
            var result = _OpdService.GetOpds(filterModel);
            return Ok(result);
        }

        // GET: api/Opds/5
        [HttpGet("{id}")]
        public ActionResult<OpdDTO> GetOpd(long id, string filter)
        {
            var Opd = _OpdService.GetOpd(id, filter);

            if (Opd == null)
            {
                return NotFound();
            }
            return Opd;
        }
        [HttpPost]
        public ActionResult<OpdDTO> PostOpd(OpdDTO OpdDTO, string includeProperties = "")
        {
            _OpdService.PostOpd(OpdDTO);
            var opd = _OpdService.GetOpd(OpdDTO.id, null, includeProperties);
            return CreatedAtAction("GetOpd", new { id = OpdDTO.id }, opd);
        }
        [HttpPut]
        public ActionResult<OpdDTO> PutOpd(OpdDTO OpdDTO, string includeProperties = "")
        {
            var Opd = _OpdService.GetOpd(OpdDTO.id);
            if (Opd == null)
            {
                return NotFound();
            }
            _OpdService.PutOpd(OpdDTO);
            Opd = _OpdService.GetOpd(OpdDTO.id, null, includeProperties);
            return CreatedAtAction("GetOpd", new { id = OpdDTO.id }, Opd);
        }
        [HttpDelete("{id}")]
        public ActionResult<OpdDTO> DeleteOpd(long id, bool isDeleted, bool removePhysical = false)
        {
            var Opd = _OpdService.GetOpd(id);
            Opd.isDeleted = isDeleted;
            if (Opd == null)
            {
                return NotFound();
            }
            _OpdService.RemoveOpd(Opd, "", removePhysical);
            return CreatedAtAction("GetOpd", new { id = id }, Opd);
        }
        [HttpPost]
        [Route("ExportReport")]
        public ActionResult ExportOPD(List<OpdDTO> opds)
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Report");
                workSheet.DefaultColWidth = 15;

                workSheet.Cells["A1"].Value = "Invoice No";
                workSheet.Cells["B1"].Value = "Opd Id";
                workSheet.Cells["C1"].Value = "Patient's Name";
                workSheet.Cells["D1"].Value = "Case Type";
                workSheet.Cells["E1"].Value = "Opd Date";
                workSheet.Cells["f1"].Value = "Cons.";
                workSheet.Cells["G1"].Value = "Usg.";
                workSheet.Cells["H1"].Value = "Upt.";
                workSheet.Cells["I1"].Value = "Inj.";
                workSheet.Cells["J1"].Value = "Other.";
                workSheet.Cells["K1"].Value = "Total";

                var headerCell = workSheet.Cells["A1:K1"];
                headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                headerCell.Style.Font.Bold = true;

                int row = 2;
                foreach (var item in opds)
                {
                    workSheet.Cells[$"A{row}"].Value = item.id;
                    workSheet.Cells[$"B{row}"].Value = item.invoiceNo;
                    workSheet.Cells[$"C{row}"].Value = item.patient.fullname;
                    workSheet.Cells[$"D{row}"].Value = item.caseType;
                    workSheet.Cells[$"E{row}"].Value = item.date;
                    workSheet.Cells[$"E{row}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    workSheet.Cells[$"F{row}"].Value = Convert.ToDecimal(item.consultCharge);
                    workSheet.Cells[$"G{row}"].Value = Convert.ToDecimal(item.usgCharge);
                    workSheet.Cells[$"H{row}"].Value = Convert.ToDecimal(item.uptCharge);
                    workSheet.Cells[$"I{row}"].Value = Convert.ToDecimal(item.injectionCharge);
                    workSheet.Cells[$"J{row}"].Value = Convert.ToDecimal(item.otherCharge);
                    workSheet.Cells[$"K{row}"].Formula = $"=SUM(F{row}:J{row})";
                    row++;
                }
                workSheet.Cells[$"A{row}"].Value = "Total";
                workSheet.Cells[$"F{row}"].Formula = $"=SUM(F2:F{row - 1})";
                workSheet.Cells[$"G{row}"].Formula = $"=SUM(G2:G{row - 1})";
                workSheet.Cells[$"H{row}"].Formula = $"=SUM(H2:H{row - 1})";
                workSheet.Cells[$"I{row}"].Formula = $"=SUM(I2:I{row - 1})";
                workSheet.Cells[$"J{row}"].Formula = $"=SUM(J2:J{row - 1})";
                workSheet.Cells[$"K{row}"].Formula = $"=SUM(F{row}:J{row})";

                var footerCell = workSheet.Cells[$"A{row}:K{row}"];
                footerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                footerCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                footerCell.Style.Font.Bold = true;

                workSheet.Column(1).Width = 10;
                workSheet.Column(3).Width = 30;
                workSheet.Column(4).Width = 10;
                package.Save();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OpdReport.xlsx");
        }
    }
}
