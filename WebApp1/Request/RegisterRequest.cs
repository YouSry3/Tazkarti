using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApp1.Models;

namespace WebApp1.Request
{
	public class RegisterRequest
	{
		
		[Required]
        [MinLength(4, ErrorMessage = "Minamin Length 4!")]
		[MaxLength(16, ErrorMessage = "maxinamm Length 16!")]
        public string Name { get; set; }
		[Required]
		[MinLength(4,ErrorMessage ="Minamin Length 4!")]
        [MaxLength(25, ErrorMessage = "maxinamm Length 16!")]


        public string Email { get; set; }
		[Required]
        [MinLength(4, ErrorMessage = "Minamin Length 4!")]
        [MaxLength(16, ErrorMessage = "maxinamm Length 16!")]


        public string Password { get; set; }
		[Required]
		
		public Role role { get; set; }

	}
}
