using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace companyApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CompanyController : ControllerBase
  {
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
      _companyService = companyService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCompanyDto>>>> GetAll()
    {
      return Ok(await _companyService.GetAllCompanies());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCompanyDto>>> GetCompany(int id)
    {
      var response = await _companyService.GetCompanyById(id);
      if (response.Data is null) return NotFound(response);
      return Ok(response); 
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCompanyDto>>>> AddCompany(AddCompanyDto newCompany)
    {
      return Ok(await _companyService.AddCompany(newCompany));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetCompanyDto>>> UpdateCompany(UpdateCompanyDto updatedCompany)
    {
      var response = await _companyService.UpdateCompany(updatedCompany);
      if (response.Data is null) return NotFound(response);
      return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCompanyDto>>> DeleteCompany(int id)
    {
      var response = await _companyService.DeleteCompany(id);
      if (response.Data is null) return NotFound(response);
      return Ok(response);
    }
  }
}