/* ----------------------------------------------------------------------------
Aurora : a 3-D modeler
Copyright (C) 2007-2017  George E Greaney

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

using Aurora.UI;

namespace Aurora
{
    public partial class AuWindow : Form
    {
        AuCanvas canvas;
        String modelFilename;

        public AuWindow()
        {
            InitializeComponent();

            canvas = new AuCanvas();
            canvas.Dock = DockStyle.Fill;
            this.Controls.Add(canvas);
        }

        public void openModel()
        {
            //OpenModelDialog.InitialDirectory = Application.StartupPath;
            //OpenModelDialog.DefaultExt = "*.stl";
            //OpenModelDialog.Filter = "stl files|*.stl|All files|*.*";
            //OpenModelDialog.ShowDialog();
            //String filename = OpenModelDialog.FileName;
            String filename = "test.stl";
            if (filename.Length > 0)
            {
                modelFilename = filename;
                canvas.loadModel(modelFilename);
                this.Text = "Aurora [" + modelFilename + "]";
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
            String msg = "Aurora\nversion 0.1.0\n" +
                        "\xA9 Servo Software 2007-2017\n" + 
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
