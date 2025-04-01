using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Member;

public class CreateMemberDTO
{
	[Required, EmailAddress, StringLength(100)]
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