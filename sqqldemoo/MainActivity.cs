using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using SQLite;
using System;

namespace sqqldemoo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public Button storeButton;
        public TextView resultView;
        public Button deleteButton;
        static string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        SQLiteConnection db = new SQLiteConnection(System.IO.Path.Combine(folder, "myDb. db"));
       
        public EditText userInput;
        Person person = new Person();

     
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            db.CreateTable<Person>();
            Uireferences();
            storeButton.Click += StoreButton_Click;
            deleteButton.Click += DeleteButton_Click;
            db.Commit();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var deleteID = db.Delete<Person>(int.Parse(userInput.Text));
            resultView.Text = "deleted : id " +deleteID;
        }

        private void StoreButton_Click(object sender, EventArgs e)
        {
            

            
            person.Name = userInput.Text;
            int id = db.Insert(person);
            resultView.Text = "Inserted  with success: id " + id;


            //Person personFromTheDatabase = db.Get<Person>(1);
            //resultView.Text = "Inserted with success: id " + personFromTheDatabase.Id + " and the name " + personFromTheDatabase.Name;

            var listOfPerson = db.Table<Person>().ToList();
            //previous was db.Get<type>();
            for(int i=0;i<listOfPerson.Count;i++)
            resultView.Text = resultView.Text+ " Inserted  with success: id " +listOfPerson[i].Name+ "\n";

        }

        public void Uireferences()
        {
            storeButton = (Button)FindViewById(Resource.Id.storeButton);
            resultView = FindViewById<TextView>(Resource.Id.resultTextView);
            userInput = (EditText)FindViewById(Resource.Id.inputDataET);
            deleteButton = (Button)FindViewById(Resource.Id.deleteButton);

        }

    }


}

