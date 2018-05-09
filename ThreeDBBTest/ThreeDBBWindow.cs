//testing running a separate window for rendering 3D images

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ThreeDBBTest
{
    public partial class ThreeDBBWindow : Form
    {
        ThreeDCanvas canvas;

        public ThreeDBBWindow()
        {
            InitializeComponent();

            canvas = new ThreeDCanvas();
            canvas.Location = new Point(0, 0);
            canvas.Size = new Size(this.Width, this.Height);
            this.Controls.Add(canvas);

            canvas.InitCanvas();
        }
    }
}
