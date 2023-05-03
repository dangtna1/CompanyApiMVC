using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace companyApi.Services.CompanyService
{
  public class CompanyService : ICompanyService
  {
    // private static List<Company> companies = new List<Company> {
    //     new Company(),
    //     new Company {Id = 1, Name = "iDeaLogic"}
    // };
    private readonly IMapper _mapper;
    private readonly CompanyDataContext _context;

    public CompanyService(IMapper mapper, CompanyDataContext context)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetCompanyDto>>> AddCompany(AddCompanyDto newCompany)
    {
      var serviceResponse = new ServiceResponse<List<GetCompanyDto>>();
      var company = _mapper.Map<Company>(newCompany);
      company.Id = _context.Companies.Max(companyItem => companyItem.Id) + 1; //good job

      //Erase Id property or change isIdentify in sqlServer
      _context.Companies.Add(company);
      _context.SaveChanges();
      // var dbCompanies = await _context.Companies.ToListAsync();
      // serviceResponse.Data = dbCompanies.Select(companyItem => _mapper.Map<GetCompanyDto>(companyItem)).ToList();
      serviceResponse.Data = await _context.Companies.Select(companyItem =>
        _mapper.Map<GetCompanyDto>(companyItem)).ToListAsync();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCompanyDto>>> DeleteCompany(int id)
    {
      var serviceResponse = new ServiceResponse<List<GetCompanyDto>>();
      try
      {
        var company = await _context.Companies.FirstOrDefaultAsync(companyItem => companyItem.Id == id);
        if (company is null)
        {
          throw new Exception($"Company with Id = {id} is not found");
        }
        _context.Companies.Remove(company);
        _context.SaveChanges();
        serviceResponse.Data = await _context.Companies.Select(companyItem =>
           _mapper.Map<GetCompanyDto>(companyItem)).ToListAsync();
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCompanyDto>>> GetAllCompanies()
    {
      var serviceResponse = new ServiceResponse<List<GetCompanyDto>>();
      // var dbCompanies = await _context.Companies.ToListAsync(); //get data from database and turn it to a list
      serviceResponse.Data = await _context.Companies.Select(companyItem =>
        _mapper.Map<GetCompanyDto>(companyItem)).ToListAsync();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCompanyDto>> GetCompanyById(int id)
    {
      var serviceResponse = new ServiceResponse<GetCompanyDto>();
      try
      {
        var dbCompany = await _context.Companies.FirstOrDefaultAsync(companyItem => companyItem.Id == id);
        if (dbCompany is null)
        {
          throw new Exception($"Company with Id = {id} is not found");
        }
        serviceResponse.Data = _mapper.Map<GetCompanyDto>(dbCompany);
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCompanyDto>> UpdateCompany(UpdateCompanyDto updatedCompany)
    {
      var serviceResponse = new ServiceResponse<GetCompanyDto>();
      try
      {
        //pointer?
        var dbCompany = await _context.Companies.FirstOrDefaultAsync(companyItem => companyItem.Id == updatedCompany.Id); //take company directly?
        if (dbCompany is null)
        {
          throw new Exception($"Company with Id = {updatedCompany.Id} is not found");
        }
        dbCompany.Name = updatedCompany.Name;
        dbCompany.isActive = updatedCompany.isActive;
        dbCompany.Rating = updatedCompany.Rating;
        _context.SaveChanges();
        serviceResponse.Data = _mapper.Map<GetCompanyDto>(dbCompany);
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }
  }
}