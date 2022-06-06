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

		static async Task Init()
		{
			Console.WriteLine("INIT start");
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Database.db");

			if ((db == null) || !(File.Exists(path)))
			{
				Console.WriteLine("DB or File exists null");
				db = new SQLiteAsyncConnection(path);
				Console.WriteLine("created conn");
			}

            Console.WriteLine("Querying for table");
			var tblExist = await db.ExecuteScalarAsync<string>("SELECT name FROM sqlite_master WHERE type='table' AND name='Users';");
			if (tblExist == null)
			{
                Console.WriteLine("tbl no exist. creating table");
				await db.CreateTableAsync<User>();

                Console.WriteLine("tbl created. creating pre users");
				List<User> campuses = new List<User>()
				{
					new User()
					{
						Id = 000001,
						Firstname = "College",
						Lastname = "Green",
						Email = "College.Green@cityofbristol.ac.uk",
						IsAdmin = true,
						Balance = 0,
						Password = "f8265568950e7208b0d7cb502bab97c606ef3253fe581c28ada4326ebe3c2378"
					},

					new User()
                    {
						Id = 000002,
						Firstname = "Ashley",
						Lastname = "Down",
						Email = "Ashley.Down@cityofbristol.ac.uk",
						IsAdmin = true,
						Balance = 0,
						Password = "633c03e1cb12134905865a6bbd4027df5eae38f16bc35b53575ba73f5dc40969"
					},

					new User()
                    {
						Id = 000003,
						Firstname = "SBSA",
						Lastname = "SBSA",
						Email = "SBSA@cityofbristol.ac.uk",
						IsAdmin = true,
						Balance = 0,
						Password = "f38164fd8ea94233313d28e03af8d2556789aed0d30556705b65a6378204ead8"
					}
				};

                Console.WriteLine("users created. adding users");
				await db.InsertAllAsync(campuses);
                Console.WriteLine("users added");
            }
		}

		public static async Task AddUser(int id, string email, string firstname, string lastname, string password)
        {
			await DeleteDatabse();
            Console.WriteLine("DB gone. Starting INIT");

			await Init();
            Console.WriteLine("Worked INIT");

			bool isUserExisting = await (IsUserExisting(id, email));
            Console.WriteLine(isUserExisting);
            if (!isUserExisting)
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

		public static async Task<bool> IsUserExisting(int id, string email)
        {
            Console.WriteLine("Check user");
			bool isExisting = true;

            Console.WriteLine("Querying");
			int countID = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Users WHERE Id = ?", id);

            Console.WriteLine(countID);
    //        if (countID == null)
    //        {
				//var countEmail = await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Users WHERE Email = ?", email);
    //            if (countEmail == null)
    //            {
				//	isExisting = false;
    //            }
    //            else if (countEmail != null)
    //            {
				//	throw new ExistingEmail(email);
				//}
    //        }
    //        else if (countID != null)
    //        {
    //            throw new ExistingID(id.ToString());
    //        }

            return isExisting;
        }

		public static async Task DeleteDatabse()
        {
            Console.WriteLine("Starting INIT");
			await Init();
            Console.WriteLine("Comp INIT. Dropping table");
			await db.DropTableAsync<User>();
            Console.WriteLine("Table dropped");
			db = null;
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

