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
using Xceed.Document.NET;
using Xceed.Words.NET;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using GemBox.Document;

namespace QR_code
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
            id1 = id;
            connection.Open();
            string zapros = "SELECT surname,patients.name,patronymic,datebirthday,polyclinic.name,type_vaccine.name,date_vaccine1,date_vaccine2 FROM zapic_vaccine,patients,polyclinic,type_vaccine where zapic_vaccine.id_patients=patients.id_patients and zapic_vaccine.id_polyclinic=polyclinic.id and zapic_vaccine.id_type_vaccine=type_vaccine.id and zapic_vaccine.id_patients=" + id;
            MySqlCommand command = new MySqlCommand(zapros, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string fio = reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2);
            FIO.Content = fio;

            datebirthday.Content = "Дата рождения: " + reader.GetString(3).Split(' ')[1];
            name_polyclinic.Content = "Поликлиника: " + reader.GetString(4);
            name_vaccine.Content = "Название вакцины: " + reader.GetString(5);
            date_vaccine.Content = "Дата первой вакцинации: " + reader.GetString(6).Split(' ')[1];
            date_vaccine2.Content = "Дата второй вакцинации: " + reader.GetString(7).Split(' ')[1];

            connection.Close();

            QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData data = qr.CreateQrCode(fio + " - Ковид паспорт действителен", QRCoder.QRCodeGenerator.ECCLevel.L);
            QRCoder.QRCode code = new QRCoder.QRCode(data);
            Bitmap bitmap = code.GetGraphic(100);
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                qrImageView.Source = bitmapimage;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.FileName = "Covid-паспорт";
            saveFileDialog.AddExtension = true;
            saveFileDialog.Title = "Выбор файла для сохранения";
            saveFileDialog.Filter = "(*.docx)|*.docx|Все файлы (*.*)|*.* ";


            if (saveFileDialog.ShowDialog() == true)
            {
                string FilePath = saveFileDialog.FileName;
                DocX document = DocX.Create(FilePath);
                var Title = document.InsertParagraph();
                Title.Alignment = Xceed.Document.NET.Alignment.center;
                Title.Append("Covid-паспорт");
                Title.FontSize(20);
                Title.Bold();
                Title.Font("Times New Roman");

                var Text = document.InsertParagraph();
                Text.Alignment = Xceed.Document.NET.Alignment.left;
                Text.Append("Фамилия: " + FIO.Content.ToString().Split(' ')[0]);
                Text.FontSize(15);
                Text.Bold(false);
                Text.Font("Times New Roman");

                var Text2 = document.InsertParagraph();
                Text2.Alignment = Xceed.Document.NET.Alignment.left;
                Text2.Append("Имя: " + FIO.Content.ToString().Split(' ')[1]);
                Text2.FontSize(15);
                Text2.Bold(false);
                Text2.Font("Times New Roman");

                var Text3 = document.InsertParagraph();
                Text3.Alignment = Xceed.Document.NET.Alignment.left;
                Text3.Append("Отчество: " + FIO.Content.ToString().Split(' ')[2]);
                Text3.FontSize(15);
                Text3.Bold(false);
                Text3.Font("Times New Roman");

                var Text4 = document.InsertParagraph();
                Text4.Alignment = Xceed.Document.NET.Alignment.left;
                Text4.Append(datebirthday.Content.ToString());
                Text4.FontSize(15);
                Text4.Bold(false);
                Text4.Font("Times New Roman");

                var Text5 = document.InsertParagraph();
                Text5.Alignment = Xceed.Document.NET.Alignment.left;
                Text5.Append(name_polyclinic.Content.ToString());
                Text5.FontSize(15);
                Text5.Bold(false);
                Text5.Font("Times New Roman");

                var Text6 = document.InsertParagraph();
                Text6.Alignment = Xceed.Document.NET.Alignment.left;
                Text6.Append(name_vaccine.Content.ToString());
                Text6.FontSize(15);
                Text6.Bold(false);
                Text6.Font("Times New Roman");

                var Text7 = document.InsertParagraph();
                Text7.Alignment = Xceed.Document.NET.Alignment.left;
                Text7.Append(date_vaccine.Content.ToString());
                Text7.FontSize(15);
                Text7.Bold(false);
                Text7.Font("Times New Roman");

                var Text8 = document.InsertParagraph();
                Text8.Alignment = Xceed.Document.NET.Alignment.left;
                Text8.Append(date_vaccine2.Content.ToString());
                Text8.FontSize(15);
                Text8.Bold(false);
                Text8.Font("Times New Roman");

                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)qrImageView.Source));
                using (FileStream stream = new FileStream(FilePath, FileMode.Create))
                    encoder.Save(stream);

                // Add a simple image from disk and set its wrapping as Square.
                var image = document.AddImage(FilePath);
                // Set Picture Height and Width.
                var picture = image.CreatePicture(100, 100);

                // Add a paragraph and the picture in it.
                var p = document.InsertParagraph("");
                p.Alignment = Alignment.both;
                p.InsertPicture(picture);
                document.Save();

                // If using Professional version, put your serial key below.
                ComponentInfo.SetLicense("FREE-LIMITED-KEY");

                // In order to convert Word to PDF, we just need to:
                //   1. Load DOC or DOCX file into DocumentModel object.
                //   2. Save DocumentModel object to PDF file.
                DocumentModel document2 = DocumentModel.Load(FilePath);
                string result = System.IO.Path.ChangeExtension(FilePath, "pdf");
                document2.Save(result);
                MessageBox.Show("Документ успешно сохранен!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            patient_menu_window pr = new patient_menu_window(id1);
            pr.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
