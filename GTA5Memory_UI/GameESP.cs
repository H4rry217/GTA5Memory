using GTA5Memory_UI.GameProcess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GTA5Memory_UI
{
    class GameESP
    {

        [DllImport("user32.dll")]
        public extern static IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        public static ESP_SCREEN ESP_Form;

        public struct GAME_Vector3
        {
            public float X;
            public float Y;
            public float Z;

            public GAME_Vector3(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }

        public struct GAME_Vector4
        {
            public float X;
            public float Y;
            public float Z;
            public float W;

            public GAME_Vector4(float x, float y, float z, float w)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
            }
        }

        public struct GAME_Vector2
        {
            public float X;
            public float Y;

            public GAME_Vector2(float x, float y)
            {
                X = x;
                Y = y;
            }
        }

        public struct GAME_Matrix_4x4
        {
            public float _11; public float _12; public float _13; public float _14;
            public float _21; public float _22; public float _23; public float _24;
            public float _31; public float _32; public float _33; public float _34;
            public float _41; public float _42; public float _43; public float _44;

            public GAME_Matrix_4x4(
                float v11, float v12, float v13, float v14,
                float v21, float v22, float v23, float v24,
                float v31, float v32, float v33, float v34,
                float v41, float v42, float v43, float v44
                )
            {
                _11 = v11; _12 = v12; _13 = v13; _14 = v14;
                _21 = v21; _22 = v22; _23 = v23; _24 = v24;
                _31 = v31; _32 = v32; _33 = v33; _34 = v34;
                _41 = v41; _42 = v42; _43 = v43; _44 = v44;
            }
        }

        public static GAME_Vector2 WORLD_TO_SCREEN(GAME_Vector3 pos)
        {
            //long view_ptr = Memory.GetPointerAddress(GameAddress.BaseAddress + 0x0208BAF0, new int[] { 0x24C });
            long view_ptr = Memory.GetPointerAddress(GameAddress.BaseAddress + 0X01F3E120, new int[] { 0x24C });

            GAME_Matrix_4x4 viewmatrix = new GAME_Matrix_4x4(
                Memory.ReadFloat(view_ptr), Memory.ReadFloat(view_ptr + 16), Memory.ReadFloat(view_ptr + 32), Memory.ReadFloat(view_ptr + 48),
                Memory.ReadFloat(view_ptr + 4), Memory.ReadFloat(view_ptr + 20), Memory.ReadFloat(view_ptr + 36), Memory.ReadFloat(view_ptr + 52),
                Memory.ReadFloat(view_ptr + 8), Memory.ReadFloat(view_ptr + 24), Memory.ReadFloat(view_ptr + 40), Memory.ReadFloat(view_ptr + 56),
                Memory.ReadFloat(view_ptr + 12), Memory.ReadFloat(view_ptr + 28), Memory.ReadFloat(view_ptr + 44), Memory.ReadFloat(view_ptr + 60)
                );

            GAME_Vector4 vec_x = new GAME_Vector4(viewmatrix._21, viewmatrix._22, viewmatrix._23, viewmatrix._24);
            GAME_Vector4 vec_y = new GAME_Vector4(viewmatrix._31, viewmatrix._32, viewmatrix._33, viewmatrix._34);
            GAME_Vector4 vec_z = new GAME_Vector4(viewmatrix._41, viewmatrix._42, viewmatrix._43, viewmatrix._44);

            GAME_Vector3 screen_pos = new GAME_Vector3(
                (vec_x.X * pos.X) + (vec_x.Y * pos.Y) + (vec_x.Z * pos.Z) + vec_x.W,
                (vec_y.X * pos.X) + (vec_y.Y * pos.Y) + (vec_y.Z * pos.Z) + vec_y.W,
                (vec_z.X * pos.X) + (vec_z.Y * pos.Y) + (vec_z.Z * pos.Z) + vec_z.W
                );

            if (pos.Z <= 0.1f)
            {
                return new GAME_Vector2(0, 0);
            }

            screen_pos.Z = 1f / screen_pos.Z;
            screen_pos.X *= screen_pos.Z;
            screen_pos.Y *= screen_pos.Z;

            int width = 1920;
            int height = 1080;

            float x_temp = width / 2;
            float y_temp = height / 2;

            screen_pos.X += x_temp + (0.5f * screen_pos.X * width + 0.5f);
            screen_pos.Y = y_temp - (0.5f * screen_pos.Y * height + 0.5f);

            return new GAME_Vector2(screen_pos.X, screen_pos.Y);

        }

    }
}
