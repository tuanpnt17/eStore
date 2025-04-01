using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public class MemberDTO
{
    public int MemberId { get; set; }
    [EmailAddress, Required]
    public string Email { get; set; }

    [Required, StringLength(40)]
    public string CompanyName { get; set; }

    [Required, StringLength(15)]
    public string City { get; set; }

    [Required, StringLength(15)]
    public string Country { get; set; }

    [Required, StringLength(30)]
    public string Password { get; set; }
    public string Role { get; set; }
}
