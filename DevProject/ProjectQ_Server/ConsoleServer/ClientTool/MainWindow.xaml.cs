using BaseClient;
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

namespace ClientTool
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        Client client;

        public MainWindow()
        {
            InitializeComponent();
            client = new Client();
            client.Init();
        }

        private void Button_Connect(object sender, RoutedEventArgs e)
        {
            client.Connect();

            double t = 0.0;
            var prevTime = DateTime.Now;

            Task.Factory.StartNew(() => {
                while (true) {
                    if (client.Player != null) {
                        var nowTime = DateTime.Now;
                        var time = nowTime - prevTime;
                        prevTime = nowTime;
                        t += time.TotalSeconds;
                        client.Update(t);
                    }
                }
            });
        }

        public void Button_RoomEnter(object sender, RoutedEventArgs e)
        {
            client.SendPacket(new Packet.PK_CS_ENTERROOM {
                type = Packet.PK_CS_ENTERROOM.RoomType.GAME,
            });
        }
    }
}
