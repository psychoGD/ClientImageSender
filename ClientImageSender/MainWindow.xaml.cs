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

namespace ClientImageSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel mainViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            mainViewModel= new MainViewModel();
            this.DataContext= mainViewModel;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) != null)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                BitmapImage myBitmapImage = new BitmapImage(new Uri($@"{files[0]}", UriKind.Relative));
                myBitmapImage.CacheOption = BitmapCacheOption.Default;
                mainViewModel.Image= myBitmapImage;
                mainViewModel.ImageSource = myBitmapImage.UriSource.ToString();

            }
        }
    }
}
