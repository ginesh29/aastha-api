using AASTHA2.Common;
using AASTHA2.DTO;
using AASTHA2.Services;
using AutoMapper;
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
    //[Authorize]
    public class IpdsController : ControllerBase
    {
        private static IpdService _IpdService;
        private static LookupService _LookupService;
        private readonly IMapper _mapper;
        public IpdsController(ServicesWrapper ServicesWrapper, IMapper mapper)
        {
            _IpdService = ServicesWrapper.IpdService;
            _LookupService = ServicesWrapper.LookupService;
            _mapper = mapper;
        }
        // GET: api/Ipds
        [HttpGet]
        public dynamic GetIpds(string filter, string sort, int skip, int take, string includeProperties = "", string fields = "")
        {
            int totalCount;
            var data = _IpdService.GetIpds(filter, out totalCount, sort, skip, take, includeProperties, fields);

            var result = new { TotalCount = totalCount, Data = data.ToDynamicList() };
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
            var opd = _IpdService.GetIpd(IpdDTO.id, null, includeProperties);
            return CreatedAtAction("GetIpd", new { id = IpdDTO.id }, opd);
        }
        [HttpPut]
        public ActionResult<IpdDTO> PutIpd(IpdDTO IpdDTO, string includeProperties = "")
        {
            var Ipd = _IpdService.GetIpd(IpdDTO.id, "", includeProperties);
            if (Ipd == null)
            {
                return NotFound();
            }
            _IpdService.PutIpd(IpdDTO);
            var removedLookup = Ipd.ipdLookups.Where(i => i.ipdId == IpdDTO.id && !IpdDTO.ipdLookups.Select(m=>m.id).Contains(i.id));
            //var map =  _mapper.Map<IEnumerable<LookupDTO>>(removedLookup);
            //_LookupService.RemoveLookupRange(map);
            Ipd = _IpdService.GetIpd(IpdDTO.id, null, includeProperties);
            return CreatedAtAction("GetIpd", new { id = IpdDTO.id }, Ipd);
        }
        [HttpDelete("{id}")]
        public ActionResult<IpdDTO> DeleteIpd(long id, bool isDeleted, bool removePhysical = false)
        {
            var Ipd = _IpdService.GetIpd(id);
            Ipd.isDeleted = isDeleted;
            if (Ipd == null)
            {
                return NotFound();
            }
            _IpdService.RemoveIpd(Ipd, "", removePhysical);
            return CreatedAtAction("GetIpd", new { id = id }, Ipd);
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
                int totalCount;
                string filter = $"type-eq-{{{(int)LookupType.ChargeType}}}";
                var charges = _LookupService.GetLookups(filter, out totalCount).ToDynamicList<LookupDTO>();
                foreach (var item in charges)
                {
                    workSheet.Cells[$"{alpha}1"].Value = $"{item.name.Substring(0, 3)}.";
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
                    workSheet.Cells[$"A{row}"].Value = item.uniqueId;
                    workSheet.Cells[$"B{row}"].Value = item.patient.fullname;
                    workSheet.Cells[$"C{row}"].Value = item.ipdType;
                    workSheet.Cells[$"D{row}"].Value = item.addmissionDate;
                    workSheet.Cells[$"E{row}"].Value = item.dischargeDate;

                    alpha = 'F';
                    foreach (var charge in charges)
                    {
                        var amount = item.charges.FirstOrDefault(m => m.lookupId == charge.id)?.amount;
                        workSheet.Cells[$"{alpha}{row}"].Value = amount > 0 ? amount : 0;
                        alpha++;
                    }
                    workSheet.Cells[$"{alpha}{row}"].Formula = $"=SUM(F{row}:{alpha}{row})";
                    workSheet.Cells[$"D{row}"].Style.Numberformat.Format = "dd/mm/yyyy";
                    workSheet.Cells[$"E{row}"].Style.Numberformat.Format = "dd/mm/yyyy";
                    row++;
                }
                workSheet.Cells[$"A{row}"].Value = "Total";
                alpha = 'F';
                foreach (var charge in charges)
                {
                    workSheet.Cells[$"{alpha}{row}"].Formula = $"=SUM({alpha}2:{alpha}{row})";
                    alpha++;
                }
                workSheet.Cells[$"{alpha}{row}"].Formula = $"=SUM({alpha}2:{alpha}{row})";

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
