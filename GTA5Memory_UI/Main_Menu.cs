using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using GTA5Memory_UI.GameProcess;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using static GTA5Memory_UI.GameESP;

namespace GTA5Memory_UI
{
    public partial class Main_Menu : Form
    {

        private static bool MacroSwitch = true;

        private KeyEventHandler myKeyEventHandeler = null;//按键钩子
        private KeyboardHook k_hook = new KeyboardHook();

        private MouseEventHandler myMouseHandeler = null;//按键钩子
        private MouseHook m_hook = new MouseHook();

        public Main_Menu()
        {
            InitializeComponent();
            startListen();

            ESP_Form = new ESP_SCREEN();
            ESP_Form.Show();

        }


        private void hook_KeyDown(object sender, KeyEventArgs e)
        {
            //  这里写具体实现
            if (e.KeyValue == 192) {
                Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_M);
                Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.UP, true);
                Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.UP, true);
                Keyboard.KeyDown(Keyboard.ScanCodeShort.KEY_C, false);
                Keyboard.KeyDown(Keyboard.ScanCodeShort.CAPITAL, false);
                Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);
                Keyboard.KeyUP(Keyboard.ScanCodeShort.CAPITAL, false);
                Keyboard.KeyUP(Keyboard.ScanCodeShort.KEY_C, false);
                /*Keyboard.KeyDown(Keyboard.ScanCodeShort.CONTROL, false);
                Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_T);
                Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_V);
                Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);*/

            }

            if ((int)ModifierKeys == (int)Keys.Control) {

                switch (e.KeyValue) {
                    case (int)Keys.NumPad4:
                        GTA5Process.Set_Wantedlevel((int)numericUpDown_WANTED_LEVEL.Value);
                        break;
                    case (int)Keys.NumPad6:
                        
                        break;
                    case (int)Keys.NumPad8:
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_M);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);
                        Thread.Sleep(50);
                        //Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.LEFT, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.UP, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.UP, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.UP, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.LEFT, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.UP, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.UP, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.LEFT, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);

                        break;
                    case (int)Keys.NumPad1:
                        Application.Exit();
                        break;
                    case (int)Keys.NumPad7:
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_M);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.UP, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.UP, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.UP, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);
                        break;
                    case (int)Keys.NumPad9:
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_M);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.DOWN, true);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.SPACE);
                        Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_M);
                        break;
                    default:
                        return;
                }

            }

        }

        private void hook_MouseButtonDown(object sender, MouseEventArgs e)
        {
            //label9.Text = e.
            switch (e.Button) {
                case MouseButtons.XButton1:
                    Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_T);
                    Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_R);
                    Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_I);
                    Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_P);
                    Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.RETURN);

                    //Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_3);
                    //Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_2);
                    //Keyboard.Mouse_Right_Dowm();
                    //Keyboard.Mouse_Left_Dowm();

                    //Keyboard.Mouse_Left_Up();
                    //Keyboard.Mouse_Right_Up();
                    break;
                case MouseButtons.XButton2:
                    Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_3);
                    Keyboard.FuckingPressKey(Keyboard.ScanCodeShort.KEY_1);
                    break;

            }
        }

        public void startListen()
        {
            myKeyEventHandeler = new KeyEventHandler(hook_KeyDown);
            k_hook.KeyDownEvent += myKeyEventHandeler;//钩住键按下
            k_hook.Start();//安装键盘钩子

            myMouseHandeler = new MouseEventHandler(hook_MouseButtonDown);
            m_hook.OnMouseActivity += myMouseHandeler;//钩住键按下
            m_hook.Start();//安装鼠标钩子
        }

        public void stopListen()
        {
            if (myKeyEventHandeler != null)
            {
                k_hook.KeyDownEvent -= myKeyEventHandeler;//取消按键事件
                myKeyEventHandeler = null;
                k_hook.Stop();//关闭键盘钩子
            }

            if (myMouseHandeler != null)
            {
                m_hook.OnMouseActivity -= myMouseHandeler;//取消按键事件
                myMouseHandeler = null;
                m_hook.Stop();//关闭鼠标钩子
            }
        }

        private void Main_Menu_Load(object sender, EventArgs e)
        {
            if (!GTA5Process.IsGameRunning())
            {
                MessageBox.Show("Cannot found 'GTA5.exe' process, Please exit program");
            }

                GTA5Process.initHack();

            //TODO

            BUTTON_Refresh_Click(sender, e);
            BUTTON_TP_Reload_Click(sender, e);
        }



        private void BUTTON_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Author:HCiLnETE \n" +
                "Email:1339544914@qq.com \n" +
                "GTA5/GTAOL memory trainer"
                );
        }

        private void BUTTON_Refresh_Click(object sender, EventArgs e)
        {
            BUTTON_Godmode.BackgroundImage = GTA5Process.Get_Godmode() ? Properties.Resources.switch_on : Properties.Resources.switch_off;
            BUTTON_Vehicle_Godmode.BackgroundImage = GTA5Process.Get_Vehicle_Godmode() ? Properties.Resources.switch_on : Properties.Resources.switch_off;
            BUTTON_infinite_ammo.BackgroundImage = GTA5Process.Get_Infinite_Ammo() ? Properties.Resources.switch_on : Properties.Resources.switch_off;
            BUTTON_Ragdoll.BackgroundImage = GTA5Process.Get_Ragdoll() ? Properties.Resources.switch_on : Properties.Resources.switch_off;
            BUTTON_weather_snow.BackgroundImage = GTA5Process.Weather_GetSnow() ? Properties.Resources.switch_on : Properties.Resources.switch_off;

            //////////////////////
            BulletType_ListBox.Items.Clear();
            foreach (string typeName in GameAddress.Button_Impact.Keys)
            {
                BulletType_ListBox.Items.Add(typeName);
            }

        }

        private void TP_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(TP_ListBox.SelectedItem.ToString());
        }

        private void BUTTON_TP_WP_2_Click(object sender, EventArgs e)
        {
            float[] wp = GTA5Process.GetWayPoint();
            long pointer = Memory.GetPointerAddress(GameAddress.BaseAddress + GameAddress.BlipPTR);

            for (int i = 0; i < 1000; i++)
            {
                long address = Memory.ReadPointer(pointer + (i * 8));

                if (address > 0 && Memory.ReadInteger(address + 0x40, 4) == 8 && Memory.ReadInteger(address + 0x48, 4) == 84)
                {

                    wp[2] = -210;

                    GTA5Process.TP_to_with_car(wp);

                }

            }

        }

        private void BUTTON_TP_WP_Click(object sender, EventArgs e)
        {
            float[] a = GTA5Process.GetWayPoint();
            a[2] = float.Parse(numericUpDown1_TP_WP.Value.ToString());

            GTA5Process.TP_to_with_car(a);

            /*for (var i = 0; i < 1000; i++)
            {
                long pointer = Memory.GetPointerAddress(GameAddress.BaseAddress + GameAddress.BlipPTR);
                long address = Memory.ReadPointer(pointer + (i * 8));
                if (address > 0)
                {
                    if (Memory.ReadInteger(address + 0x40, 4) == 8 && Memory.ReadInteger(address + 0x48, 4) == 84)
                    {
                        float waypointposX = Memory.ReadFloat(address + 0x10);
                        float waypointposY = Memory.ReadFloat(address + 0x14);
                        long worldptr = Memory.GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR);
                        long player = Memory.ReadPointer(Memory.ReadPointer(worldptr) + 8);
                        byte[] vehicle_or_not = Memory.ReadBytes(player + 0x146B, 1);
                        if (vehicle_or_not[0] == 0)
                        {
                            player = Memory.ReadPointer(player + 0xD28);
                        }
                        long vehicle = Memory.ReadPointer(player + 0x30);
                        MessageBox.Show(waypointposX+"");

                        Memory.WriteFloat(vehicle + 0x50, waypointposX);
                        Memory.WriteFloat(vehicle + 0x54, waypointposY);
                        Memory.WriteFloat(vehicle + 0x58, -210);
                        Memory.WriteFloat(player + 0x90, waypointposX);
                        Memory.WriteFloat(player + 0x94, waypointposY);
                        Memory.WriteFloat(player + 0x98, -210);
                    }
                }
            }*/

        }

        private void BUTTON_Godmode_Click(object sender, EventArgs e)
        {

            if (GTA5Process.CheatList["Godmode"])
            {
                BUTTON_Godmode.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["Godmode"] = false;

                GTA5Process.Set_Godmode(false);

                return;
            }

            GTA5Process.CheatList["Godmode"] = true;
            BUTTON_Godmode.BackgroundImage = Properties.Resources.switch_on;

            /*if (GTA5Process.Get_Godmode()) {
                BUTTON_Godmode.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.Set_Godmode(false);

                return;
            }

            BUTTON_Godmode.BackgroundImage = Properties.Resources.switch_on;
            GTA5Process.Set_Godmode(true);*/

        }

        private void BUTTON_SprintSpeed_Click(object sender, EventArgs e)
        {
            GTA5Process.SetRunSpeed(float.Parse(numericUpDown_SprintSpeed.Value.ToString()));
        }

        private void BUTTON_ShowPos_Click(object sender, EventArgs e)
        {
            LABLE_ShowPos.Text = (int)GTA5Process.GetPlayerPosition()[0] + " " + (int)GTA5Process.GetPlayerPosition()[1] + " " + (int)GTA5Process.GetPlayerPosition()[2];
        }

        private void BUTTON_Delete_Pos_Click(object sender, EventArgs e)
        {
            if (TP_ListBox.SelectedItem == null)
            {
                MessageBox.Show("Please selected point!");
                return;
            }

            TXTConvert.Remove(TP_ListBox.SelectedItem.ToString());
            BUTTON_TP_Reload_Click(sender, e);

        }

        private void BUTTON_SavePos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TEXTBOX_SavePos.Text)) {
                MessageBox.Show("Position name illegal!");
                return;
            }

            if (TXTConvert.Exists(TEXTBOX_SavePos.Text)) {
                MessageBox.Show("Position name has exists!");
                return;
            }

            TXTConvert.Add(TEXTBOX_SavePos.Text, GTA5Process.GetPlayerPosition());
            BUTTON_TP_Reload_Click(sender, e);

        }

        private void BUTTON_TP_TO_Click(object sender, EventArgs e)
        {
            if (TP_ListBox.SelectedItem == null)
            {
                MessageBox.Show("Please selected point!");
                return;
            }

            GTA5Process.TP_to_with_car(TXTConvert.GetPosition(TP_ListBox.SelectedItem.ToString()));
        }

        private void BUTTON_TP_Reload_Click(object sender, EventArgs e)
        {
            TP_ListBox.Items.Clear();
            foreach (string posname in TXTConvert.GetPositionList().Keys)
            {
                TP_ListBox.Items.Add(posname);
            }
        }

        private void BUTTON_Vehicle_Godmode_Click(object sender, EventArgs e)
        {
            if (GTA5Process.CheatList["VehGodmode"])
            {
                BUTTON_Vehicle_Godmode.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["VehGodmode"] = false;

                GTA5Process.Set_Vehicle_Godmode(false);

                return;
            }

            GTA5Process.CheatList["VehGodmode"] = true;
            BUTTON_Vehicle_Godmode.BackgroundImage = Properties.Resources.switch_on;
        }

        private void BUTTON_Show_Health_Click(object sender, EventArgs e)
        {
            LABLE_Show_Health.Text = GTA5Process.Get_Vehicle_Health()[0] + "/" + GTA5Process.Get_Vehicle_Health()[1] + "/" + GTA5Process.Get_Vehicle_Health()[2];
        }

        private void BUTTON_Vehicle_SetHealth_Click(object sender, EventArgs e)
        {
            GTA5Process.Set_Vehicle_Health(float.Parse(numericUpDown_Vehicle_SetHelath.Value.ToString()));
        }

        private void BUTTON_Vehicle_Gravity_Click(object sender, EventArgs e)
        {
            GTA5Process.Set_Vehicle_Gravity(float.Parse(numericUpDown_Vehicle_Gravity.Value.ToString()));
        }

        private void BUTTON_Show_Gravity_Click(object sender, EventArgs e)
        {
            LABLE_VehicleGravity.Text = GTA5Process.Get_Vehicle_Gravity() + "";
        }

        private void BUTTON_infinite_ammo_Click(object sender, EventArgs e)
        {
            if (GTA5Process.CheatList["InfiniteAmmo"])
            {
                BUTTON_infinite_ammo.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["InfiniteAmmo"] = false;

                GTA5Process.Set_Infinite_Ammo(false);

                return;
            }

            GTA5Process.CheatList["InfiniteAmmo"] = true;
            BUTTON_infinite_ammo.BackgroundImage = Properties.Resources.switch_on;
        }

        private void BUTTON_bullte_damage_Click(object sender, EventArgs e)
        {
            GTA5Process.Set_Bullet_Damage(float.Parse(numericUpDown_bullte_damage.Value.ToString()));
        }

        private void BUTTON_Show_Damage_Click(object sender, EventArgs e)
        {
            label3_showdamage.Text = GTA5Process.Get_Bullet_Damage() + "";
        }

        private void BUTTON_Debug_Click(object sender, EventArgs e)
        {

            //long pointer1 = Memory.FindPattern(new byte[] { 0x48, 0x8B, 0x05, 0x0, 0x0, 0x0, 0x0, 0x45, 0x0, 0x0, 0x0, 0x0, 0x48, 0x8B, 0x48, 0x08, 0x48, 0x85, 0xC9, 0x74, 0x07}, "xxx????x????xxxxxxxxx");
            //var addr = Memory.FindPattern(new byte[] { 0x4C, 0x8D, 0x05, 0x0, 0x0, 0x0, 0x0, 0x4D, 0x8B, 0x08, 0x4D, 0x85, 0xC9, 0x74, 0x11 }, "xxx????xxxxxxxx");
            //long WorldPTR = pointer1 + Memory.ReadInteger(GameAddress.BaseAddress + pointer1 + 3, 4) + 7;
            //MessageBox.Show(Convert.ToString(WorldPTR, 16));

            Memory.WriteFloat(Memory.GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_HP), 0);
            //MessageBox.Show(Memory.ReadFloat(Memory.GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_X)) + "");
            /*Form f2 = new Form();
            f2.BackColor = Color.White;
            f2.TransparencyKey = Color.White;

            f2.Show();

            Pen p = new Pen(Color.LawnGreen, 5);
            Graphics g = Graphics.FromHwnd(f2.Handle);

            g.DrawLine(p, 960, 0, 960, 540);

            p.Dispose();
            g.Dispose();*/

            /*TEXTBOX_SavePos.Text = Memory.FindPattern(
                new byte[] { 0x48, 0x8D, 0x0D, 0x0, 0x0, 0x0, 0x0, 0x89, 0x44, 0x24, 0x30, 0xE8, 0x0, 0x0, 0x0, 0x0, 0x48, 0x83, 0xC4, 0x28, 0xC3, 0x48, 0x8B, 0x05},
                "xxx????xxxxx????xxxxxxxx"
                ) + "";*/

            /*long pedcount = Memory.GetPointerAddress(GameAddress.pedListPTR, new int[] { 0x110 });
            int maxPed = Memory.ReadInteger(pedcount, 4);
            long v26 = Memory.GetPointerAddress(GameAddress.pedListPTR, new int[] { 0x100 });

            float[] mypos = GTA5Process.GetPlayerPosition();

            for (int i = 0; i < maxPed; i++)
            {
                long npc = Memory.GetPointerAddress(v26, new int[] { i * 0x10 });

                if (Utils.Distance(
                    mypos,
                    new float[] {
                        Memory.ReadFloat(Memory.GetPointerAddress(npc, new int[] { 0x90 })),
                        Memory.ReadFloat(Memory.GetPointerAddress(npc, new int[] { 0x94 })),
                        Memory.ReadFloat(Memory.GetPointerAddress(npc, new int[] { 0x98 }))
                    }
                ) > 3)
                {
                    float health = Memory.ReadFloat(Memory.GetPointerAddress(npc, new int[] { 0x280 }));
                    if (health > 200f || health == 0f) continue;

                    GameESP.GAME_Vector2 screen_pos = GameESP.WORLD_TO_SCREEN(new GameESP.GAME_Vector3(
                        Memory.ReadFloat(Memory.GetPointerAddress(npc, new int[] { 0x90 })),
                        Memory.ReadFloat(Memory.GetPointerAddress(npc, new int[] { 0x94 })),
                        Memory.ReadFloat(Memory.GetPointerAddress(npc, new int[] { 0x98 })) + 1
                        ));

                    Pen p = new Pen(Color.LawnGreen, 5);
                    Graphics g = Graphics.FromHdc(GameESP.GetDC(IntPtr.Zero));

                    g.DrawLine(p, 960, 0, screen_pos.X, screen_pos.Y);

                    p.Dispose();
                    g.Dispose();
                    //MessageBox.Show(screen_pos.X + " "+screen_pos.Y);
                }
            }*/


        }

        private void BUTTON_Player_Name_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TEXTBOX_Player_Name.Text))
            {
                MessageBox.Show("Position name illegal!");
                return;
            }

            if (TEXTBOX_Player_Name.Text.Length > 20)
            {
                MessageBox.Show("Name illegal! must be <= 20");
                return;
            }

            GTA5Process.Set_MyName(TEXTBOX_Player_Name.Text);
        }

        private void BUTTON_Ragdoll_Click(object sender, EventArgs e)
        {
            if (GTA5Process.Get_Ragdoll())
            {
                BUTTON_Ragdoll.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.Set_Ragdoll(false);

                return;
            }

            BUTTON_Ragdoll.BackgroundImage = Properties.Resources.switch_on;
            GTA5Process.Set_Ragdoll(true);
        }

        private void BUTTON_OPMK2_Missles_Click(object sender, EventArgs e)
        {
            GTA5Process.Set_OPMK2_Missles(int.Parse(numericUpDown_OPMK2_Missles.Value.ToString()));
        }

        private void BUTTON_WANTED_LEVEL_Click(object sender, EventArgs e)
        {
            GTA5Process.Set_Wantedlevel((int)numericUpDown_WANTED_LEVEL.Value);
        }

        private void PlayerPage_Click(object sender, EventArgs e)
        {

        }

        private void BUTTON_Get_exp_Click(object sender, EventArgs e)
        {
            if (GTA5Process.CheatList["GetRP"])
            {
                BUTTON_Get_exp.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["GetRP"] = false;

                GTA5Process.Timer_RP.Stop();

                return;
            }

            GTA5Process.CheatList["GetRP"] = true;
            BUTTON_Get_exp.BackgroundImage = Properties.Resources.switch_on;

            GTA5Process.Timer_RP.Start();

        }

        private void BUTTON_OUT_RADAR_Click(object sender, EventArgs e)
        {
            if (GTA5Process.CheatList["OutRadar"])
            {
                BUTTON_OUT_RADAR.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["OutRadar"] = false;

                GTA5Process.Out_of_Radar(false);

                return;
            }

            GTA5Process.CheatList["OutRadar"] = true;
            BUTTON_OUT_RADAR.BackgroundImage = Properties.Resources.switch_on;
        }

        private void BUTTON_SetArmor_Click(object sender, EventArgs e)
        {
            long address = Memory.GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Armor);
            Memory.WriteFloat(address, float.Parse(numericUpDown1_SetArmor.Value.ToString()));
        }

        private void BUTTON_weather_snow_Click(object sender, EventArgs e)
        {
            if (GTA5Process.CheatList["Weather_Snow"])
            {
                BUTTON_weather_snow.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["Weather_Snow"] = false;

                GTA5Process.Weather_SetSnow(false);

                return;
            }

            GTA5Process.CheatList["Weather_Snow"] = true;
            BUTTON_weather_snow.BackgroundImage = Properties.Resources.switch_on;
        }

        private void BUTTON_Boost_Click(object sender, EventArgs e)
        {
            if (GTA5Process.CheatList["Vehicle_Boost"])
            {
                BUTTON_Boost.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["Vehicle_Boost"] = false;

                if (!GTA5Process.CheatList["SuperJump"] && !GTA5Process.CheatList["ExplosiveAmmo"])
                {
                    GTA5Process.Timer_PlayerFlag.Stop();
                }

                return;

            }

            GTA5Process.CheatList["Vehicle_Boost"] = true;
            BUTTON_Boost.BackgroundImage = Properties.Resources.switch_on;

            GTA5Process.Timer_PlayerFlag.Start();
        }

        private void BUTTON_SetBulletType_Click(object sender, EventArgs e)
        {
            if (BulletType_ListBox.SelectedItem == null) return;

            GTA5Process.Bullet_ImpactType = BulletType_ListBox.SelectedItem.ToString();
            GTA5Process.Set_Bullet_Impact();
            GTA5Process.Timer_PlayerFlag.Stop();

        }

        private void BUTTON_SUPERJUMP_Click(object sender, EventArgs e)
        {

            if (GTA5Process.CheatList["SuperJump"])
            {
                BUTTON_SUPERJUMP.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["SuperJump"] = false;

                if (!GTA5Process.CheatList["ExplosiveAmmo"] && !GTA5Process.CheatList["Vehicle_Boost"])
                {
                    GTA5Process.Timer_PlayerFlag.Stop();
                }

                return;
            }

            GTA5Process.CheatList["SuperJump"] = true;
            BUTTON_SUPERJUMP.BackgroundImage = Properties.Resources.switch_on;

            GTA5Process.Timer_PlayerFlag.Start();

        }

        private void BUTTON_explosiveAmmo_Click(object sender, EventArgs e)
        {
            if (GTA5Process.CheatList["ExplosiveAmmo"])
            {
                BUTTON_explosiveAmmo.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["ExplosiveAmmo"] = false;

                if (!GTA5Process.CheatList["SuperJump"] && !GTA5Process.CheatList["Vehicle_Boost"])
                {
                    GTA5Process.Timer_PlayerFlag.Stop();
                }

                return;

            }

            GTA5Process.CheatList["ExplosiveAmmo"] = true;
            BUTTON_explosiveAmmo.BackgroundImage = Properties.Resources.switch_on;

            GTA5Process.Timer_PlayerFlag.Start();
        }

        private void BUTTON_kill_attacker_Click_1(object sender, EventArgs e)
        {
            if (GTA5Process.CheatList["KillAttacker"])
            {
                BUTTON_kill_attacker.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["KillAttacker"] = false;

                GTA5Process.Timer_KillAttacker_Loop.Stop();

                return;
            }

            GTA5Process.CheatList["KillAttacker"] = true;
            BUTTON_kill_attacker.BackgroundImage = Properties.Resources.switch_on;

            GTA5Process.Timer_KillAttacker_Loop.Start();
        }

        private void BUTTON_Playerlist_refresh_Click(object sender, EventArgs e)
        {
            listBox_PlayersList.Items.Clear();
            foreach (string pn in GTA5Process.GetPlayers().Keys) {
                listBox_PlayersList.Items.Add(pn);
            }

            GTA5Process.DropPlayerName = null;
        }

        private void BUTTON_Playerlist_TPto_Click(object sender, EventArgs e)
        {
            if (listBox_PlayersList.SelectedItem == null)
            {
                MessageBox.Show("Please selected Player!");
                return;
            }

            float[] pos = GTA5Process.GetPlayerPos(listBox_PlayersList.SelectedItem.ToString());
            pos[2] += 2f;

            GTA5Process.TP_to_with_car(pos);
        }

        private void BUTTON_DropMoney_Click(object sender, EventArgs e)
        {
            if (listBox_PlayersList.SelectedItem == null)
            {
                MessageBox.Show("Please selected Player!");
                return;
            }

            long pedcount = Memory.GetPointerAddress(GameAddress.pedListPTR, new int[] { 0x110 });
            int maxPed = Memory.ReadInteger(pedcount, 4);
            long v26 = Memory.GetPointerAddress(GameAddress.pedListPTR, new int[] { 0x100 });

            float[] pos = GTA5Process.GetPlayerPos(listBox_PlayersList.SelectedItem.ToString());
            float[] mypos = GTA5Process.GetPlayerPosition();

            for (int i = 0; i < maxPed; i++)
            {
                long npc = Memory.GetPointerAddress(v26, new int[] { i * 0x10 });
                long v24 = Memory.GetPointerAddress(npc, new int[] { 0x30 });
                long v23 = Memory.GetPointerAddress(npc, new int[] { 0x20 });
                long v5 = Memory.GetPointerAddress(v23, new int[] { 0x270 });

                if (Utils.Distance(
                    mypos,
                    new float[] {
                        Memory.ReadFloat(Memory.GetPointerAddress(npc, new int[] { 0x90 })),
                        Memory.ReadFloat(Memory.GetPointerAddress(npc, new int[] { 0x94 })),
                        Memory.ReadFloat(Memory.GetPointerAddress(npc, new int[] { 0x98 }))
                    }
                ) > 3)
                {
                    float health = Memory.ReadFloat(Memory.GetPointerAddress(npc, new int[] { 0x280 }));
                    if (health > 200f || health == 0f) continue;

                    Memory.WriteInteger(Memory.GetPointerAddress(npc, new int[] { 0x15DC }), (int)numericUpDown1_dropmoney.Value, 4);
                    Memory.WriteFloat(Memory.GetPointerAddress(npc, new int[] { 0x90 }), pos[0]);
                    Memory.WriteFloat(Memory.GetPointerAddress(npc, new int[] { 0x94 }), pos[1]);
                    Memory.WriteFloat(Memory.GetPointerAddress(npc, new int[] { 0x98 }), pos[2] + 2);
                    Memory.WriteFloat(Memory.GetPointerAddress(npc, new int[] { 0x280 }), 0);
                }
            }
        }

        private void listBox_PlayersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                LABLE_Show_IPAddress.Text =
                    "IPAddress - " +
                    GTA5Process.GetPlayerIP(listBox_PlayersList.SelectedItem.ToString()).ToString() +
                    ":" +
                    GTA5Process.GetPlayerPort(listBox_PlayersList.SelectedItem.ToString()).ToString();

                LABLE_Show_Distance.Text =
                    "Distance - " +
                    Math.Round(Utils.Distance(GTA5Process.GetPlayerPosition(), GTA5Process.GetPlayerPos(listBox_PlayersList.SelectedItem.ToString()))) +
                    "M";

                GTA5Process.DropPlayerName = listBox_PlayersList.SelectedItem;

            }
            catch (Exception ex) {
                GTA5Process.DropPlayerName = null;
                LABLE_Show_Distance.Text = LABLE_Show_IPAddress.Text = "NEED RELOAD";
            }

        }

        private void BUTTON_TP_to_LastShootPos_Click(object sender, EventArgs e)
        {
            float[] pos = GTA5Process.GetLastShootPos();
            pos[2] += 1f;

            GTA5Process.TP_to_with_car(pos);
        }

        private void BUTTON_Player_Invisibility_Click(object sender, EventArgs e)
        {
            if (GTA5Process.CheatList["Invisibility"])
            {
                BUTTON_Player_Invisibility.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["Invisibility"] = false;

                GTA5Process.Set_Player_Invisibility(false);

                return;
            }

            GTA5Process.CheatList["Invisibility"] = true;
            BUTTON_Player_Invisibility.BackgroundImage = Properties.Resources.switch_on;
        }

        private void Main_Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            stopListen();
        }

        private void Main_Menu_MouseDown(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(e.Button.ToString());
        }

        private void BUTTON_Macro_Click(object sender, EventArgs e)
        {
            /*if (MacroSwitch)
            {
                if (myKeyEventHandeler != null)
                {
                    k_hook.KeyDownEvent -= myKeyEventHandeler;//取消按键事件
                    myKeyEventHandeler = null;
                    k_hook.Stop();//关闭键盘钩子
                }

                if (myMouseHandeler != null)
                {
                    m_hook.OnMouseActivity -= myMouseHandeler;//取消按键事件
                    myMouseHandeler = null;
                    m_hook.Stop();//关闭鼠标钩子
                }
            }else {
                myKeyEventHandeler = new KeyEventHandler(hook_KeyDown);
                k_hook.KeyDownEvent += myKeyEventHandeler;//钩住键按下
                k_hook.Start();//安装键盘钩子

                myMouseHandeler = new MouseEventHandler(hook_MouseButtonDown);
                m_hook.OnMouseActivity += myMouseHandeler;//钩住键按下
                m_hook.Start();//安装鼠标钩子
            }*/

            MacroSwitch = !MacroSwitch;
            BUTTON_Macro.BackgroundImage = MacroSwitch ? Properties.Resources.switch_on : Properties.Resources.switch_off;

        }

        private void BUTTON_DropPedLoop_Click(object sender, EventArgs e)
        {
            if (GTA5Process.CheatList["DropPedToSelectPlayer"])
            {
                BUTTON_DropPedLoop.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["DropPedToSelectPlayer"] = false;
                GTA5Process.Timer_DropPed.Stop();

                return;

            }

            GTA5Process.CheatList["DropPedToSelectPlayer"] = true;
            BUTTON_DropPedLoop.BackgroundImage = Properties.Resources.switch_on;

            GTA5Process.Timer_DropPed.Start();
        }

        private void BUTTON_ESP_Click(object sender, EventArgs e)
        {
            if (GTA5Process.CheatList["ESP"])
            {
                BUTTON_ESP.BackgroundImage = Properties.Resources.switch_off;
                GTA5Process.CheatList["ESP"] = false;

                GTA5Process.Timer_Game_ESP.Stop();

                return;
            }

            GTA5Process.CheatList["ESP"] = true;
            BUTTON_ESP.BackgroundImage = Properties.Resources.switch_on;

            GTA5Process.Timer_Game_ESP.Start();

        }

        private void NumericUpDown1_dropmoney_ValueChanged(object sender, EventArgs e)
        {
            GTA5Process.DropMoneyAmout = (int)numericUpDown1_dropmoney.Value;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            long address1 = Memory.GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_ACCELERATION);
            long address2 = Memory.GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_Traction_Curve_Min);
            Memory.WriteFloat(address1, float.Parse(numericUpDown1.Value.ToString()));
            Memory.WriteFloat(address2, float.Parse(numericUpDown1.Value.ToString()) == 1f? 2.4f: 10f);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            GTA5Process.SetSwimSpeed(float.Parse(numericUpDown2.Value.ToString()));
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            byte[] colors = Memory.ReadBytes(
                Memory.GetPointerAddress(
                    GameAddress.BaseAddress + GameAddress.WorldPTR,
                    GameAddress.VehicleColorState[button_ChangeCarColor.Text]
                    ),
                3
                );

            colors[2] = (byte)trackBar1.Value;
            Memory.WriteBytes(
                Memory.GetPointerAddress(
                    GameAddress.BaseAddress + GameAddress.WorldPTR,
                    GameAddress.VehicleColorState[button_ChangeCarColor.Text]
                    ),
                colors
                );

            label_color1.Text = "R : " + trackBar1.Value;
        }

        private void TrackBar2_ValueChanged(object sender, EventArgs e)
        {
            byte[] colors = Memory.ReadBytes(
                Memory.GetPointerAddress(
                    GameAddress.BaseAddress + GameAddress.WorldPTR,
                    GameAddress.VehicleColorState[button_ChangeCarColor.Text]
                    ),
                3
                );

            colors[1] = (byte)trackBar2.Value;
            Memory.WriteBytes(
                Memory.GetPointerAddress(
                    GameAddress.BaseAddress + GameAddress.WorldPTR,
                    GameAddress.VehicleColorState[button_ChangeCarColor.Text]
                    ),
                colors
                );

            label_color2.Text = "G : " + trackBar2.Value;
        }

        private void TrackBar3_ValueChanged(object sender, EventArgs e)
        {
            byte[] colors = Memory.ReadBytes(
                Memory.GetPointerAddress(
                    GameAddress.BaseAddress + GameAddress.WorldPTR,
                    GameAddress.VehicleColorState[button_ChangeCarColor.Text]
                    ),
                3
                );

            colors[0] = (byte)trackBar3.Value;
            Memory.WriteBytes(
                Memory.GetPointerAddress(
                    GameAddress.BaseAddress + GameAddress.WorldPTR,
                    GameAddress.VehicleColorState[button_ChangeCarColor.Text]
                    ),
                colors
                );

            label_color3.Text = "B : " + trackBar3.Value;

        }

        private void Button_ChangeCarColor_Click(object sender, EventArgs e)
        {
            switch (button_ChangeCarColor.Text)
            {
                case "1-VehicleColor":
                    button_ChangeCarColor.Text = "2-VehicleColor";
                    break;
                case "2-VehicleColor":
                    button_ChangeCarColor.Text = "3-WheelColor";
                    break;
                case "3-WheelColor":
                    button_ChangeCarColor.Text = "1-VehicleColor";
                    break;
            }
        }
    }
}
