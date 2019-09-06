
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using GTA5Memory_UI.GameProcess;
using SharpDX;
using SharpDX.DirectWrite;

namespace GTA5Memory_UI
{
    public class DrawScreen
    {

        public static WindowRenderTarget _renderTarget;
        public static SolidColorBrush _LineBrush;
        public static SolidColorBrush _LineBrush2;
        public static TextFormat _textFormat;
        public static SharpDX.Direct2D1.Factory _Factory;

        public static void Init()
        {
           _Factory = new SharpDX.Direct2D1.Factory();

            var renderTargetProperties = new RenderTargetProperties()
            {
                PixelFormat = new PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Ignore)
            };
            var hwndRenderTargetProperties = new HwndRenderTargetProperties()
            {
                Hwnd = Memory.FindWindow(null, "Steam"),
                PixelSize = new Size2(1920, 1080),
                PresentOptions = PresentOptions.RetainContents,
            };

            //Memory.FindWindow(null, "Steam")

            _textFormat = new TextFormat(new SharpDX.DirectWrite.Factory(), "Segoe UI", 15);
            _renderTarget = new WindowRenderTarget(_Factory, renderTargetProperties, hwndRenderTargetProperties);
            _LineBrush = new SolidColorBrush(_renderTarget, new RawColor4(255, 0, 0, 1f));
            _LineBrush2 = new SolidColorBrush(_renderTarget, new RawColor4(0, 255, 0, 1f));

            /*_renderTarget.BeginDraw();
            _renderTarget.Clear(null);
            _renderTarget.DrawLine(new RawVector2(960, 0), new RawVector2(960, 540), brush, 3);
            _renderTarget.EndDraw();*/
        }

    }
}