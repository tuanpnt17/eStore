using BusinessObject.Contracts;
using BusinessObject.Models;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Services;

public class MemberService(IUnitOfWork unitOfWork, IConfiguration configuration) : IMemberService 
{
    public async Task<MemberDTO> GetMember(int id)
    {
        var member = await unitOfWork.MemberRepository.GetMemberById(id);
        if (member == null)
        {
            return null;
        }
        
        return new MemberDTO
        {
            MemberId = member.MemberId,
            Email = member.Email,
            Password = member.Password,
            City = member.City,
            Country = member.Country,
            CompanyName = member.CompanyName
        };
    }

    public async Task<MemberDTO> Login(string email, string password)
    {
        var adminEmail = configuration["AdminAccount:Email"];
        var adminPassword = configuration["AdminAccount:Password"];
        if (email == adminEmail && password == adminPassword)
        {
            return new MemberDTO
            {
                Email = configuration["AdminAccount:AccountId"],
                Password = configuration["AdminAccount:Password"],
                Role = "Admin"
            };
        }
        var member = await unitOfWork.MemberRepository.Login(email, password);
        if (member == null)
        {
            return null;
        }
        return new MemberDTO
        {
            Email = member.Email,
            Password = member.Password,
            City = member.City,
            Country = member.Country,
            CompanyName = member.CompanyName,
            Role = "Member",
            MemberId = member.MemberId
        };
    }
    public async Task Register(MemberDTO memberDTO)
    {
        if(unitOfWork.MemberRepository.CheckExistingEmail(memberDTO.Email))
        {
            throw new Exception("Email already exists");
        }
        Member member = new Member
        {
            Email = memberDTO.Email,
            Password = memberDTO.Password,
            City = memberDTO.City,
            Country = memberDTO.Country,
            CompanyName = memberDTO.CompanyName
        };
        unitOfWork.MemberRepository.Register(member);
        await unitOfWork.Complete();
    }

    public async Task UpdateProfile(MemberDTO memberDTO)
    {
        var existingMember = await unitOfWork.MemberRepository.GetMemberById(memberDTO.MemberId);

        //existingMember.Email = memberDTO.Email;
        existingMember.Password = memberDTO.Password;
        existingMember.City = memberDTO.City;
        existingMember.Country = memberDTO.Country;
        existingMember.CompanyName = memberDTO.CompanyName;

        unitOfWork.MemberRepository.UpdateProfile(existingMember);
        await unitOfWork.Complete();
    }

}
