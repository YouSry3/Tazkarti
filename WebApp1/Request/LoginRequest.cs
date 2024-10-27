using System.ComponentModel.DataAnnotations;

namespace WebApp1.Request
{
	public class LoginRequest
	{
		[Required]
		[MinLength(4, ErrorMessage = "Minamin Length 4!")]

		public string Email { get; set; }
		[Required]

		public string Password { get; set; }
	}
}
