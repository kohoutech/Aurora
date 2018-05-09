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
        public static extern void RunTestWindow(IntPtr module);


        public ThreeDCanvas()
        {
            BackColor = Color.LightBlue;
        }

        public void InitCanvas()
        {
            IntPtr module = Process.GetCurrentProcess().Handle;
            RunTestWindow(module);
        }
    }
}
