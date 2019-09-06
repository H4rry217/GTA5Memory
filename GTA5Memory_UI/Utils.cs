using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTA5Memory_UI
{
    class Utils
    {

        public static float Distance(float[] p1, float[] p2) {
            double distance = Math.Sqrt(
                Math.Pow(p1[0] - p2[0], 2) +
                Math.Pow(p1[2] - p2[2], 2) +
                Math.Pow(p1[1] - p2[1], 2)
                );

            return float.Parse(distance.ToString());
        }

        /// <summary>
        /// 导入模拟键盘的方法
        /// </summary>
        /// <param name="bVk" >按键的虚拟键值</param>
        /// <param name= "bScan" >扫描码，一般不用设置，用0代替就行</param>
        /// <param name= "dwFlags" >选项标志：0：表示按下，2：表示松开</param>
        /// <param name= "dwExtraInfo">一般设置为0</param>
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);


    }
}
