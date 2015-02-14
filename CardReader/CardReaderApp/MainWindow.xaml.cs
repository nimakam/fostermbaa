using CardReaderLibrary;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CardReaderApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        uint RowCount = 1;
        string SheetName = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToLongTimeString();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SheetName = SheetName.Replace(" ", "");
            SheetName = SheetName.Replace(":", "-");
            SheetName = SheetName.Replace(",", "-");
            SheetName = SheetName.Replace("/", "-");


            var fileName = "Student Sign-in.xlsx";

            string baseDirectory;
            string fullFileName = null;
            

            foreach (var drive in Environment.GetLogicalDrives().Reverse())
            {
                baseDirectory = drive;
                fullFileName = System.IO.Path.Combine(baseDirectory, fileName);

                if (File.Exists(fullFileName))
                {
                    break;
                }
            }


            if (!File.Exists(fullFileName))
            {
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                fullFileName = System.IO.Path.Combine(baseDirectory, fileName);

                if (!File.Exists(fullFileName))
                {
                    fileName = DateTime.Now.ToLongDateString() + "-" + DateTime.Now.ToShortTimeString() + ".xlsx";
                    fileName = fileName.Replace(",", "");
                    fileName = fileName.Replace(":", "-");
                    fileName = fileName.Replace(" ", "-");
                    fullFileName = System.IO.Path.Combine(baseDirectory, fileName);
                }
            }

            TextBox2.Text = fullFileName;            

            try
            {
                using (var excelWrapper = new CardReaderLibrary.ExcelWrapper(fullFileName, RowCount, SheetName))
                {
                    RowCount = 2;


                    StudentIdFullNameLookup = excelWrapper.GetIdFullNameLookup();
                }

                Rectangle1.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);



            }
            catch
            {
                Rectangle1.Fill = new SolidColorBrush(System.Windows.Media.Colors.Red);
                throw;
            }

            var studentIdCapture = new StudentIdCapture();

            GlobalKeyboardCapture.Instance.Subscribe(studentIdCapture);
            studentIdCapture.Subscribe(new ActionSubscriber<string>(delegate(string studentId)
            {
                string fullName = string.Empty;

                CardReaderLibrary.ExcelWrapper.StudentInfo studentInfo = null; // = string.Empty;
                if (StudentIdFullNameLookup != null)
                {
                    if(StudentIdFullNameLookup.TryGetValue(studentId, out studentInfo))
                    {
                        fullName = studentInfo.FullName;
                        TextBox1.Text = string.Format("{0} - Scanned Card with Student ID: {1}", DateTime.Now.ToLongTimeString(), studentId);
                        FullNameTextBox.Text = studentInfo.FullName;
                        ClassTextBox.Text = "Class: " + studentInfo.Class;
                        if(studentInfo.IsMbaaMember.HasValue)
                        {
                            if(studentInfo.IsMbaaMember.Value)
                            {
                                MbaaTextBox.Text = "MBAA Member";
                                MbaaTextBox.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Green); 
                            }
                            else
                            {
                                MbaaTextBox.Text = "Not an MBAA Member";
                                MbaaTextBox.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Red);
                            }
                        }
                        else
                        {
                            MbaaTextBox.Text = string.Empty;
                        }
                    }
                }
               
                using (var excelWrapper = new CardReaderLibrary.ExcelWrapper(fullFileName, RowCount, SheetName))
                {
                    excelWrapper.AddStudentId(studentId, fullName);
                    RowCount++;
                }



                
                this.BringIntoView();
                TextBox2.Focus();
            }));

            GlobalKeyboardCapture.Loaded();
        }

        Dictionary<string, CardReaderLibrary.ExcelWrapper.StudentInfo> StudentIdFullNameLookup { get; set; }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            GlobalKeyboardCapture.Unloaded();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel documents (.xlsx)|*.xlsx";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {                // Open document 

                string filename = dlg.FileName;
                TextBox2.Text = filename;

                try
                {
                    using (var excelWrapper = new CardReaderLibrary.ExcelWrapper(TextBox2.Text, RowCount, SheetName))
                    {
                        RowCount = 2;
                    }

                    Rectangle1.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                }
                catch
                {
                    Rectangle1.Fill = new SolidColorBrush(System.Windows.Media.Colors.Red);
                    throw;
                }

                

            }

            TextBox2.Focus();


        }

    }
}
