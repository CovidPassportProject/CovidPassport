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

namespace Patients2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string connBD = ConnectionBD_path.Path();
        MySqlConnection connection = new MySqlConnection(connBD);
        string id = "";
        List<string> id_name_polyclinic = new List<string>();
        List<string> id_vaccine = new List<string>();
        public MainWindow(string id1)
        {
            InitializeComponent();
            date_vaccine1.SelectedDate = DateTime.Today;
            id = id1;
            id_vaccine.Clear();
            connection.Open();
            string Query1 = "SELECT * FROM covid_passport.type_vaccine";
            MySqlCommand command1 = new MySqlCommand(Query1, connection);
            MySqlDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                id_vaccine.Add(reader1.GetString(0));
                vibor_vaccine.Items.Add(reader1.GetString(1));
            }
            connection.Close();

            List<string> id_area = new List<string>();
            connection.Open();
            string Query2 = "SELECT * FROM covid_passport.area";
            MySqlCommand command2 = new MySqlCommand(Query2, connection);
            MySqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                id_area.Add(reader2.GetString(0));
                vibor_rajona.Items.Add(reader2.GetString(1));
            }
            connection.Close();

            connection.Open();
            string zapros2 = "SELECT * FROM covid_passport.patients where id_patients=" + id;
            MySqlCommand command3 = new MySqlCommand(zapros2, connection);
            MySqlDataReader reader3 = command3.ExecuteReader();
            reader3.Read();
            int status_vaccine = Convert.ToInt32(reader3.GetString(7));
            if (status_vaccine == 0)
            {
                ok_button.IsEnabled = true;
                ok_button.Foreground = Brushes.Black;
            }
            connection.Close();
        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {

            string theDate = date_vaccine1.SelectedDate.Value.Date.ToShortDateString();
            string date1 = theDate.Split('.')[2] + "." + theDate.Split('.')[1] + "." + theDate.Split('.')[0];
            DateTime dt = date_vaccine1.SelectedDate.Value.Date;
            dt = dt.AddDays(21);
            string date4 = dt.ToString().Split(' ')[0];
            date_vaccine2.Content += date4;
            string date2 = date4.ToString().Split('.')[2] + "." + date4.ToString().Split('.')[1] + "." + date4.ToString().Split('.')[0];

            connection.Close();
            connection.Open();
            string zapros = "insert into zapic_vaccine (id_patients,date_vaccine1,date_vaccine2,id_polyclinic,id_type_vaccine) values('" + id + "','" + date1 + "','" + date2 + "','" + id_name_polyclinic[vibor_polikliniki.SelectedIndex] + "','" + id_vaccine[vibor_vaccine.SelectedIndex] + "')";
            //MessageBox.Show(zapros);
            MySqlCommand command = new MySqlCommand(zapros, connection);
            MySqlDataReader read = command.ExecuteReader();
            read.Read();
            connection.Close();

            connection.Open();
            string zapros2 = "update patients set status_vaccine= 1 where id_patients= " + id;
            //MessageBox.Show(zapros2);
            MySqlCommand command2 = new MySqlCommand(zapros2, connection);
            MySqlDataReader read2 = command2.ExecuteReader();
            read2.Read();
            connection.Close();
            MessageBox.Show("Вы успешно отметили вакцинации!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            //this.Close();
            date_vaccine1.IsEnabled = false;
            vibor_rajona.IsEnabled = false;
            vibor_vaccine.IsEnabled = false;
            vibor_polikliniki.IsEnabled = false;
            ok_button.IsEnabled = false;
            cancel_button.IsEnabled = false;
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            vibor_vaccine.Text = "";
            vibor_polikliniki.Text = "";
            vibor_rajona.Text = "";
            date_vaccine1.SelectedDate = DateTime.Today;
            date_vaccine2.Content = "Дата 2 - ой вакцинации:";
            MessageBox.Show("Данные были успешно очищены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void vibor_rajona_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            vibor_polikliniki.Items.Clear();
            id_name_polyclinic.Clear();
            connection.Close();
            connection.Open();
            string Query3 = "SELECT polyclinic.id,polyclinic.name FROM polyclinic,area where polyclinic.id_area = area.id and id_area=" + Convert.ToString(vibor_rajona.SelectedIndex + 1);
            MySqlCommand command3 = new MySqlCommand(Query3, connection);
            MySqlDataReader reader3 = command3.ExecuteReader();
            while (reader3.Read())
            {
                id_name_polyclinic.Add(reader3.GetString(0));
                vibor_polikliniki.Items.Add(reader3.GetString(1));
            }
            connection.Close();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            //patient_menu_window k = new patient_menu_window(id);
            //k.Show();
        }
    }
}
