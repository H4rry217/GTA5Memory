using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GTA5Memory_UI.GameProcess
{
    class Memory
    {

        [DllImport("kernel32.dll")]
        public static extern int WriteProcessMemory(IntPtr Handle, long Address, byte[] buffer, int Size, int BytesWritten = 0);

        [DllImport("kernel32.dll")]
        public static extern int ReadProcessMemory(IntPtr Handle, long Address, byte[] buffer, int Size, int BytesRead = 0);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();

        // 0x48, 0x8B, 0x05, 0x0, 0x0, 0x0, 0x0, 0x45, 0x0, 0x0, 0x0, 0x0, 0x48, 0x8B, 0x48, 0x08, 0x48, 0x85, 0xC9, 0x74, 0x07 }, "xxx????x????xxxxxxxxx"/
        public static long FindPattern(byte[] pattern, string mask)
        {
            Process proc = GTA5Process.GetProcess();
            int moduleSize = proc.MainModule.ModuleMemorySize;

            if (moduleSize == 0)
                throw new Exception($"Size of module {proc.MainModule.ModuleName} is INVALID.");

            byte[] moduleBytes = new byte[moduleSize];
            ReadProcessMemory(GTA5Process.GetHandle(), GameAddress.BaseAddress, moduleBytes, moduleSize);

            for (long i = 0; i < moduleSize; i++)
            {
                bool found = true;

                for (int l = 0; l < mask.Length; l++)
                {
                    found = mask[l] == '?' || moduleBytes[l + i] == pattern[l];

                    if (!found)
                        break;
                }

                if (found)
                {
                    moduleBytes = null;
                    return i;
                }

            }
            return 0;
        }

        public static long GetPointerAddress(long Pointer, int[] Offset = null)
        {
            byte[] Buffer = new byte[8];

            ReadProcessMemory(GTA5Process.GetHandle(), Pointer, Buffer, Buffer.Length);

            if (Offset != null)
            {
                for (int x = 0; x < (Offset.Length - 1); x++)
                {
                    Pointer = BitConverter.ToInt64(Buffer, 0) + Offset[x];
                    ReadProcessMemory(GTA5Process.GetHandle(), Pointer, Buffer, Buffer.Length);
                }

                Pointer = BitConverter.ToInt64(Buffer, 0) + Offset[Offset.Length - 1];
            }

            return Pointer;
        }

        public static long GetPointerAddress_2(long Pointer, int[] Offset = null)
        {
            byte[] Buffer = new byte[8];

            ReadProcessMemory(GTA5Process.GetHandle(), Pointer, Buffer, Buffer.Length);

            if (Offset != null)
            {
                for (int x = 0; x < (Offset.Length - 1); x++)
                {
                    Pointer = BitConverter.ToInt64(Buffer, 0) - Offset[x];
                    ReadProcessMemory(GTA5Process.GetHandle(), Pointer, Buffer, Buffer.Length);
                }

                Pointer = BitConverter.ToInt64(Buffer, 0) - Offset[Offset.Length - 1];
            }

            return Pointer;
        }

        public static byte[] ReadBytes(long Address, int Length)
        {
            byte[] Buffer = new byte[Length];
            ReadProcessMemory(GTA5Process.GetHandle(), Address, Buffer, Length);

            return Buffer;
        }

        public static float ReadFloat(long Address)
        {
            byte[] Buffer = new byte[4]; ;
            ReadProcessMemory(GTA5Process.GetHandle(), Address, Buffer, 4);
            return BitConverter.ToSingle(Buffer, 0);
        }

        public static double ReadDouble(long Address)
        {
            byte[] Buffer = new byte[8];
            ReadProcessMemory(GTA5Process.GetHandle(), Address, Buffer, 8);
            return BitConverter.ToDouble(Buffer, 0);
        }

        public static int ReadInteger(long Address, int Length)
        {
            byte[] Buffer = new byte[Length];
            ReadProcessMemory(GTA5Process.GetHandle(), Address, Buffer, Length);
            return BitConverter.ToInt32(Buffer, 0);
        }

        public static string ReadString(long Address, int size)
        {
            byte[] Buffer = new byte[size]; ;
            ReadProcessMemory(GTA5Process.GetHandle(), Address, Buffer, size);
            return new UTF8Encoding().GetString(Buffer);
        }

        public static long ReadPointer(long Address)
        {
            byte[] Buffer = new byte[8];
            ReadProcessMemory(GTA5Process.GetHandle(), Address, Buffer, Buffer.Length);
            return BitConverter.ToInt64(Buffer, 0);
        }


        /////////////////////////////////

        public static void WriteBytes(long Address, byte[] Bytes)
        {
            WriteProcessMemory(GTA5Process.GetHandle(), Address, Bytes, Bytes.Length);
        }

        public static void WriteFloat(long Address, float Value)
        {
            WriteProcessMemory(GTA5Process.GetHandle(), Address, BitConverter.GetBytes(Value), 4);
        }

        public static void WriteDouble(long Address, double Value)
        {
            WriteProcessMemory(GTA5Process.GetHandle(), Address, BitConverter.GetBytes(Value), 8);
        }
        public static void WriteInteger(long Address, int Value, int size)
        {
            WriteProcessMemory(GTA5Process.GetHandle(), Address, BitConverter.GetBytes(Value), size);
        }
        public static void WriteString(long Address, string String)
        {
            byte[] Buffer = new ASCIIEncoding().GetBytes(String);
            WriteProcessMemory(GTA5Process.GetHandle(), Address, Buffer, Buffer.Length);
        }

        ////////////////////////////


    }
}