using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using GTA5Memory_UI.GameProcess;

namespace GTA5Memory_UI
{
    class TXTConvert
    {

        /// <summary>
        /// 坐标Dictionary
        /// </summary>
        public static Dictionary<string, float[]> PositionList;

        public static void Init()
        {
            String filename = Process.GetCurrentProcess().MainModule.FileName.Replace(Process.GetCurrentProcess().MainModule.ModuleName, "") + "PositionList.txt";
            if (!File.Exists(filename))
            {
                StreamWriter sw = File.CreateText(filename);
                sw.Close();
            }

            GTA5Process.PositionFile = filename;

            PositionList = GetAllPositions();
        }

        private static string GetAll()
        {
            StreamReader sr = new StreamReader(GTA5Process.PositionFile);

            String txt = sr.ReadToEnd();

            sr.Close();

            return txt;
        }

        public static bool Exists(string key)
        {
            return PositionList.ContainsKey(key);
        }

        private static string[] GetAllLine()
        {
            return File.ReadAllLines(GTA5Process.PositionFile, Encoding.Default);
        }

        private static Dictionary<string, float[]> GetAllPositions()
        {
            Dictionary<string, float[]> poslist = new Dictionary<string, float[]>();
            string[] a = GetAllLine();

            Array.Sort(a);

            foreach (string var in a)
            {
                string[] data = Regex.Split(var, "#");

                if (!poslist.ContainsKey(data[0]))
                {
                    poslist.Add(data[0], new float[] { float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]) });
                }
            }

            return poslist;
        }

        public static Dictionary<string, float[]> GetPositionList()
        {
            return PositionList;
        }

        public static bool Add(string key, float[] xyz)
        {
            if (PositionList.ContainsKey(key))
            {
                return false;
            }

            PositionList.Add(key, xyz);

            StreamWriter sw = new StreamWriter(GTA5Process.PositionFile, true, Encoding.Default);

            StringBuilder data = new StringBuilder().Append(key).Append("#").Append(xyz[0]).Append("#").Append(xyz[1]).Append("#").Append(xyz[2]);

            sw.WriteLine(data.ToString());

            sw.Close();

            return true;
        }

        public static bool Remove(string key)
        {
            string[] nmsl = GetAllLine();

            for (int i = 0; i < nmsl.Length; i++)
            {
                if (nmsl[i].IndexOf(key, 0, key.Length) != -1)
                {
                    string[] newnmsl = DeleteString(nmsl, nmsl[i]);

                    StreamWriter sw = new StreamWriter(GTA5Process.PositionFile, false, Encoding.Default);

                    sw.Write("");
                    sw.Close();

                    StreamWriter sww = new StreamWriter(GTA5Process.PositionFile, true, Encoding.Default);

                    foreach (string line in newnmsl)
                    {
                        string[] data = Regex.Split(line, "#");

                        StringBuilder dataa = new StringBuilder().Append(data[0]).Append("#").Append(data[1]).Append("#").Append(data[2]).Append("#").Append(data[3]);
                        sww.WriteLine(dataa.ToString());
                    }

                    sww.Close();

                    PositionList.Remove(key);

                    return true; ;
                }

            }

            return false;

        }

        public static float[] GetPosition(string key)
        {
            return PositionList[key];
        }

        private static string[] DeleteString(string[] list, string var)
        {
            List<string> l = new List<string>();
            foreach (string s in list)
            {
                if (s != var)
                {
                    l.Add(s);
                }
            }
            return l.ToArray();
        }

    }
}
