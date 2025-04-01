﻿namespace BusinessObject.Models.Member;

public class MemberViewDTO
{
	public int MemberId { get; set; }
	public required string Email { get; set; }
	public required string CompanyName { get; set; }
	public required string City { get; set; }
	public required string Country { get; set; }
	public required string Password { get; set; } 
}