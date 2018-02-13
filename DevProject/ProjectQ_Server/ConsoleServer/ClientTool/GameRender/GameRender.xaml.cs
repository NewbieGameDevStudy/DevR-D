using OpenTK;
using Player;
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

namespace ClientTool.GameRender
{
    /// <summary>
    /// GameRender.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GameRender : Window
    {
        PlayerObject playerObject;
        Dictionary<int, Ellipse> objectList = new Dictionary<int, Ellipse>();
        bool isInitPlayer = false;

        public GameRender()
        {
            InitializeComponent();
            
        }

        public void SetPlayerObject(PlayerObject player)
        {
            playerObject = player;
        }

        public void Render()
        {
            ObjectInitRender();
            ObjectRender();
        }

        private void ObjectRender()
        {
            if (playerObject.Client.Player.CurrentObjList.Count == 0)
                return;

            foreach (var obj in playerObject.CurrentObjList) {
                if (!objectList.ContainsKey(obj.Key))
                    continue;

                var elip = objectList[obj.Key];
                Canvas.SetLeft(elip, obj.Value.PlayerData.Xpos);
                Canvas.SetTop(elip, obj.Value.PlayerData.Ypos);
            }

            var playerElip = objectList[playerObject.Handle];
            Canvas.SetLeft(playerElip, playerObject.PlayerData.Xpos);
            Canvas.SetTop(playerElip, playerObject.PlayerData.Ypos);
        }

        void ObjectInitRender()
        {
            if (playerObject.isEnterComplete && !isInitPlayer) {
                isInitPlayer = true;
                Ellipse elips = new Ellipse();
                elips.Fill = new SolidColorBrush(Colors.Crimson);
                elips.Width = 30;
                elips.Height = 30;
                GameCanvans.Children.Add(elips);
                Canvas.SetLeft(elips, playerObject.PlayerData.Xpos);
                Canvas.SetTop(elips, playerObject.PlayerData.Ypos);

                objectList.Add(playerObject.Handle, elips);
            }

            if (playerObject.Client.Player.CurrentObjList.Count == 0)
                return;

            foreach (var obj in playerObject.Client.Player.CurrentObjList) {
                if (objectList.ContainsKey(obj.Key))
                    continue;
                
                var ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(Colors.Black);
                ellipse.Width = 30;
                ellipse.Height = 30;
                GameCanvans.Children.Add(ellipse);

                Canvas.SetLeft(ellipse, obj.Value.PlayerData.Xpos);
                Canvas.SetTop(ellipse, obj.Value.PlayerData.Xpos);

                objectList.Add(obj.Key, ellipse);
            }
        }

        private void GameCanvans_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var p = Mouse.GetPosition(GameCanvans);
            playerObject.PlayerData.SetTargetPosition((float)p.X, (float)p.Y);
            playerObject.Client.ReqInputTargetMovePos((float)p.X, (float)p.Y);
        }
    }
}
