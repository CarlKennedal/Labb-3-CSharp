using Labb_3_CSharp.Model;
using Labb_3_CSharp.ViewModel;
using System.Windows;

namespace Labb_3_CSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindomViewModel();



        }
    }
}