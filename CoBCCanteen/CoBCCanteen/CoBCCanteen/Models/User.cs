using System;
using SQLite;

namespace CoBCCanteen.Models
{
	[Table("Users")]
	public class User
	{
		[PrimaryKey, MaxLength(6), Column("Id")]
		public int Id { get; set; }
		[MaxLength(64), Column("Firstname")]
		public string Firstname { get; set; }
		[MaxLength(64), Column("Lastname")]
		public string Lastname { get; set; }
		[MaxLength(254), Column("Email")]
		public string Email { get; set; }
		[Column("Admin")]
		public bool IsAdmin { get; set; }
		[Column("Balance")]
		public int Balance { get; set; }
		[MaxLength(64), Column("Password")]
		public string Password { get; set; }
	}
}

