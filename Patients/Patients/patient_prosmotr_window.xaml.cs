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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Patients
{
    /// <summary>
    /// Логика взаимодействия для patient_prosmotr_window.xaml
    /// </summary>
    public partial class patient_prosmotr_window : Window
    {
        string id = "";
        static string connBD = ConnectionBD_path.Path();
        MySqlConnection connection = new MySqlConnection(connBD);
        public patient_prosmotr_window(string id1)
        {
            InitializeComponent();
            id = id1;

            connection.Open();
            string zapros = "SELECT * FROM covid_passport.patients where id_patients =" + id1;
            MySqlCommand command = new MySqlCommand(zapros, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string fio = reader.GetString(1) + " " + reader.GetString(2) + " " + reader.GetString(3);
            FIO.Content = fio;
            birthday.Content = "Дата рождения: " + reader.GetString(4).Split(' ')[0];
            snils.Content = "СНИЛС: " + reader.GetString(5);
            Address.Content = "Адрес: " + reader.GetString(6);

            connection.Close();
            connection.Open();
            string zapros2 = "SELECT * FROM covid_passport.patients where id_patients=" + id1;
            MySqlCommand command2 = new MySqlCommand(zapros2, connection);
            MySqlDataReader reader2 = command2.ExecuteReader();
            reader2.Read();
            int status_vaccine = Convert.ToInt32(reader2.GetString(7));
            if (status_vaccine == 0)
            {
                covid_pasport.Content = "Вы не прошли полную вакцинацию";
                zapic_vaccine_button.IsEnabled = true;
                zapic_vaccine_button.Foreground = Brushes.Black;
            }
            else
            {
                covid_pasport.Content = "Вы получили covid-паспорт";
                covid_pasport.Foreground = Brushes.Green;
                raspechatat_button.IsEnabled = true;
                raspechatat_button.Foreground = Brushes.Black;
            }
        }

        private void zapic_vaccine_button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
           // patient_zapic_vaccine_window z = new patient_zapic_vaccine_window(id);
           // z.Show();
        }

        private void raspechatat_button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
           // kovid_passport_window k = new kovid_passport_window(id);
           // k.Show();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
           // patient_menu_window pr = new patient_menu_window(id);
           // pr.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
