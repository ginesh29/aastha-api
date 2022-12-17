using AASTHA2.Common;
using AASTHA2.Services;
using AASTHA2.Services.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IpdsController : ControllerBase
    {
        private static IpdService _IpdService;
        private static LookupService _LookupService;
        public IpdsController(ServicesWrapper ServicesWrapper)
        {
            _IpdService = ServicesWrapper.IpdService;
            _LookupService = ServicesWrapper.LookupService;
        }
        // GET: api/Ipds
        [HttpGet]
        public ActionResult GetIpds([FromQuery] FilterModel filterModel)
        {
            var result = _IpdService.GetIpds(filterModel);
            return Ok(result);
        }

        // GET: api/Ipds/5
        [HttpGet("{id}")]
        public ActionResult<IpdDTO> GetIpd(long id, string filter)
        {
            var Ipd = _IpdService.GetIpd(id, filter);
            if (Ipd == null)
            {
                return NotFound();
            }
            return Ipd;
        }
        [HttpPost]
        public ActionResult<IpdDTO> PostIpd(IpdDTO IpdDTO, string includeProperties = "")
        {
            _IpdService.PostIpd(IpdDTO);
            var opd = _IpdService.GetIpd(IpdDTO.Id, null, includeProperties);
            return CreatedAtAction("GetIpd", new { IpdDTO.Id }, opd);
        }
        [HttpPut]
        public ActionResult<IpdDTO> PutIpd(IpdDTO IpdDTO, string includeProperties = "")
        {
            var Ipd = _IpdService.GetIpd(IpdDTO.Id, "", includeProperties);
            if (Ipd == null)
            {
                return NotFound();
            }
            var removedLookup = Ipd.IpdLookups.Where(i => i.IpdId == IpdDTO.Id && !IpdDTO.IpdLookups.Select(m => m.Id).Contains(i.Id));
            _IpdService.RemoveIpdLookup(removedLookup, "", true);
            _IpdService.PutIpd(IpdDTO);
            Ipd = _IpdService.GetIpd(IpdDTO.Id, null, includeProperties);
            return CreatedAtAction("GetIpd", new { IpdDTO.Id }, Ipd);
        }
        [HttpDelete("{id}")]
        public ActionResult<IpdDTO> DeleteIpd(long id, bool isDeleted, bool removePhysical = false)
        {
            var Ipd = _IpdService.GetIpd(id);
            Ipd.IsDeleted = isDeleted;
            if (Ipd == null)
            {
                return NotFound();
            }
            _IpdService.RemoveIpd(Ipd, "", removePhysical);
            return CreatedAtAction("GetIpd", new { id }, Ipd);
        }
        [HttpPost]
        [Route("ExportReport")]
        public ActionResult ExportIPD(List<IpdDTO> ipds)
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Report");
                workSheet.DefaultColWidth = 15;

                workSheet.Cells["A1"].Value = "Invoice No";
                workSheet.Cells["B1"].Value = "Patient's Name";
                workSheet.Cells["C1"].Value = "Ipd Type";
                workSheet.Cells["D1"].Value = "Add. Date";
                workSheet.Cells["E1"].Value = "Dis. Date";

                char alpha = 'F';
                FilterModel filterModel = new()
                {
                    filter = $"type-eq-{{{(int)LookupType.ChargeType}}}"
                };
                var charges = _LookupService.GetLookups(filterModel).Data.ToDynamicList<LookupDTO>(); ;
                foreach (var item in charges)
                {
                    workSheet.Cells[$"{alpha}1"].Value = $"{item.Name[..3]}.";
                    alpha++;
                }

                workSheet.Cells[$"{alpha}1"].Value = "Total";

                var headerCell = workSheet.Cells[$"A1:${alpha}1"];
                headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                headerCell.Style.Font.Bold = true;

                int row = 2;
                foreach (var item in ipds)
                {
                    workSheet.Cells[$"A{row}"].Value = item.UniqueId;
                    workSheet.Cells[$"B{row}"].Value = item.Patient.Fullname;
                    workSheet.Cells[$"C{row}"].Value = item.IpdType;
                    workSheet.Cells[$"D{row}"].Value = item.AddmissionDate;
                    workSheet.Cells[$"E{row}"].Value = item.DischargeDate;

                    alpha = 'F';
                    foreach (var charge in charges)
                    {
                        var amount = item.Charges.FirstOrDefault(m => m.LookupId == charge.Id)?.Amount;
                        var a = $"{alpha}{row}";
                        workSheet.Cells[$"{alpha}{row}"].Value = amount > 0 ? amount : 0;
                        alpha++;
                    }
                    workSheet.Cells[$"{alpha}{row}"].Formula = $"=SUM(F{row}:{--alpha}{row})";
                    workSheet.Cells[$"D{row}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    workSheet.Cells[$"E{row}"].Style.Numberformat.Format = "dd-mm-yyyy";
                    row++;
                }
                workSheet.Cells[$"A{row}"].Value = "Total";
                alpha = 'F';
                foreach (var charge in charges)
                {
                    workSheet.Cells[$"{alpha}{row}"].Formula = $"=SUM({alpha}2:{alpha}{row - 1})";
                    alpha++;
                }
                workSheet.Cells[$"{alpha}{row}"].Formula = $"=SUM(F{row}:{--alpha}{row})";

                var footerCell = workSheet.Cells[$"A{row}:{alpha}{row}"];
                footerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                footerCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                footerCell.Style.Font.Bold = true;

                workSheet.Column(1).Width = 10;
                workSheet.Column(2).Width = 30;
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"IPDReport.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}