using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using pr8.Classes;
using System.IO;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using static pr8.Classes.User;
using System.Text.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace pr8.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    /// 

    public partial class UserPage : Page
    {
        private User user { get; set; } = new User();
        string[] text_file; 
        public UserPage()
        {
            InitializeComponent();
            //десериализация из файла json
            var options = new JsonSerializerOptions
            {
                Converters ={
                    new JsonStringEnumConverter()
                },
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = true
            };
            string json = File.ReadAllText("user.json");
            User peopleFromJsonString = JsonSerializer.Deserialize<User>(json, options);

            // option 2, read the JSON from a stream
            User peopleFromJsonStream;
            using (FileStream jsonStream = new FileStream("user.json", FileMode.Open, FileAccess.Read))
            {
                peopleFromJsonStream = JsonSerializer.Deserialize<User>(jsonStream, options);
            }
            user.Username = peopleFromJsonString.Username;
            user.Status = peopleFromJsonStream.Status;
            user.LastLogin = Convert.ToDateTime(peopleFromJsonStream.LastLogin);
            //выгрузка из файла
            //string[] text = new string[4];
            //int count = 0;
            //StreamReader streamReader = File.OpenText("text.txt");
            //while (true)
            //{
            //    text[count]=streamReader.ReadLine();
            //    if (text[count]==null) break;
            //    count++;
            //}
            //user.Username = text[0];
            //user.Status = text[1];
            //user.LastLogin = Convert.ToDateTime(text[2]);
            DataContext = user;
            //user.Username = "Vasya";
            //user.Status = "Manager";
            //user.LastLogin = new DateTime(2007,07,07);
            //DataContext = user;
        }

        private void ChangePropertiesButton_Click(object sender, RoutedEventArgs e)
        {
            user.Username = "Natachya";
            user.Status = "Admin";
            user.LastLogin = DateTime.Now;
        }
        private void CheckProperties_Click(object sender, RoutedEventArgs e)
        {
            string pattern = "Username: {0}; Status: {1}; LastLogin: {2}";
            MessageBox.Show(string.Format(pattern, user.Username, user.Status, user.LastLogin));
        }
        private async void SaveProperties_Click(object sender, RoutedEventArgs e)
        {
            User ns = new User(user.Username, user.Status, user.LastLogin);
            //сохранение в текстовый файл
            string text = $"Username: {user.Username}; Status: {user.Status}; LastLogin: {user.LastLogin}";
            StreamWriter streamWriter = File.CreateText("text.txt");
            streamWriter.WriteLine(user.Username.ToString());
            streamWriter.WriteLine(user.Status.ToString());
            streamWriter.WriteLine(user.LastLogin.ToString());
            streamWriter.Close();
            //сохранение в JSON формат
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                await System.Text.Json.JsonSerializer.SerializeAsync<User>(fs, ns);
            }

                MessageBox.Show("Данные сохранены");
            
        }

        private void Username_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Username_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Any(symbol => !char.IsLetter(symbol)))
            {
                // то "запрещаем" ввод
                e.Handled = true;
            }
        }
    }
}
