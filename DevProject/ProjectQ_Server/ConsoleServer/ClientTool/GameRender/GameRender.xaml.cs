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

        public GameRender()
        {
            InitializeComponent();
        }

        public void SetPlayerObject(PlayerObject player)
        {
            playerObject = player;
            
            Ellipse elips = new Ellipse();
            elips.Fill = new SolidColorBrush(Colors.Crimson);
            elips.Width = 30;
            elips.Height = 30;
            GameCanvans.Children.Add(elips);
            Canvas.SetLeft(elips, player.PlayerData.Xpos);
            Canvas.SetTop(elips, player.PlayerData.Ypos);

            objectList.Add(player.Handle, elips);
        }
    }
}
