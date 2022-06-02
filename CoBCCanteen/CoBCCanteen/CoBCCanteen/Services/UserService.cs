using System;
using System.IO;
using System.Threading.Tasks;
using CoBCCanteen.Models;
using SQLite;
using Xamarin.Essentials;

namespace CoBCCanteen.Services
{
	public class UserService
	{
		static SQLiteAsyncConnection db;

		static async Task Init()
		{
            if (db != null)
            {
				return;
            }
            else
            {
				var path = Path.Combine(FileSystem.AppDataDirectory, "Database.db");
				db = new SQLiteAsyncConnection(path);
				await db.CreateTableAsync<User>();
			}
		}

		public static async Task AddUser(string id, string email, string firstname, string lastname, string password)
        {
			await Init();

			bool isAdmin;

            if (id == "123456")
            {
				isAdmin = true;
            }
            else
            {
				isAdmin = false;
            }

			var newUser = new User
			{
				Id = id,
				Firstname = firstname,
				Lastname = lastname,
				Email = email,
				IsAdmin = isAdmin,
				Balance = 0,
				Password = password
			};

			await db.InsertAsync(newUser);
        }
	}
}

