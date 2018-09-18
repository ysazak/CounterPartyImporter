using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CounterPartyDomain.Repositories;
using DataImporter.FileParsers;
using DataImporter.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CounterPartyImporter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IServiceProvider services;
        private readonly ICompanyRepository companyRepository;

        public CompanyController(IServiceProvider services, ICompanyRepository companyRepository)
        {
            this.services = services;
            this.companyRepository = companyRepository;
        }


        [HttpPost("ImportFile"), DisableRequestSizeLimit]
        public async Task<IActionResult> ImportFile()
        {
            try
            {
                var filePath = Path.GetTempFileName();
                var result = new List<int>();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string extension = IOUtils.GetExtensionWithoutDot(fileName).ToLower();

                    if (!FileParserFactory.SupportedExtensions.Any(e => e.Equals(extension, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        return BadRequest(new { error = "File format is not supported." });
                    }

                    filePath = Path.ChangeExtension(filePath, extension);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    var importer = ActivatorUtilities.CreateInstance<BL.CounterPartyImporter>(services);
                    var list = importer.Import(filePath);
                    await companyRepository.AddRange(list);

                    foreach (var company in list)
                    {
                        result.Add(company.Id);
                    }
                    return Ok(new { list = result });
                }
                return BadRequest(new { error = "No file" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int page, [FromQuery]int pageSize)
        {
            int count = await companyRepository.GetCompaniesCount();
            var list = await companyRepository.GetCompanies(page, pageSize);
            return Ok(new { count = count, page = page, pageSize = pageSize, data = list });
        }

    }
}