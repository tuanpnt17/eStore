using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Member;

public class UpdateMemberDTO
{
	public int MemberId { get; set; }

	[EmailAddress, StringLength(100)]
	public string? Email { get; set; }

	[StringLength(40)]
	public string? CompanyName { get; set; }

	[StringLength(15)]
	public string? City { get; set; }

	[StringLength(15)]
	public string? Country { get; set; }

	[StringLength(30)]
	public string? Password { get; set; }
}