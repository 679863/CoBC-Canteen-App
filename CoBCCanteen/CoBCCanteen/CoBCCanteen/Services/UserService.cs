using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CoBCCanteen.Models;
using SQLite;

namespace CoBCCanteen.Services
{
	public class UserService
	{
		static SQLiteAsyncConnection db;

		// Creates database and table if it is not existing.
		static async Task Init()
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Database.db");

			if ((db == null) || !(File.Exists(path)))
			{
				db = new SQLiteAsyncConnection(path);
			}

			// Queries the database as to whether the "Users" table exists.
			var tblExist = await db.ExecuteScalarAsync<string>("SELECT name FROM sqlite_master WHERE type='table' AND name='Users';");
			if (tblExist == null)
			{
				await db.CreateTableAsync<User>();

				// Creates default admin users.
				List<User> campuses = new List<User>()
				{
					new User()
					{
						Id = "000001",
						Firstname = "College",
						Lastname = "Green",
						Email = "College.Green@cityofbristol.ac.uk",
						IsAdmin = true,
						Balance = 0,
						Password = "f8265568950e7208b0d7cb502bab97c606ef3253fe581c28ada4326ebe3c2378".ToUpper()
					},

					new User()
                    {
						Id = "000002",
						Firstname = "Ashley",
						Lastname = "Down",
						Email = "Ashley.Down@cityofbristol.ac.uk",
						IsAdmin = true,
						Balance = 0,
						Password = "633c03e1cb12134905865a6bbd4027df5eae38f16bc35b53575ba73f5dc40969".ToUpper()
					},

					new User()
                    {
						Id = "000003",
						Firstname = "SBSA",
						Lastname = "SBSA",
						Email = "SBSA@cityofbristol.ac.uk",
						IsAdmin = true,
						Balance = 0,
						Password = "f38164fd8ea94233313d28e03af8d2556789aed0d30556705b65a6378204ead8".ToUpper()
					}
				};

				await db.InsertAllAsync(campuses);
            }
		}

		// Adds user to database.
		public static async Task AddUser(string _id, string _email, string _firstname, string _lastname, string _password)
        {
			//await DeleteDatabse();
			await Init();

			// Stores the user's first and last name in a correct format.
			string firstname = _firstname[0].ToString().ToUpper() + _firstname.Substring(1);
			string lastname = _lastname[0].ToString().ToUpper() + _lastname.Substring(1);

			// Checks if a user with entered id or email already exists.
			bool isUserExisting = await (IsUserExisting(_id, _email));
            if (!isUserExisting)
            {
				var newUser = new User
				{
					Id = _id,
					Firstname = firstname,
					Lastname = lastname,
					Email = _email,
					IsAdmin = false,
					Balance = 0,
					Password = _password
				};

				await db.InsertAsync(newUser);
			}
        }

		// Checks if a user with entered id or email already exists.
		public static async Task<bool> IsUserExisting(string id, string email)
        {
			bool isExisting = true;
			var countID = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Users WHERE Id = ?", id);
			var countEmail = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Users WHERE Email = ?", email);

            if (countID == 0)
            {
                if (countEmail == 0)
                {
					isExisting = false;
                }
                else if (countEmail != 0)
                {
					// Custom exception.
					throw new ExistingEmail(email);
                }
            }
            else if (countID != 0)
            {
				// Custom exception.
				throw new ExistingID(id);
            }

			return isExisting;
        }

		public static async Task<User> Login(string id, string password)
        {
			//await DeleteDatabse();
			await Init();
			User user = await db.Table<User>().Where(x => (x.Id == id) && (x.Password == password)).FirstOrDefaultAsync();
			return user;
        }

		public static Task<User> GetUserByID(string id)
        {
			return db.Table<User>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

		public static async Task<User> UpdateUserAndGet(User user)
        {
			await db.UpdateAsync(user);
			return await db.Table<User>().Where(x => x.Id == user.Id).FirstOrDefaultAsync();
        }

		// Called once if changes are made to the database, so the database updates.
		public static async Task DeleteDatabse()
        {
			await Init();
			await db.DropTableAsync<User>();
			db = null;
        }

		// Hashes the user's password in preparation for storing it in the database, and comparing password when loggin in.
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

