using Android.App;
using Android.Widget;
using Android.OS;
using System.Data.SqlClient;
using System.IO;
using System;

namespace Android
{
    [Activity(Label = "Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
        async System.Threading.Tasks.Task LoginAsync(string userName, string passWord)
        {
            //string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-WebManager;Trusted_Connection=True;MultipleActiveResultSets=true";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    // Do work here; connection closed on following line.
            //}

            var dbFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var fileName = "Cats.db";
            var dbFullPath = Path.Combine(dbFolder, fileName);


            try
            {
                //using (var db = new ApplicationDbContext("a"))
                //{
                //    await db.Database.MigrateAsync(); //We need to ensure the latest Migration was added. This is different than EnsureDatabaseCreated.

                //    Cat catGary = new Cat() { CatId = 1, Name = "Gary", MeowsPerSecond = 5 };
                //    Cat catJack = new Cat() { CatId = 2, Name = "Jack", MeowsPerSecond = 11 };
                //    Cat catLuna = new Cat() { CatId = 3, Name = "Luna", MeowsPerSecond = 3 };

                //    List<Cat> catsInTheHat = new List<Cat>() { catGary, catJack, catLuna };

                //    if (await db.Cats.CountAsync() < 3)
                //    {
                //        await db.Cats.AddRangeAsync(catsInTheHat);
                //        await db.SaveChangesAsync();
                //    }

                //    var catsInTheBag = await db.Cats.ToListAsync();

                //    foreach (var cat in catsInTheBag)
                //    {
                //        textView.Text += $"{cat.CatId} - {cat.Name} - {cat.MeowsPerSecond}" + System.Environment.NewLine;
                //    }
                //}

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}

