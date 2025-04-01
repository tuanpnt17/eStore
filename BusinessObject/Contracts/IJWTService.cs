using BusinessObject.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Contracts
{
    public interface IJWTService
    {
        string GenerateJwtToken(MemberDTO user);

    }
}
