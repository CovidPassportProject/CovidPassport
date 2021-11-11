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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string id1 = "";
        static string connBD = ConnectionBD_path.Path();
        MySqlConnection connection = new MySqlConnection(connBD);
        public MainWindow(string id)
        {
            InitializeComponent();
            InitializeComponent();
            id1 = id;

            connection.Open();
            string zapros2 = "SELECT * FROM covid_passport.patients where id_patients=" + id;
            MySqlCommand command2 = new MySqlCommand(zapros2, connection);
            MySqlDataReader reader2 = command2.ExecuteReader();
            reader2.Read();
            int status_vaccine = Convert.ToInt32(reader2.GetString(7));
            if (status_vaccine == 0)
            {
                zapic_vaccine_button.IsEnabled = true;
                zapic_vaccine_button.Foreground = Brushes.Black;
            }
            else
            {
                kovid_passport_button.IsEnabled = true;
                kovid_passport_button.Foreground = Brushes.Black;
            }
        }

        private void zapic_vaccine_button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
           // patient_zapic_vaccine_window z = new patient_zapic_vaccine_window(id1);
           // z.Show();
        }

        private void prosmotr_button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            patient_prosmotr_window pr = new patient_prosmotr_window(id1);
            pr.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void kovid_passport_button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
           // kovid_passport_window k = new kovid_passport_window(id1);
           // k.Show();
        }
    }
}
