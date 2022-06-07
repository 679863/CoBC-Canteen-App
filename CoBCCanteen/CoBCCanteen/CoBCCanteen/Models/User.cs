using System;
using SQLite;

namespace CoBCCanteen.Models
{
	[Table("Users")]
	public class User
	{
		[PrimaryKey, MaxLength(6), Column("Id"), NotNull]
		public string Id { get; set; }
		[MaxLength(64), Column("Firstname"), NotNull]
		public string Firstname { get; set; }
		[MaxLength(64), Column("Lastname"), NotNull]
		public string Lastname { get; set; }
		[MaxLength(254), Column("Email"), NotNull]
		public string Email { get; set; }
		[Column("Admin"), NotNull]
		public bool IsAdmin { get; set; }
		[Column("Balance"), NotNull]
		public int Balance { get; set; }
		[MaxLength(64), Column("Password"), NotNull]
		public string Password { get; set; }
	}
}

