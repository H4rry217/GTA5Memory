using GTA5Memory_UI.GameProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTA5Memory_UI
{
    public partial class ESP_SCREEN : Form
    {

        public ESP_SCREEN()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            BackColor = Color.White;
            TransparencyKey = Color.White;

        }

        private void ESP_SCREEN_Paint(object sender, PaintEventArgs e)
        {
            if (!GTA5Process.CheatList["ESP"]) return;

            Graphics gg = e.Graphics;

            SortedDictionary<string, long> players = GTA5Process.GetPlayers();

            Bitmap bmp = new Bitmap(1920, 1080);

            Pen p = new Pen(Color.Green, 5);
            Graphics g = Graphics.FromImage(bmp);
            
            foreach (string pname in players.Keys)
            {
                float[] otherplayerpos = GTA5Process.GetPlayerPos(pname);
                float distance = Utils.Distance(GTA5Process.GetPlayerPosition(), otherplayerpos);

                if (distance > 2f && distance < 1000f)
                {

                    GameESP.GAME_Vector2 screen_pos = GameESP.WORLD_TO_SCREEN(new GameESP.GAME_Vector3(otherplayerpos[0], otherplayerpos[1], otherplayerpos[2] + 1));
                    g.DrawLine(p, 960, 0, screen_pos.X, screen_pos.Y);
                    //g.DrawRectangle(p, screen_pos.X - 5, screen_pos.Y, 10, 20);
                    g.DrawString(Math.Round(distance) + "m   " + pname, new Font("Arial", 20), new SolidBrush(Color.Red), screen_pos.X - 60, screen_pos.Y + 30);
                }

            }

            gg.DrawImage(bmp, 0, 0);

            p.Dispose();
            g.Dispose();
            bmp.Dispose();

        }
    }
}
