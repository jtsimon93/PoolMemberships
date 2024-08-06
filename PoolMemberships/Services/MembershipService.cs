using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OfficeOpenXml;
using PoolMemberships.Dtos;
using PoolMemberships.Models;
using PoolMemberships.Repositories;

namespace PoolMemberships.Services;

public class MembershipService : IMembershipService
{
    private readonly IMapper _mapper;
    private readonly IMembershipRepository _membershipRepository;

    public MembershipService(IMembershipRepository membershipRepository, IMapper mapper)
    {
        _membershipRepository = membershipRepository;
        _mapper = mapper;
    }

    public async Task<Membership> AddAsync(Membership membership)
    {
        return await _membershipRepository.AddAsync(membership);
    }

    public async Task<IEnumerable<Membership>> GetAllAsync()
    {
        return await _membershipRepository.GetAllAsync();
    }

    public async Task<IEnumerable<MembershipWithPersonDto>> GetAllWithPersonAsync()
    {
        var memberships = await _membershipRepository.GetAllWithPersonAsync();
        return _mapper.Map<IEnumerable<MembershipWithPersonDto>>(memberships);
    }

    public async Task<MembershipWithPersonDto?> GetWithPersonAsync(int id)
    {
        var membership = await _membershipRepository.GetWithPersonAsync(id);

        return membership == null ? null : _mapper.Map<MembershipWithPersonDto>(membership);
    }

    public async Task<Membership> UpdateAsync(int membershipId, UpdateMembershipDto membershipDto)
    {
        var membership = await _membershipRepository.GetAsync(membershipId);

        if (membership == null) throw new KeyNotFoundException($"Membership with id {membershipId} not found");

        _mapper.Map(membershipDto, membership);
        return await _membershipRepository.UpdateAsync(membership);
    }
    
    public async Task<IEnumerable<MembershipWithPersonDto>> SearchAsync(MembershipSearchCriteriaDto searchDto)
    {
        var memberships = await _membershipRepository.SearchAsync(searchDto);
        return _mapper.Map<IEnumerable<MembershipWithPersonDto>>(memberships);
    }

    public async Task<byte[]> GenerateExcelFile()
    {
        var memberships = await _membershipRepository.GetAllWithPersonAsync();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Memberships");
        
        // Headers
        worksheet.Cells[1, 1].Value = "Membership ID";
        worksheet.Cells[1, 2].Value = "Start Date";
        worksheet.Cells[1, 3].Value = "End Date";
        worksheet.Cells[1, 4].Value = "Membership Status";
        worksheet.Cells[1, 5].Value = "Key Fob ID";
        worksheet.Cells[1, 6].Value = "First Name";
        worksheet.Cells[1, 7].Value = "Last Name";
        worksheet.Cells[1, 8].Value = "Email";
        worksheet.Cells[1, 9].Value = "Phone Number";
        worksheet.Cells[1, 10].Value = "Street Address";
        worksheet.Cells[1, 11].Value = "City";
        worksheet.Cells[1, 12].Value = "State";
        worksheet.Cells[1, 13].Value = "Zip Code";
        
        // Bold the headers
        int col = 1;
        while (col <= 13)
        {
            worksheet.Cells[1, col].Style.Font.Bold = true;
            col++;
        }
        
        // Starting row
        int row = 2;

        foreach (var membership in memberships)
        {
            worksheet.Cells[row, 1].Value = membership.MembershipId;
            worksheet.Cells[row, 2].Style.Numberformat.Format = "mm/dd/yyyy";
            worksheet.Cells[row, 2].Value = membership.StartDate;
            worksheet.Cells[row, 3].Style.Numberformat.Format = "mm/dd/yyyy";
            worksheet.Cells[row, 3].Value = membership.EndDate;
            worksheet.Cells[row, 4].Value = membership.Active == true ? "Active" : "Inactive";
            worksheet.Cells[row, 5].Value = membership.KeyFobId;
            worksheet.Cells[row, 6].Value = membership.Person.FirstName;
            worksheet.Cells[row, 7].Value = membership.Person.LastName;
            worksheet.Cells[row, 8].Value = membership.Person.Email;
            worksheet.Cells[row, 9].Value = membership.Person.Phone;
            worksheet.Cells[row, 10].Value = membership.Person.Address;
            worksheet.Cells[row, 11].Value = membership.Person.City;
            worksheet.Cells[row, 12].Value = membership.Person.State;
            worksheet.Cells[row, 13].Value = membership.Person.Zip;
            row++;
        }
        
        // Auto fit columns
        worksheet.Cells.AutoFitColumns();

        return await package.GetAsByteArrayAsync();
    }
}