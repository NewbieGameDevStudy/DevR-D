﻿using BaseClient;
using GameServer.HttpDTO;
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
        Random tempRandom = new Random(10000);

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
            client.Connect();

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
            //Random r = new Random();
            //id = (ulong)r.Next(0, 100);
            //var playerInfo = new ReqPlayerInfo {
            //    accountId = (ulong)id,
            //};

            /*
            var playerInfo = new ReqShopBuyProduct {
                buyProductId = 0,
                buyProductCount = 999,
            };*/

            //playerInfo.accountId = id;
            //client.HttpConnect();
            //client.HttpConnection.HttpConnectAsync(186282282057728513, playerInfo, (ShopBuyProduct result) => {
            //    if (result == null)
            //        return;

            //});

            var temp = new PK_CS_CANCEL_MATCHING
            {
                userSequence = (ulong)tempRandom.Next(1, 100000)//id
        };
            client.SendPacket(temp);
        }

        public void TestSession2(object sender, EventArgs e)
        {
            /*
            var playerInfo = new ReqLoginInfo {
                
            };*/

            //client.HttpConnect();
            //client.HttpConnection.HttpConnectAsync(186282282057728513, playerInfo, (PlayerStatus result) => {
            //    if (result == null)
            //        return;

            //});

            var temp = new PK_CS_READY_COMPLETE_FOR_GAME
            {
                userSequence
            }
        }

        public void Button_RoomEnter(object sender, RoutedEventArgs e)
        {
            var temp = new PK_CS_ENTERROOM
            {
                userSequence = (ulong)tempRandom.Next(1, 100000)//id
        };

            client.SendPacket(temp);


            //gameRender = new GameRender.GameRender();
            //gameRender.SetPlayerObject(client.Player);
            //gameRender.Show();
        }

        private void Button_Disconnect(object sender, RoutedEventArgs e)
        {

        }
    }
}
