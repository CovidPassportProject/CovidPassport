using authorization_registration.Class;
using MySql.Data.MySqlClient;
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

namespace authorization_registration
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class authorization_window  : Window
    {
        static string connBD = ConnectionBD_path.Path();
        bool useHiddenPass = true;

        string password = "";
        bool res_admin = false;
        bool res_user = false;


        Image img = new Image();
        BitmapImage btmg = new BitmapImage(new Uri("pack://application:,,,/images/eye_icon-icons.com_69386.png"));

        MySqlConnection connection = new MySqlConnection(connBD);
        public authorization_window()
        {
            InitializeComponent();

            img.Source = btmg;
            Eye_button.Content = img;

        }

        private void entry_Click(object sender, RoutedEventArgs e)
        {

            string login = login_field.Text;

            if (useHiddenPass == true)
            {
                password = password_field.Password;
            }
            else
            {
                password = password_textbox.Text;
            }

            if (login == "" && password == "")
            {
                MessageBox.Show("Заполните все данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (login != "" && password == "")
            {
                MessageBox.Show("Вы не заполнили пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            if (login == "" && password != "")
            {
                MessageBox.Show("Вы не заполнили логин!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            else
            {
                try
                {
                    connection.Open();
                    string zapros = "Select * from covid_passport.credentials_patients";
                    MySqlCommand command = new MySqlCommand(zapros, connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    bool result = false;
                    int j = 0;
                    string id = "";
                    while (reader.Read())
                    {
                        if (login == reader.GetString(1) & password == reader.GetString(2) & reader.GetString(3) == "user")
                        {
                            result = true;
                            res_user = true;
                            id = reader.GetString(0);
                            break;
                        }
                        if (login != reader.GetString(1) & password == reader.GetString(2) & reader.GetString(3) == "user")
                        {
                            id = reader.GetString(0);
                            login_field.Text = "";
                            MessageBox.Show("Неверный логин!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            res_user = true;
                            break;

                        }
                        if (login == reader.GetString(1) & password != reader.GetString(2) & reader.GetString(3) == "user")
                        {
                            id = reader.GetString(0);
                            password_field.Clear();
                            password_textbox.Clear();
                            MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            res_user = true;
                            break;
                        }
                        else
                        {
                            result = false;
                            res_user = false;
                        }
                    }
                    connection.Close();
                    if (res_user == false)
                    {
                        connection.Open();
                        string zapros2 = "SELECT * FROM covid_passport.admin";
                        MySqlCommand command2 = new MySqlCommand(zapros2, connection);
                        MySqlDataReader reader2 = command2.ExecuteReader();
                        while (reader2.Read())
                        {
                            if (login == reader2.GetString(0) & password == reader2.GetString(1) & reader2.GetString(2) == "admin")
                            {
                                result = true;
                                res_admin = true;
                                break;
                            }
                            else if (login != reader2.GetString(0) & password == reader2.GetString(1) & reader2.GetString(2) == "admin")
                            {
                                id = reader.GetString(0);
                                login_field.Text = "";
                                MessageBox.Show("Неверный логин!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                res_admin = true;
                                break;
                            }
                            else if (login == reader2.GetString(0) & password == reader2.GetString(1) & reader2.GetString(2) == "admin")
                            {
                                id = reader.GetString(0);
                                password_field.Clear();
                                password_textbox.Clear();
                                MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                res_admin = true;
                                break;
                            }
                            else
                            {
                                result = false;
                                res_admin = false;
                            }
                        }

                    }

                    if (result == true)
                    {

                        login_field.Text = "";
                        password_field.Password = "";
                        if (res_user == true)
                        {
                           // patient_menu_window p = new patient_menu_window(id);
                           // p.Show();
                            this.Close();
                        }
                        else
                        {
                           // admin_window admin = new admin_window();
                           // admin.Show();
                            this.Close();
                        }
                    }
                    else if (res_admin == false && res_user == false)
                    {
                        MessageBox.Show("Пользователя с такими данными не существует! Зарегистрируйтесь !", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        MessageBoxResult output = MessageBox.Show("Вы хотите зарегистрироваться?", "Информация",
                            MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (output == MessageBoxResult.Yes)
                        {
                            this.Hide();
                            //registration_window t = new registration_window();
                           // t.Show();
                        }

                    }

                }
                catch (Exception error)
                {
                    MessageBox.Show("Возникла ошибка: " + error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private void registration_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
          //  registration_window r = new registration_window();
          //  r.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Eye_button_Click(object sender, RoutedEventArgs e)
        {
            if (password_textbox.Visibility == Visibility.Hidden)
            {
                useHiddenPass = false;
                password_textbox.Visibility = Visibility.Visible;
                password_field.Visibility = Visibility.Hidden;
                password_textbox.Text = password_field.Password;
            }
            else
            {
                useHiddenPass = true;
                password_textbox.Visibility = Visibility.Hidden;
                password_field.Visibility = Visibility.Visible;
                password_field.Password = password_textbox.Text;
            }
        }

        private void login_field_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void login_field_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (int.TryParse(e.Text, out int i))
            {
                e.Handled = true;
            }
        }
    }
}
