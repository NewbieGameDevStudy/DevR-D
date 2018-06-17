﻿using BaseClient;
using GameServer.WebHttp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Packet;

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
        ulong id;
        Random tempRandom = new Random();

        static GameRender.GameRender gameRender;

        public MainWindow()
        {
            InitializeComponent();
            id = (ulong)tempRandom.Next(1, 100000);
        }

        public void Update(object sender, EventArgs e)
        {
            var nowTime = DateTime.Now;
            var time = nowTime - prevTime;
            prevTime = nowTime;
            deltaTime += time.TotalSeconds;
            client.Update(time.TotalSeconds);

            if (gameRender != null)
                gameRender.Render(time.TotalSeconds);
        }

        private void Button_Connect(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            button.IsEnabled = false;
            DisconnectButton.IsEnabled = true;

            ConnectState.Text = "접속완료";

            client = new Client();
            client.Init(client, "OnReceivePacket");
            client.Connect("10.70.50.63", 5050);

            m_updateTimer = new DispatcherTimer {
                Interval = TimeSpan.FromMilliseconds(33)
            };
            m_updateTimer.Tick += Update;
            m_updateTimer.Start();

            deltaTime = 0.0;
            prevTime = DateTime.Now;
        }

        public void TestSession(object sender, EventArgs e)
        {
            //var playerInfo = new ReqLoginInfo {
            //});

            client.SendPacket(new Packet.PK_CS_CLIENT_ACCOUNT {
                accountId = 194630578601984257,
            });
        }

        public void TestSession2(object sender, EventArgs e)
        {
            client.SendPacket(new Packet.PK_CS_CLIENT_ACCOUNT {
                accountId = 196912997203968513,
            });

            //client.HttpConnect();
            //client.HttpConnection.HttpConnectAsync(194630578601984257, playerInfo, (PlayerStatus result) => {
            //    if (result == null)
            //        return;

            //});
        }

        public void TestSession3(object sender, EventArgs e)
        {
            client.SendPacket(new Packet.PK_CS_CLIENT_ACCOUNT {
                accountId = 196863835766784513,
            });

            //client.HttpConnect();
            //client.HttpConnection.HttpConnectAsync(194630578601984257, playerInfo, (PlayerStatus result) => {
            //    if (result == null)
            //        return;

            //});
            /*
            var playerInfo = new ReqLoginInfo {
                
            };*/

            /*
            client.HttpConnect();
            client.HttpConnection.HttpConnectAsync(194630578601984257, playerInfo, (PlayerStatus result) => {
                if (result == null)
                    return;
            });*/

            var temp = new PK_CS_READY_COMPLETE_FOR_GAME
            {
                //AccountIDClient = (ulong)tempRandom.Next(1, 100000),
                accountId = id
            };
        }

        public void Button_RoomEnter(object sender, RoutedEventArgs e)
        {
            client.SendPacket(new Packet.PK_CS_ENTERROOM {

            });

        }

        private void Button_Disconnect(object sender, RoutedEventArgs e)
        {

        }

        private void InputIDBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var temp = (TextBox)sender;
            id = Convert.ToUInt64(temp.Text);
            int b = 0;
        }

    }
}
