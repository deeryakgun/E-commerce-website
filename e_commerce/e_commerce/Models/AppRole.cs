using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace e_commerce.Models
{
	public class AppRole : IdentityRole<int>
	{
    }
}

