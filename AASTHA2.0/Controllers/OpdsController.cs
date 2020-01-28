using AASTHA2.Common;
using AASTHA2.DTO;
using AASTHA2.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public dynamic GetOpds(string filter, string sort, int skip, int take, string includeProperties = "", string fields = "")
        {
            int totalCount;
            var data = _OpdService.GetOpds(filter, out totalCount, sort, skip, take, includeProperties, fields);

            var result = new { TotalCount = totalCount, Data = data.ToDynamicList() };
            return Ok(result);
        }

        // GET: api/Opds/5
        [HttpGet("{id}")]
        public ActionResult<OpdDTO> GetOpd(long id, string filter)
        {
            var Opd = _OpdService.GetOpd(id, filter);

            string stringValue = Enum.GetName(typeof(CaseType), Opd.caseType);
            if (Opd == null)
            {
                return NotFound();
            }
            return Opd;
        }
        [HttpGet]
        [Route("GetStatistics")]
        public ActionResult<dynamic> GetStatistics(string filter)
        {
            int totalCount;
            var result = _OpdService.GetOpdStatistics(filter, out totalCount);
            return Ok(result);
        }
        [HttpPost]
        public ActionResult<OpdDTO> PostOpd(OpdDTO OpdDTO)
        {
            _OpdService.PostOpd(OpdDTO);
            return CreatedAtAction("GetOpd", new { id = OpdDTO.id }, OpdDTO);
        }
        [HttpPut]
        public ActionResult<OpdDTO> PutOpd(OpdDTO OpdDTO)
        {
            var Opd = _OpdService.GetOpd(OpdDTO.id);
            if (Opd == null)
            {
                return NotFound();
            }
            _OpdService.PutOpd(OpdDTO);
            Opd = _OpdService.GetOpd(OpdDTO.id);
            return CreatedAtAction("GetOpd", new { id = OpdDTO.id }, Opd);
        }
        [HttpDelete("{id}")]
        public ActionResult<OpdDTO> DeleteOpd(long id, bool removePhysical = false)
        {
            var Opd = _OpdService.GetOpd(id);
            if (Opd == null)
            {
                return NotFound();
            }
            _OpdService.RemoveOpd(Opd, "", removePhysical);
            return CreatedAtAction("GetOpd", new { id = id }, Opd);
        }
        [HttpGet]
        [Route("ExportOpdReport")]
        public ActionResult ExportOPD(string filter, string sort, int skip, int take, string includeProperties = "", string fields = "")
        {
            var stream = new MemoryStream();
            int totalCount;
            var opds = _OpdService.GetOpds(filter, out totalCount, sort, skip, take, includeProperties, fields).ToDynamicList<OpdDTO>();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet2 = package.Workbook.Worksheets.Add("Report");
                workSheet2.DefaultColWidth = 15;

                workSheet2.Cells["A1"].Value = "Invoice No";
                workSheet2.Cells["B1"].Value = "Opd Id";
                workSheet2.Cells["C1"].Value = "Patient's Name";
                workSheet2.Cells["D1"].Value = "Case Type";
                workSheet2.Cells["E1"].Value = "Opd Date";
                workSheet2.Cells["f1"].Value = "Cons.";
                workSheet2.Cells["G1"].Value = "Usg.";
                workSheet2.Cells["H1"].Value = "Upt.";
                workSheet2.Cells["I1"].Value = "Inj.";
                workSheet2.Cells["J1"].Value = "Other.";
                workSheet2.Cells["K1"].Value = "Total";

                var headerCell = workSheet2.Cells["A1:K1"];
                headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                headerCell.Style.Font.Bold = true;

                int row1 = 2;
                foreach (var item in opds)
                {
                    var totalCharge = item.consultCharge + item.usgCharge + item.uptCharge + item.injectionCharge + item.otherCharge;
                    workSheet2.Cells[string.Format("A{0}", row1)].Value = item.id;
                    workSheet2.Cells[string.Format("B{0}", row1)].Value = item.invoiceNo;
                    workSheet2.Cells[string.Format("C{0}", row1)].Value = item.Patient.fullname;
                    workSheet2.Cells[string.Format("D{0}", row1)].Value = item.caseType;
                    workSheet2.Cells[string.Format("E{0}", row1)].Value = item.date;
                    workSheet2.Cells[string.Format("F{0}", row1)].Value = Convert.ToDecimal(item.consultCharge);
                    workSheet2.Cells[string.Format("G{0}", row1)].Value = Convert.ToDecimal(item.usgCharge);
                    workSheet2.Cells[string.Format("H{0}", row1)].Value = Convert.ToDecimal(item.uptCharge);
                    workSheet2.Cells[string.Format("I{0}", row1)].Value = Convert.ToDecimal(item.injectionCharge);
                    workSheet2.Cells[string.Format("J{0}", row1)].Value = Convert.ToDecimal(item.otherCharge);
                    workSheet2.Cells[string.Format("K{0}", row1)].Value = Convert.ToDecimal(totalCharge);
                    workSheet2.Cells[string.Format("E{0}", row1)].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    row1++;
                }
                workSheet2.Cells[string.Format("A{0}", row1)].Value = "Total";
                workSheet2.Cells[string.Format("F{0}", row1)].Formula = "=SUM(F2:F" + row1 + ")";
                workSheet2.Cells[string.Format("G{0}", row1)].Formula = "=SUM(G2:G" + row1 + ")";
                workSheet2.Cells[string.Format("H{0}", row1)].Formula = "=SUM(H2:H" + row1 + ")";
                workSheet2.Cells[string.Format("I{0}", row1)].Formula = "=SUM(I2:I" + row1 + ")";
                workSheet2.Cells[string.Format("J{0}", row1)].Formula = "=SUM(J2:J" + row1 + ")";
                workSheet2.Cells[string.Format("K{0}", row1)].Formula = "=SUM(K2:K" + row1 + ")";

                var footerCell = workSheet2.Cells[string.Format("A{0}:K{0}", row1)];
                footerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                footerCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                footerCell.Style.Font.Bold = true;

                workSheet2.Column(1).Width = 10;
                workSheet2.Column(3).Width = 30;
                workSheet2.Column(4).Width = 10;
                package.Save();
            }
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
