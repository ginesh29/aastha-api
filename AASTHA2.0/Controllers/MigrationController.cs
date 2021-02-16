//using AASTHA.Entities;
//using AASTHA2.Common;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Threading;

//namespace AASTHA2.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MigrationController : ControllerBase
//    {
//        private IHostingEnvironment _env;
//        private Migration _migration;
//        public MigrationController(IHostingEnvironment env)
//        {
//            _env = env;
//            _migration = new Migration(_env);
//        }       
//        [HttpGet]
//        public ActionResult Index()
//        {
//            _migration.GenerateMigrationScript();
//            return Ok(new { Message = "Data Migration Successfully" });
//        }

//    }
//}
