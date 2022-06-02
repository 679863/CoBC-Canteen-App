using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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

				List<User> campuses = new List<User>();

				var campusCollegeGreen = new User
				{
					Id = "000001",
					Firstname = "College",
					Lastname = "Green",
					Email = "College.Green@cityofbristol.ac.uk",
					IsAdmin = true,
					Balance = 0,
					Password = "f8265568950e7208b0d7cb502bab97c606ef3253fe581c28ada4326ebe3c2378"
				};

				campuses.Add(campusCollegeGreen);

				var campusAshleyDown = new User
				{
					Id = "000002",
					Firstname = "Ashley",
					Lastname = "Down",
					Email = "Ashley.Down@cityofbristol.ac.uk",
					IsAdmin = true,
					Balance = 0,
					Password = "633c03e1cb12134905865a6bbd4027df5eae38f16bc35b53575ba73f5dc40969"
				};

				campuses.Add(campusAshleyDown);

				var campusSBSA = new User
				{
					Id = "000003",
					Firstname = "SBSA",
					Lastname = "SBSA",
					Email = "SBSA@cityofbristol.ac.uk",
					IsAdmin = true,
					Balance = 0,
					Password = "f38164fd8ea94233313d28e03af8d2556789aed0d30556705b65a6378204ead8"
				};

				campuses.Add(campusSBSA);

				await db.InsertAllAsync(campuses);
			}
		}

		public static async Task AddUser(string id, string email, string firstname, string lastname, string password)
        {
			await Init();

            if (IsUserExisting(id, email) == false)
            {
				var newUser = new User
				{
					Id = id,
					Firstname = firstname,
					Lastname = lastname,
					Email = email,
					IsAdmin = false,
					Balance = 0,
					Password = password
				};

				await db.InsertAsync(newUser);
			}
        }

		public static bool IsUserExisting(string id, string email)
        {
			bool isExisting = true;

			var checkID = db.Table<User>().Where(u => u.Id == id).FirstOrDefaultAsync();
            if (checkID == null)
            {
				var checkEmail = db.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
                if (checkEmail == null)
                {
					isExisting = false;
                }
                else
                {
					throw new ExistingEmail(email);
                }
            }
            else
            {
				throw new ExistingID(id);
            }

			return isExisting;
        }

		public static string HashPassword(string password)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					sb.Append(bytes[i].ToString("X2"));
				}

				return sb.ToString();
			}
		}
	}
}

