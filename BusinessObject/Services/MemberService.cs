using BusinessObject.Contracts;
using BusinessObject.Models;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessObject.Services;

public class MemberService(IUnitOfWork unitOfWork) : IMemberService 
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
            CompanyName = member.CompanyName
        };
    }
    public async Task Register(MemberDTO memberDTO)
    {
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
        Member member = new Member
        {
            Email = memberDTO.Email,
            Password = memberDTO.Password,
            City = memberDTO.City,
            Country = memberDTO.Country,
            CompanyName = memberDTO.CompanyName
        };
        unitOfWork.MemberRepository.UpdateProfile(member);
        await unitOfWork.Complete();
    }

}
