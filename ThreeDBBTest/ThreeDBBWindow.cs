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
            canvas.Location = new Point(0, 50);
            canvas.Size = new Size(this.Width, this.Height - 50);
            this.Controls.Add(canvas);

            canvas.InitCanvas();
        }

        protected override void OnResize(EventArgs e)
        {
            if (canvas != null)
            {
                canvas.Size = new Size(this.Width, this.Height - 50);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            canvas.CloseIt();
        }
    }
}
