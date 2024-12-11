using Labb_3_CSharp.Model;
using Labb_3_CSharp.ViewModel;
using System.Windows;
using System.Text.Json;
using System.IO;
using System.Windows.Forms;
using Labb_3_CSharp.Command;
using System.Windows.Input;

namespace Labb_3_CSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public static readonly string FilePath = "question_packs.json";
        public static RoutedCommand ToggleFullscreenCommand = new RoutedCommand();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindomViewModel();
            CommandBindings.Add(new CommandBinding(ToggleFullscreenCommand, ToggleFullscreenCommand_Executed));


        }

        public void ToggleFullscreenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ToggleFullScreen();
        }

        public void ToggleFullScreen()
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
            }
        }

    }
}