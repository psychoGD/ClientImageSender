using System;
using System.Net.Sockets;
using System.Net;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
//using System.Windows.Controls;

namespace ClientImageSender
{
    public class MainViewModel : BaseViewModel
    {
        //public RelayCommand DropEvent { get; set; }


        public RelayCommand ClickCommand { get; set; }

        private string imageSource;

        public string ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; OnPropertyChanged(); }
        }

        public BitmapImage Image { get; set; }

        public bool isConnected { get; set; } = true;


        public MainViewModel()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var ipAddress = IPAddress.Parse("192.168.56.1");
            var port = 80;
            var ep = new IPEndPoint(ipAddress, port);

            try
            {
                socket.Connect(ep);
            }
            catch (Exception)
            {
                MessageBox.Show("Cant Connect To Server");
                isConnected = false;
            }
            ClickCommand = new RelayCommand(o =>
            {
                if (isConnected)
                {
                    if (socket.Connected)
                    {
                        byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(Image, typeof(byte[]));
                        socket.Send(bytes);
                    }
                }


            });
        }


    }
}
