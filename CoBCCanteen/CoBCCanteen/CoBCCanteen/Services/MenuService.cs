using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CoBCCanteen.Models;
using SQLite;

namespace CoBCCanteen.Services
{
	public class MenuService
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

			// Queries the database as to whether the "Menu" table exists.
			var tblExist = await db.ExecuteScalarAsync<string>("SELECT name FROM sqlite_master WHERE type='table' AND name='Menu';");
			if (tblExist == null)
			{
				await db.CreateTableAsync<MenuItem>();

				// Creates default menu items.
				List<MenuItem> menuItems = new List<MenuItem>()
				{
					new MenuItem()
					{
						Name = "Quarter Pounder & Cheese",
						Type = "Main",
						Price = 150,
						Description = "Quarter pounder beef burger in a sesame seeded bun, with an option processed cheese slice and a optional burger sauce. Say on arrival whether you want the optional items included or excluded from your meal.",
						Image = new Uri("https://rustlersonline.com/wp-content/uploads/2020/07/Quarter-Pounder-1180x878.png"),
						Stock = 250,
						Available = true,
						Calories = 526,
						Fats = 28.4f,
						Saturates = 11.8f,
						Suagrs = 6.6f,
						Salts = 2.8f,
						Allergens = "Barley, Eggs, Milk, Mustard, Sesame, Soya, Wheat"
					},

					new MenuItem()
					{
						Name = "Vegan Burger",
						Type = "Main",
						Price = 125,
						Description = "Vegan falafel burger in a sesame seed bun. It comes with lettuce, tomatoes, pickles, onions, and optional sauces of either ketchup (V) or mustard (V). Say on arrival whether you want the optional items included or excluded from your meal.",
						Image = new Uri("https://www.houseoffalafelmi.com/uploads/1/3/1/9/131915416/s935057928319119636_p65_i4_w690.png"),
						Stock = 50,
						Available = true,
						Calories = 429,
						Fats = 20f,
						Saturates = 6.6f,
						Suagrs = 10f,
						Salts = 2.2f,
						Allergens = "Mustard, Sesame, Wheat"
					},

					new MenuItem()
					{
						Name = "5x Mini Margherita Pizzas",
						Type = "Main",
						Price = 175,
						Description = "5x Mini pizza bases topped with tomato sauce, mozzarella cheese, Emmental cheese and Edam cheese sprinkled with dried parsley.",
						Image = new Uri("https://delico.ch/assets/images/Private-Label/Mini-Pizza-M-Budget/Mini-Pizza-Salami_Frei.png"),
						Stock = 100,
						Available = false,
						Rotation = "Friday",
						Calories = 345,
						Fats = 13.5f,
						Saturates = 4.5f,
						Suagrs = 4f,
						Salts = 1.5f,
						Allergens = "Cheese, Milk, Mozzarella Cheese, Soya, Wheat"
					},

					new MenuItem()
					{
						Name = "Ham & Cheese Toastie",
						Type = "Main",
						Price = 100,
						Description = "British ham, mature Cheddar cheese and optional ketchup or brown sauce on barmarked white bread. Say on arrival whether you want the optional items included or excluded from your meal.",
						Image = new Uri("https://www.pngmart.com/files/16/Grilled-Cheese-Sandwich-PNG-Image.png"),
						Stock = 100,
						Available = true,
						Calories = 354,
						Fats = 11f,
						Saturates = 6.5f,
						Suagrs = 4.1f,
						Salts = 1.3f,
						Allergens = "Milk, Wheat"
					},

					new MenuItem()
					{
						Name = "Chips",
						Type = "Snack",
						Price = 100,
						Description = "Made with premium potatoes such as the Russet Burbank and the Shepody. These chips are crispy and golden on the outside and fluffy on the inside.",
						Image = new Uri("https://thefryfix.com/wp-content/uploads/2019/03/CONE-OF-FRIES.png"),
						Stock = 300,
						Available = true,
						Calories = 337,
						Fats = 17f,
						Saturates = 1.5f,
						Suagrs = 0.6f,
						Salts = 0.62f,
						Allergens = "N/A"
					},

					new MenuItem()
					{
						Name = "Walkers Oven Baked Sea Salt",
						Type = "Snack",
						Price = 50,
						Description = "Walkers oven baked is a tasty crisp baked in the oven for extra crispy crunchiness and all the taste of Walkers. Crunchy Walkers baked contain 50 Percent less fat than potato crisps on average and you can't argue with that.",
						Image = new Uri("https://latinmarket.co.uk/1384-large_default/walkers-oven-baked-sea-salt.jpg"),
						Stock = 150,
						Available = true,
						Calories = 109,
						Fats = 3.4f,
						Saturates = 0.3f,
						Suagrs = 1.5f,
						Salts = 0.3f,
						Allergens = "Barley, Celery, Gluten, Milk, Mustard, Soya, Wheat"
					},

					new MenuItem()
					{
						Name = "Dairy Milk",
						Type = "Snack",
						Price = 50,
						Description = "Classic bar of deliciously creamy Cadbury Dairy Milk milk chocolate, made with fresh milk from the British Isles and Ireland. A mouthful of \"mmmm\" in every piece!",
						Image = new Uri("http://images.sweetauthoring.com/product/80649.png"),
						Stock = 75,
						Available = true,
						Calories = 240,
						Fats = 14f,
						Saturates = 8.3f,
						Suagrs = 25f,
						Salts = 0.06f,
						Allergens = "Milk"
					},

					new MenuItem()
					{
						Name = "Water",
						Type = "Drink",
						Container = "Bottle",
						Price = 50,
						Description = "Made from British spring water which is vapour-distilled before electrolytes are added. It has a distinctive, crisp, clean taste.",
						Image = new Uri("https://pngimg.com/uploads/water_bottle/water_bottle_PNG98959.png"),
						Stock = 250,
						Available = true,
						Calories = 0,
						Fats = 0f,
						Saturates = 0f,
						Suagrs = 0f,
						Salts = 0f,
						Allergens = "N/A"
					},

					new MenuItem()
					{
						Name = "Pepsi Max",
						Type = "Drink",
						Container = "Can",
						Price = 50,
						Description = "Maximum taste. No sugar.",
						Image = new Uri("https://latinmarket.co.uk/1384-large_default/walkers-oven-baked-sea-salt.jpg"),
						Stock = 150,
						Available = true,
						Calories = 1,
						Fats = 0f,
						Saturates = 0f,
						Suagrs = 0f,
						Salts = 0f,
						Allergens = "N/A"
					},

					new MenuItem()
					{
						Name = "Tea",
						Type = "Takeaway Cup",
						Price = 50,
						Description = "Tea made with PG tips tea bags. Option of milk, ask on arrival.",
						Image = new Uri("https://www.nicepng.com/png/full/315-3153101_paper-cup-transparent-background-coffee-cup-png.png"),
						Stock = 75,
						Available = true,
						Calories = 15,
						Fats = 0.5f,
						Saturates = 0.3f,
						Suagrs = 1.5f,
						Salts = 0.05f,
						Allergens = "Milk"
					},
				};

				await db.InsertAllAsync(menuItems);
			}
		}
	}
}

