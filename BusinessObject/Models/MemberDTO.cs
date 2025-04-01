using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class MemberDTO 
{
    public int MemberId { get; set; }
    public required string Email { get; set; }

    [Required, StringLength(40)]
    public required string CompanyName { get; set; }

    [Required, StringLength(15)]
    public required string City { get; set; }

    [Required, StringLength(15)]
    public required string Country { get; set; }

    [Required, StringLength(30)]
    public required string Password { get; set; }
}
