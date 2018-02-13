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
using System.Windows.Threading;

namespace ClientTool
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        Client client;
        private DispatcherTimer m_updateTimer;
        DateTime prevTime;
        double deltaTime = 0;

        static GameRender.GameRender gameRender;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Update(object sender, EventArgs e)
        {
            var nowTime = DateTime.Now;
            var time = nowTime - prevTime;
            prevTime = nowTime;
            deltaTime += time.TotalSeconds;
            client.Update(deltaTime);

            if (gameRender != null)
                gameRender.Render();
        }

        private void Button_Connect(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            button.IsEnabled = false;
            DisconnectButton.IsEnabled = true;

            ConnectState.Text = "접속완료";

            client = new Client();
            client.Init();
            client.Connect();

            m_updateTimer = new DispatcherTimer {
                Interval = TimeSpan.FromMilliseconds(33)
            };
            m_updateTimer.Tick += Update;
            m_updateTimer.Start();

            deltaTime = 0.0;
            prevTime = DateTime.Now;
        }

        public void Button_RoomEnter(object sender, RoutedEventArgs e)
        {
            if (client != null)
                client.SendPacket(new Packet.PK_CS_ENTERROOM {
                    type = Packet.PK_CS_ENTERROOM.RoomType.GAME,
                });

            gameRender = new GameRender.GameRender();
            gameRender.SetPlayerObject(client.Player);
            gameRender.Show();
        }

        private void Button_Disconnect(object sender, RoutedEventArgs e)
        {

        }
    }
}
