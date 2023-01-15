using System;
using System.Net.Sockets;
using System.Net;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
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

        public BitmapImage ImageTest { get; set; }

        public bool isConnected { get; set; } = true;

        public byte[] BitmapImageToBytes(BitmapImage image)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }


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
                MessageBox.Show(socket.Connected.ToString());
                if (isConnected)
                {
                    if (socket.Connected)
                    {
                        //var image = new System.Windows.Controls.Image();
                        //var imageSource2 = new ImageSource();
                        //byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(Image, typeof(byte[]));
                        try
                        {
                            var bytes = BitmapImageToBytes(ImageTest);
                            MessageBox.Show(bytes.Length.ToString());
                            socket.Send(bytes);
                            ImageSource = "";
                            ImageTest = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        
                    }
                }


            });
        }


    }
}
