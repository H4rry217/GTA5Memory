using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Timers;
using System.Windows;

namespace GTA5Memory_UI.GameProcess
{
    class GTA5Process : Memory
    {
        public static Dictionary<string, bool> CheatList = new Dictionary<string, bool>();

        public static SortedDictionary<string, long> PlayersList = new SortedDictionary<string, long>();

        public static string PositionFile;

        public static bool RP_Wanted = false;
        public static string Bullet_ImpactType = "Default_Bullet";

        public static string ExeName = "GTA5";

        public static IntPtr GTAHandle;

        public static object DropPlayerName = null;

        public static int DropMoneyAmout = 2000;

        public static Timer Timer_RP;

        public static Timer Timer_PlayerFlag;

        public static Timer Timer_KillAttacker_Loop;

        public static Timer Timer_DropPed;

        public static Timer Timer_Game_ESP;

        public static void initHack()
        {
            
            GameAddress.BaseAddress = GetBaseAddress("GTA5.exe");
            TXTConvert.Init();
            //DrawScreen.Init();

            CheatList.Add("Godmode", Get_Godmode());
            CheatList.Add("VehGodmode", Get_Vehicle_Godmode());
            CheatList.Add("InfiniteAmmo", Get_Infinite_Ammo());
            CheatList.Add("GetRP", false);
            CheatList.Add("SuperJump", false);
            CheatList.Add("ExplosiveAmmo", false);
            CheatList.Add("OutRadar", false);
            CheatList.Add("Weather_Snow", false);
            CheatList.Add("Vehicle_Boost", false);
            CheatList.Add("KillAttacker", false); 
            CheatList.Add("Invisibility", false);
            CheatList.Add("DropPedToSelectPlayer", false);
            CheatList.Add("ESP", false);

            /*SharpDX.Direct2D1.Factory factory = new SharpDX.Direct2D1.Factory(FactoryType.SingleThreaded);

            RenderTargetProperties renderProps = new RenderTargetProperties(new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied));

            HwndRenderTargetProperties properties = new HwndRenderTargetProperties
            {
                Hwnd = FindWindow(null, "Grand Theft Auto V"),
                PixelSize = new Size2(1920, 1080),
                PresentOptions = PresentOptions.None
            };
            Renden_Device = new WindowRenderTarget(factory, renderProps, properties);*/


            if (!GameAddress.BaseAddress.Equals(IntPtr.Zero.ToInt64())) {

                Timer timer = new Timer(5000);
                timer.AutoReset = true;
                timer.Elapsed += Timer_Refresh;
                timer.Start();

                Timer_RP = new Timer(1);
                Timer_RP.AutoReset = true;
                Timer_RP.Elapsed += Timer_RP_Refresh;

                Timer_KillAttacker_Loop = new Timer(1000);
                Timer_KillAttacker_Loop.AutoReset = true;
                Timer_KillAttacker_Loop.Elapsed += Timer_KillAttacker_Refresh;

                Timer_DropPed = new Timer(1000);
                Timer_DropPed.AutoReset = true;
                Timer_DropPed.Elapsed += Timer_DropPed_Refresh;

                Timer_Game_ESP = new Timer(300);
                Timer_Game_ESP.AutoReset = true;
                Timer_Game_ESP.Elapsed += Timer_ESP_Refresh;

                Timer_PlayerFlag = new Timer(1);
                Timer_PlayerFlag.AutoReset = true;
                Timer_PlayerFlag.Elapsed += Timer_PlayerFlag_Refresh;

            }

            GameAddress.pedListPTR = GetPointerAddress(GameAddress.BaseAddress + GameAddress.ReplayInterface, new int[] { 0x18 });
        }

        public static Process GetProcess()
        {
            Process[] pr = Process.GetProcessesByName(ExeName);

            if (pr.Length == 1)
            {
                return pr[0];
            }

            return null;
        }

        public static IntPtr GetHandleByProcess(Process p)
        {
            try
            {

                GTAHandle = p.Handle;

                return GTAHandle;
            }
            catch
            {
                return IntPtr.Zero;
            }

        }

        public static IntPtr GetHandle()
        {
            return GetHandleByProcess(GetProcess());
        }

        public static bool IsGameRunning()
        {
            return GetProcess() != null;
        }

        public static long GetBaseAddress(string ModuleName)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(ExeName);
                ProcessModuleCollection modules = processes[0].Modules;
                ProcessModule DLLBaseAddress = null;

                foreach (ProcessModule i in modules)
                {
                    if (i.ModuleName == ModuleName)
                    {
                        DLLBaseAddress = i;
                    }
                }

                return DLLBaseAddress.BaseAddress.ToInt64();
            }
            catch
            {
                return 0;
            }
        }

        public static void Timer_Refresh(object sender, ElapsedEventArgs e)
        {
            if (CheatList["Godmode"]) {
                Set_Godmode(true);
            }

            if (CheatList["VehGodmode"])
            {
                Set_Vehicle_Godmode(true);
            }

            if (CheatList["InfiniteAmmo"])
            {
                Set_Infinite_Ammo(true);
            }

            if (CheatList["OutRadar"])
            {
                Out_of_Radar(true);
            }

            if (CheatList["Weather_Snow"])
            {
                Weather_SetSnow(true);
            }

            if (CheatList["Invisibility"])
            {
                Set_Player_Invisibility(true);
            }

        }

        public static void Timer_RP_Refresh(object sender, ElapsedEventArgs e)
        {
            if (CheatList["GetRP"])
            {

                RP_Wanted = !RP_Wanted;

                if (RP_Wanted)
                {
                    Set_Wantedlevel(5);
                    return;
                }

                Set_Wantedlevel(0);
            }
        }

        public static void Timer_PlayerFlag_Refresh(object sender, ElapsedEventArgs e)
        {

            long point = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSET_PLAYER_FLAG);
            long point1 = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_BOOST);

            if (CheatList["Vehicle_Boost"])
            {
                if(In_Vehicle()) WriteFloat(point1, 2.25f);
            }

            if (CheatList["SuperJump"] && CheatList["ExplosiveAmmo"])
            {
                WriteBytes(point, new byte[] { 0x48 });
                return;
            }

            if (CheatList["SuperJump"])
            {
                WriteBytes(point, new byte[] { 0x40 });
                return;
            }

            if (CheatList["ExplosiveAmmo"])
            {
                WriteBytes(point, new byte[] { 0x08 });
                return;
            }

        }

        public static void Set_Bullet_Impact() {
            long pointer1 = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Bullet_Impact_Type);
            long pointer2 = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Bullet_Impact_Bullet);

            if (Bullet_ImpactType.Equals("Default_Bullet")) {
                WriteInteger(pointer1, GameAddress.Button_ImpactType["Bullet"], 4);
                WriteInteger(pointer2, GameAddress.Button_Impact["Default_Bullet"], 4);
                return;
            }

            WriteInteger(pointer1, GameAddress.Button_ImpactType["Explosive"], 4);
            WriteInteger(pointer2, GameAddress.Button_Impact[Bullet_ImpactType], 4);
        }

        public static void SetRunSpeed(float speed) {
            long pointer = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.SprintSpeed);
            WriteFloat(pointer, speed);
        }

        public static void SetSwimSpeed(float speed)
        {
            long pointer = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.SwimSpeed);
            WriteFloat(pointer, speed);
        }

        public static float[] GetPlayerPosition()
        {
            
            return new float[]
            {
                ReadFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_X)),
                ReadFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Y)),
                ReadFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Z))
            };
        }

        public static float[] GetWayPoint()
        {
            /*long address = GetPointerAddress(BaseAddress + WAYPOINT_X) - 0x10;

            for (int i = 0; i < 1000; i++)
            {
                GetPointerAddress(BaseAddress + WAYPOINT_X)
            }*/

            return new float[]
            {
                ReadFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.OFFSETS_WAYPOINT_X)),
                ReadFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.OFFSETS_WAYPOINT_Y)),
                800f
            };

            /*ReadFloat(GetPointerAddress(BaseAddress + WorldPTR, OFFSETS_Z))*/
        }

        public static void TP_to_with_car(float[] pos)
        {
            WriteFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_X), pos[0]);
            WriteFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Y), pos[1]);
            WriteFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Z), pos[2]);

            if (In_Vehicle())
            {
                WriteFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_X), pos[0]);
                WriteFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_Y), pos[1]);
                WriteFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_Z), pos[2]);

            }
        }

        public static void Set_Player_Invisibility(bool bl) {
            long point = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Player_Invisibility);
            WriteBytes(point, new byte[] { (bl ? Convert.ToByte(1) : Convert.ToByte(39)) });

        }

        public static void Set_Godmode(bool enabled)
        {
            long pointer = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Godmode);
            if (enabled == true)
            {
                WriteBytes(pointer, new byte[] { 0x1 });
            }
            else
            {
                WriteBytes(pointer, new byte[] { 0x0 });
            }
        }

        public static bool Get_Godmode()
        {
            long pointer = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Godmode);
            return ReadBytes(pointer, 1)[0] == 0x01;
        }

        public static void Weather_SetSnow(bool enabled)
        {
            long pointer = GetPointerAddress(GameAddress.BaseAddress + GameAddress.PointerAddressA + 0x08, GameAddress.OFFSETS_WORLD_SNOW);
            if (enabled == true)
            {
                WriteBytes(pointer, new byte[] { 0x1 });
            }
            else
            {
                WriteBytes(pointer, new byte[] { 0x0 });
            }
        }

        public static bool Weather_GetSnow()
        {
            long pointer = GetPointerAddress(GameAddress.BaseAddress + GameAddress.PointerAddressA + 0x08, GameAddress.OFFSETS_WORLD_SNOW);
            return ReadBytes(pointer, 1)[0] == 0x01;
        }

        public static void Set_Infinite_Ammo(bool? enabled)
        {
            long pointer = GetPointerAddress(GameAddress.BaseAddress + GameAddress.AmmoPTR);
            if (enabled == true)
            {
                WriteBytes(pointer, GameAddress.Ammo1);
            }
            else
            {
                WriteBytes(pointer, GameAddress.Ammo2);
            }
        }

        public static bool Get_Infinite_Ammo()
        {
            long pointer = GetPointerAddress(GameAddress.BaseAddress + GameAddress.AmmoPTR);
            byte[] a = ReadBytes(pointer,3);

            return a[0] == GameAddress.Ammo1[0] && a[1] == GameAddress.Ammo1[1] && a[2] == GameAddress.Ammo1[2];
        }

        public static bool In_Vehicle()
        {
            long pointer = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_InVehicle);
            return ReadBytes(pointer, 2)[0] != 16;
        }

        public static float[] Get_Vehicle_Health()
        {
            return new float[]
            {
                ReadFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_HEALTH1)),
                ReadFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_HEALTH2)),
                ReadFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_HEALTH3))
            };
        }

        public static void Set_Vehicle_Health(float health)
        {
            long pointer11 = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_HEALTH1);
            long pointer22 = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_HEALTH2);
            long pointer33 = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_HEALTH3);

            WriteFloat(pointer11, health);
            WriteFloat(pointer22, health);
            WriteFloat(pointer33, health);
        }

        public static float Get_Vehicle_Gravity()
        {
            return ReadFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_Gravity));
        }

        public static int GetPlayerCount() {
            return ReadInteger(GetPointerAddress(GameAddress.BaseAddress + GameAddress.PlayerCountPTR_STATIC), 4);
        }

        public static void Set_Vehicle_Gravity(float gravity)
        {
            WriteFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_Gravity), gravity);
        }

        public static void Set_Vehicle_Godmode(bool enabled)
        {
            long pointer = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_GODMODE);
            if (enabled)
            {
                WriteBytes(pointer, new byte[] { 0x1 });
            }
            else
            {
                WriteBytes(pointer, new byte[] { 0x0 });
            }
        }

        public static bool Get_Vehicle_Godmode()
        {
            long pointer = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_GODMODE);
            return ReadBytes(pointer, 1)[0] == 0x01;
        }

        public static float Get_Bullet_Damage()
        {
            long pointeraa = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_BULLET_DAMAGE);
            return ReadFloat(pointeraa);
        }

        public static void Set_Bullet_Damage(float damage)
        {
            long pointeraa = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_BULLET_DAMAGE);
            WriteFloat(pointeraa, damage);
        }

        public static void Set_MyName(string name)
        {
            WriteString(GetPointerAddress(GameAddress.BaseAddress + GameAddress.PlayerCountPTR, GameAddress.OFFSETS_Player_Name_Local), "                    ");
            WriteString(GetPointerAddress(GameAddress.BaseAddress + GameAddress.PlayerCountPTR, GameAddress.OFFSETS_Player_Name_Local), name);
        }

        public static bool Get_Ragdoll()
        {
            long pointeraa = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Ragdoll);
            return ReadBytes(pointeraa, 1)[0] == 0x20;        
        }

        public static void Set_Ragdoll(bool enable)
        {
            long pointeraa = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Ragdoll);
            if (enable) {
                WriteBytes(pointeraa, new byte[] { 0x20 });
                return;
            }

            WriteBytes(pointeraa, new byte[] { 0x00 });
        }

        public static void Set_OPMK2_Missles(int count)
        {
            long pointeraa = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_VEHICLE_OPMK2_MISSLES);
            WriteBytes(pointeraa, new byte[] {byte.Parse(count.ToString()) });
        }

        public static void Set_Wantedlevel(int level)
        {
            long pointeraa = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_WANTED_LEVEL);
            WriteInteger(pointeraa, level, 4);
        }

        public static void Out_of_Radar(bool a)
        {
            long ee = ReadPointer(GameAddress.BaseAddress + GameAddress.GlobalPTR2 + 0x48);
            int c = ReadInteger(ee + 0x97CF0, 4);

            if (a)
            {
                int d = ReadInteger(ee + 0x716B0, 4);
                MessageBox.Show(ReadInteger(ee + 0x97F20, 4).ToString()+"     "+d.ToString());
                //WriteInteger(ee + 0x97F20, d + 5341000, 4);
                //WriteInteger(ee + 0x7E610 + 0xCE8 * c, 1, 4);

                return;
            }

            //WriteInteger(ee + 0x7E610 + 0xCE8 * c, 0, 4);

        }

        public static void Timer_KillAttacker_Refresh(object sender, ElapsedEventArgs e) {
            foreach (long npc in GetAttackers())
            {
                /*float[] pos = GetAttackerPos(npc);
                pos[2] += 100f;
                SetAttackerPos(npc, pos);*/
                WriteFloat(GetPointerAddress(npc, new int[] { 0x280 }), 0f);
                WriteInteger(GetPointerAddress(npc, new int[] { 0x15D4 }), 10000, 4);
            }

            long pointerhp = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_HP);
            long pointermaxhp = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_Max_HP);

            WriteFloat(pointerhp, ReadFloat(pointermaxhp));
        }

        public static void Timer_ESP_Refresh(object sender, ElapsedEventArgs e)
        {
            /*SortedDictionary<string, long> players = GetPlayers();

            Pen p = new Pen(Color.Green, 5);
            Graphics g = Graphics.FromHwnd(FindWindow(null, "Grand Theft Auto V"));


            foreach (string pname in players.Keys)
            {
                float[] otherplayerpos = GetPlayerPos(pname);
                float distance = Utils.Distance(GetPlayerPosition(), otherplayerpos);

                if (distance > 2f && distance < 1000f)
                {
                   
                    GameESP.GAME_Vector2 screen_pos = GameESP.WORLD_TO_SCREEN(new GameESP.GAME_Vector3(otherplayerpos[0], otherplayerpos[1], otherplayerpos[2] + 1));
                    g.DrawLine(p, 960, 0, screen_pos.X, screen_pos.Y);
                    g.DrawRectangle(p, screen_pos.X - 5, screen_pos.Y, 10, 20);
                    g.DrawString(Math.Round(distance) + "m   " + pname, new Font("Arial", 20), new SolidBrush(Color.Red), screen_pos.X - 60, screen_pos.Y + 30);
                }

            }

            p.Dispose();
            g.Dispose();*/
            //GameESP.ESP_Form.Update();
            GameESP.ESP_Form.Invalidate();
        }

        public static void Timer_DropPed_Refresh(object sender, ElapsedEventArgs e)
        {
            if (DropPlayerName == null)
            {
                Timer_DropPed.Stop();
                return;
            }

            long pedcount = GetPointerAddress(GameAddress.pedListPTR, new int[] { 0x110 });
            int maxPed = ReadInteger(pedcount, 4);
            long v26 = GetPointerAddress(GameAddress.pedListPTR, new int[] { 0x100 });
            
            float[] pos = GetPlayerPos(DropPlayerName.ToString());
            float[] mypos = GetPlayerPosition();
            
            for (int i = 0; i < maxPed; i++)
            {
                long npc = GetPointerAddress(v26, new int[] { i * 0x10 });
                long v24 = GetPointerAddress(npc, new int[] { 0x30 });
                long v23 = GetPointerAddress(npc, new int[] { 0x20 });
                long v5 = GetPointerAddress(v23, new int[] { 0x270 });

                if (Utils.Distance(
                    mypos,
                    new float[] {
                        ReadFloat(GetPointerAddress(npc, new int[] { 0x90 })),
                        ReadFloat(GetPointerAddress(npc, new int[] { 0x94 })),
                        ReadFloat(GetPointerAddress(npc, new int[] { 0x98 }))
                    }
                ) > 3)
                {
                    float health = ReadFloat(GetPointerAddress(npc, new int[] { 0x280 }));
                    if (health > 200f || health == 0f) continue;

                    WriteInteger(GetPointerAddress(npc, new int[] { 0x15DC }), DropMoneyAmout, 4);
                    WriteFloat(GetPointerAddress(npc, new int[] { 0x90 }), pos[0]);
                    WriteFloat(GetPointerAddress(npc, new int[] { 0x94 }), pos[1]);
                    WriteFloat(GetPointerAddress(npc, new int[] { 0x98 }), pos[2] + 2);
                    WriteFloat(GetPointerAddress(npc, new int[] { 0x280 }), 0);
                }
            }
        }

        public static List<long> GetAttackers() {
            List<long> attackers = new List<long>();
            long attackbase = GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, new int[] { 0x08, 0x2A8 });

            for (var i = 0; i < 10; i++)
            {
                long npcc = GetPointerAddress(attackbase, new int[] { (i * 0x18) });
      
                float npcx = ReadFloat(GetPointerAddress(npcc, new int[] { 0x30, 0x50 }));

                if (npcx == 0f) continue;
                //if (Utils.Distance(GetAttackerPos(npcc), GetPlayerPosition()) < 5) continue;
                attackers.Add(npcc);

            }

            return attackers;
        }

        public static float[] GetAttackerPos(long attacker_address) {
            return new float[] {
                ReadFloat(GetPointerAddress(attacker_address, new int[]{ 0x30, 0x50})),
                ReadFloat(GetPointerAddress(attacker_address, new int[]{ 0x30, 0x54})),
                ReadFloat(GetPointerAddress(attacker_address, new int[]{ 0x30, 0x58}))
            };
        }

        public static void SetAttackerPos(long attacker_address, float[] pos)
        {
            WriteFloat(GetPointerAddress(attacker_address, new int[] { 0x30, 0x50 }), pos[0]);
            WriteFloat(GetPointerAddress(attacker_address, new int[] { 0x30, 0x54 }), pos[1]);
            WriteFloat(GetPointerAddress(attacker_address, new int[] { 0x30, 0x58 }), pos[2]);
        }

        public static SortedDictionary<string, long> GetPlayers()
        {
            SortedDictionary<string, long> playerlist = new SortedDictionary<string, long>();
            int max = GetPlayerCount();

            for (int i = 0; i < max; i++)
            {
                long otherplayerbase = GetPointerAddress(GameAddress.BaseAddress + GameAddress.PlayerPTR, new int[] { (0x08 * i) + 0x180 });
                if (ReadFloat(GetPointerAddress(otherplayerbase, GameAddress.OFFSETS_OTHERPLAYER_X)) == 0f) {
                    max++;
                    continue;
                }

                if (!playerlist.ContainsKey(GetPlayerName(otherplayerbase)))
                {
                    playerlist.Add(GetPlayerName(otherplayerbase), otherplayerbase);
                }
                
            }
            
            PlayersList = playerlist;

            return PlayersList;
        }

        public static string GetPlayerName(long playeraddress) {
            return ReadString(GetPointerAddress(playeraddress, GameAddress.OFFSETS_OTHERPLAYER_Name), 32);
        }

        public static float[] GetPlayerPos(string name) {
            if (!PlayersList.ContainsKey(name)) {
                return new float[] {
                    0f,
                    0f,
                    0f,
                };
            }

            return new float[] {
                ReadFloat(GetPointerAddress(PlayersList[name], GameAddress.OFFSETS_OTHERPLAYER_X)),
                ReadFloat(GetPointerAddress(PlayersList[name], GameAddress.OFFSETS_OTHERPLAYER_Y)),
                ReadFloat(GetPointerAddress(PlayersList[name], GameAddress.OFFSETS_OTHERPLAYER_Z)),
            };
        }

        public static IPAddress GetPlayerIP(string name) {
            if (!PlayersList.ContainsKey(name)) return IPAddress.None;
            IPAddress ip;

            try
            {
                ip = IPAddress.Parse(ReadInteger(GetPointerAddress(PlayersList[name], GameAddress.OFFSETS_OTHERPLAYER_IP), 4).ToString());
            } catch (Exception e) {
                ip = IPAddress.None;
            }

            return ip;
        }

        public static int GetPlayerPort(string name) {
            if (!PlayersList.ContainsKey(name)) return 0;
            int port;

            try
            {
                port = ReadInteger(GetPointerAddress(PlayersList[name], GameAddress.OFFSETS_OTHERPLAYER_PORT), 16);
            }
            catch (Exception e)
            {
                port = 0;
            }

            return port;
        }

        public static float[] GetLastShootPos() {
            return new float[] {
                ReadFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_LAST_SHOOT_POS_X)),
                ReadFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_LAST_SHOOT_POS_Y)),
                ReadFloat(GetPointerAddress(GameAddress.BaseAddress + GameAddress.WorldPTR, GameAddress.OFFSETS_LAST_SHOOT_POS_Z)),
            };
        }

    }
}
