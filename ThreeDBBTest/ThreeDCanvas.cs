//testing running a separate window for rendering 3D images

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ThreeDBBTest
{
    class ThreeDCanvas : Control
    {
        [DllImport("Xavier.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RunTestWindow(IntPtr module, IntPtr winhdl);

        [DllImport("Xavier.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CloseTestWindow();

        [DllImport("Xavier.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResizeTestWindow(int width, int height);

        public ThreeDCanvas()
        {
            BackColor = Color.LightBlue;
        }

        public void InitCanvas()
        {
            IntPtr module = Process.GetCurrentProcess().Handle;
            IntPtr winhdl = Handle;
            RunTestWindow(module, winhdl);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeTestWindow(this.Width, this.Height);
        }

        internal void CloseIt()
        {
            CloseTestWindow();
        }
    }
}
