using System.Collections.Generic;

namespace GTA5Memory_UI
{
    class GameAddress
    {

        public static long BaseAddress;

        public static int WorldPTR = 0x249C050;//20190724
        public static int AmmoPTR = 0xED45C9;//20190724
        public static int ClipPTR = 0xED4584;
        public static int BlipPTR = 0x1F21C40;//20190724
        public static int PointerAddressA = 0x2D0F3F0;//20181230
        public static int PlayerCountPTR = 0x2894C40;// 20190725 XXXXX
        public static int PlayerCountPTR_STATIC = 0x1F1EC50; //20190726
        public static int GlobalPTR = 0x224D43;//20190725
        public static int GlobalPTR2 = 0x2D2EE60;//20190805
        public static int PlayerPTR = 0x1D68738;//20190725
        public static int ReplayInterface = 0x1E97418;//20190724

        /*
        WorldPTR -> 48 8B 05 ? ? ? ? 45 ? ? ? ? 48 8B 48 08 48 85 C9 74 07
        BlipPTR -> 4C 8D 05 ? ? ? ? 0F B7 C1
        PlayerCountPTR -> 48 8B 0D ? ? ? ? E8 ? ? ? ? 48 8B C8 E8 ? ? ? ? 48 8B CF
        GetPointerAddressA -> 4D 89 B4 F7 ? ? ? ? 48 8B 74 24
        GlobalPTR -> 4C 8D 05 ? ? ? ? 4D 8B 08 4D 85 C9 74 11
        */

        public static long pedListPTR;

        //ammo
        public static byte[] Ammo1 = new byte[] { 0x90, 0x90, 0x90 };
        public static byte[] Ammo2 = new byte[] { 0x41, 0x2B, 0xD1 };
        public static int[] OFFSETS_BULLET_DAMAGE = new int[] { 0x8, 0x10C8, 0x20, 0xB0 };
        public static int[] OFFSETS_Bullet_Impact_Type = new int[] { 0x08, 0x10C8, 0x20, 0x20 };
        public static int[] OFFSETS_Bullet_Impact_Bullet = new int[] { 0x08, 0x10C8, 0x20, 0x24 };

        //player
        public static int[] OFFSETS_Max_HP = new int[] { 0x08, 0x2A0 };
        public static int[] OFFSETS_HP = new int[] { 0x08, 0x280 };
        public static int[] OFFSETS_Godmode = new int[] { 0x08, 0x189 };
        public static int[] OFFSETS_Armor = new int[] { 0x8, 0x14B8 };
        public static int[] OFFSETS_InVehicle = new int[] { 0x08, 0x146F };
        public static int[] OFFSETS_Player_Name_Local = new int[] { 0x08, 0x10B8, 0x7C };
        public static int OFFSETS_Player_Name = 0x451CEC;
        public static int[] OFFSETS_Ragdoll = new int[] { 0x08, 0x10A8};
        public static int[] OFFSETS_WANTED_LEVEL = new int[] { 0x08 , 0x10B8 , 0x818};
        public static int[] OFFSETS_LAST_SHOOT_POS_X = new int[] { 0x08, 0x10C8, 0x1B0 };
        public static int[] OFFSETS_LAST_SHOOT_POS_Y = new int[] { 0x08, 0x10C8, 0x1B4 };
        public static int[] OFFSETS_LAST_SHOOT_POS_Z = new int[] { 0x08, 0x10C8, 0x1B8 };
        public static int[] OFFSETS_Player_Invisibility = new int[] { 0x08, 0x2c };

        //TP
        public static int[] OFFSETS_X = new int[] { 0x8, 0x30, 0x50 };
        public static int[] OFFSETS_Y = new int[] { 0x8, 0x30, 0x54 };
        public static int[] OFFSETS_Z = new int[] { 0x8, 0x30, 0x58 };
        public static int[] OFFSETS_VEHICLE_X = new int[] { 0x8, 0xD28, 0x30, 0x50 };
        public static int[] OFFSETS_VEHICLE_Y = new int[] { 0x8, 0xD28, 0x30, 0x54 };
        public static int[] OFFSETS_VEHICLE_Z = new int[] { 0x8, 0xD28, 0x30, 0x58 };

        public static int OFFSETS_WAYPOINT_X = 0x1F329B8;
        public static int OFFSETS_WAYPOINT_Y = 0x1F329BC;

        //speed
        public static int[] SprintSpeed = new int[] { 0x08, 0x10b8, 0x14C };
        public static int[] SwimSpeed = new int[] { 0x08, 0x10b8, 0x148 };

        //vehicle
        public static int[] OFFSETS_VEHICLE_Gravity = new int[] { 0x08, 0xD28, 0xC1C };
        public static int[] OFFSETS_VEHICLE_GODMODE = new int[] { 0x08, 0xD28, 0x189 };
        public static int[] OFFSETS_VEHICLE_HEALTH1 = new int[] { 0x08, 0xD28, 0x280 };
        public static int[] OFFSETS_VEHICLE_HEALTH2 = new int[] { 0x08, 0xD28, 0x8E8 };
        public static int[] OFFSETS_VEHICLE_HEALTH3 = new int[] { 0x08, 0xD28, 0x824 };
        public static int[] OFFSETS_VEHICLE_BOOST = new int[] { 0x08, 0xD28, 0x320 };
        public static int[] OFFSETS_VEHICLE_OPMK2_MISSLES = new int[] { 0x08, 0xD28 , 0x1264 };
        public static int[] OFFSETS_VEHICLE_ACCELERATION = new int[] { 0x08, 0xD28, 0x918, 0x4c};
        public static int[] OFFSETS_VEHICLE_Traction_Curve_Min = new int[] { 0x08, 0xD28, 0x918, 0x90 };
        public static int[] OFFSETS_VEHICLE_COLOR1 = new int[] { 0x08, 0xD28, 0x48, 0x20, 0xA4 };
        public static int[] OFFSETS_VEHICLE_COLOR2 = new int[] { 0x08, 0xD28, 0x48, 0x20, 0xA8 };
        public static int[] OFFSETS_VEHICLE_COLORWHEEL = new int[] { 0x08, 0xD28, 0x48, 0x20, 0xB0 };


        //other
        public static int[] OFFSETS_WORLD_SNOW = new int[] { 0x9390 };

        //otherplayer
        public static int[] OFFSETS_OTHERPLAYER_Name = new int[] { 0xA8, 0x7C };
        public static int[] OFFSETS_OTHERPLAYER_X = new int[] { 0xA8, 0x1C8, 0x90 };
        public static int[] OFFSETS_OTHERPLAYER_Y = new int[] { 0xA8, 0x1C8, 0x94 };
        public static int[] OFFSETS_OTHERPLAYER_Z = new int[] { 0xA8, 0x1C8, 0x98 };
        public static int[] OFFSETS_OTHERPLAYER_IP = new int[] { 0xA8, 0x47 };
        public static int[] OFFSETS_OTHERPLAYER_PORT = new int[] { 0xA8, 0x48 };

        //test
        public static int[] Explosive_AMMO = new int[] { 0x08, 0x10B8, 0x1F8, 0x10 };
        public static int[] OFFSET_PLAYER_FLAG = new int[] { 0x08, 0x10b8, 0x1F9 };

        //Bullet impact type
        public static Dictionary<string, int> Button_ImpactType = new Dictionary<string, int> {
            {"Fist",  2},
            {"Bullet",  3},
            {"Explosive",  5}
        };

        public static Dictionary<string, int> Button_Impact = new Dictionary<string, int> {
            {"Default_Bullet", -1},
            {"Grenade_Explosive", 1},
            {"Sticky_Bomb_Explosive", 2},
            {"Moltov_Coctail_Explosive", 3},
            {"Big_Explosive", 4},
            {"Medium_Explosive", 40},
            {"Tiny_Explosion", 25},
            {"Big_Firey_Explosive", 5},
            {"Small_Water_Spray", 11},
            {"Small_Fire_Spray", 12},
            {"Big_Water_Spray", 13},
            {"Big_Fire_Spray", 14},
            {"MK2_Explosive_Bullets", 18},
            {"Smoke_Grenades", 19},
            {"Tear_Gas", 20},
            {"Tear_Gas_2", 21},
            {"Red_Flare_Smoke", 22},
            {"Cool_Ground_Explosion", 23},
            {"Crazy_ShockWave_Explosion", 26},
            {"Huge_Firey_Explosion", 28},
            {"Massive_Blimp_Explosion", 29},
            {"Massive_Blimp_Explosion_2", 37},
            {"Big_Fire_Spray_2", 30},
            {"Large_Explosion_Falling_Debris", 31},
            {"Fire_Ball_Explosion", 32},
            {"Firework_Explosion", 38},
            {"Snowball_Hit", 39},
            {"Tiny_Explosion_2", 33},
            {"Screen_Shake", 41},
            {"Spoof_Explosion", 99},
        };

        public static Dictionary<string, int[]> VehicleColorState = new Dictionary<string, int[]>
            {
                {"1-VehicleColor", OFFSETS_VEHICLE_COLOR1},
                {"2-VehicleColor", OFFSETS_VEHICLE_COLOR2},
                {"3-WheelColor", OFFSETS_VEHICLE_COLORWHEEL},
            };

    }
}
