using FlightSimulatorApp.ViewModel;
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

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for ErrorBox.xaml
    /// </summary>
    public partial class ErrorBox : Window
    {
        ConnectionAndErrorVIewModel cevm;

        public ErrorBox(ConnectionAndErrorVIewModel vm)
        {
            InitializeComponent();
            cevm = vm;
            DataContext = cevm;
        }

        private void ignore_Click(object sender, RoutedEventArgs e)
        {
            errorText.Text = "";
            cevm.VM_ErrorClear = null;
            cevm.VM_HasError = "";
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
