/* ----------------------------------------------------------------------------
Aurora : a 3-D modeler
Copyright (C) 2007-2018  George E Greaney

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
----------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Aurora.ThreeDBB;
using Aurora.Model;

namespace Aurora
{
    public partial class AuWindow : Form
    {
        BBCanvas canvas;
        AuModel curModel;
        String modelFilename;
        TrackBar zoomTrackBar;

        public AuWindow()
        {
            InitializeComponent();

            canvas = new BBCanvas();
            canvas.Location = new Point(0, auToolStrip.Bottom);
            canvas.Size = new Size(this.Width, auStatusStrip.Top - auToolStrip.Bottom);
            this.Controls.Add(canvas);

            curModel = null;
            canvas.Invalidate();
        }

        //private void AuWindow_FormClosing(object sender, FormClosingEventArgs e)
        //{            
        //}

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (canvas != null)
            {
                canvas.Location = new Point(0, auToolStrip.Bottom);
                canvas.Size = new Size(this.Width, auStatusStrip.Top - auToolStrip.Bottom);
            }
        }

        public void openModel()
        {
            OpenModelDialog.InitialDirectory = Application.StartupPath;
            OpenModelDialog.DefaultExt = "*.stl";
            OpenModelDialog.Filter = "stl files|*.stl|All files|*.*";
            OpenModelDialog.ShowDialog();
            String filename = OpenModelDialog.FileName;
            //String filename = "test.stl";
            if (filename.Length > 0)
            {
                //modelFilename = filename;
                //curModel = AuModel.loadModel(filename);
                //canvas.setModel(curModel);
                //zoomTrackBar.Value = 10;
                //this.Text = "Aurora [" + modelFilename + "]";
            }
        }

        private void newFileMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openFileMenuItem_Click(object sender, EventArgs e)
        {
            openModel();
        }

        private void saveFileMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveAsFileMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitFileMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

//- help menu ----------------------------------------------------------------

        private void aboutHelpMenuItem_Click(object sender, EventArgs e)
        {
            String msg = "Aurora\nversion 0.2.0\n" +
                        "\xA9 Servo Software 2007-2018\n" + 
                        "http://servo.kohoutech.com";
            MessageBox.Show(msg, "About");

        }

//- toolbar -------------------------------------------------------------------

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openModel();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {

        }

    }
}
