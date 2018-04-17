﻿/* ----------------------------------------------------------------------------
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using Aurora;
using Aurora.Model;
using Aurora.Math;

namespace Aurora.UI
{
    class AuCanvas : Control
    {
        AuModel model;
        float centerX, centerY;
        float zoom;

        public AuCanvas()
        {
            BackColor = Color.PaleGreen;
            DoubleBuffered = true;
            centerX = this.Width / 2;
            centerY = this.Height / 2;
            model = null;
        }


        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    //centerX = this.Width / 2;
        //    //centerY = this.Height / 2;
        //    //if (model != null)
        //    //{
        //    //    model.setCenter(centerX, centerY);
        //    //}
        //    //Invalidate();
        //}

        public void setModel(AuModel _model)
        {
            model = _model;
            model.setCenter(centerX, centerY);
            Invalidate();
        }

        public void setZoom(float zoom)
        {
            model.setZoom(zoom);
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //does nothing, just overridden to prevent app from painting background & cause flicker
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Graphics g = e.Graphics;
            //g.SmoothingMode = SmoothingMode.AntiAlias;           

            //if (model != null)
            //{
            //    foreach (Face view in model.views)
            //    {
            //        g.DrawLine(Pens.Black, view.vert1.x, view.vert1.y, view.vert2.x, view.vert2.y);
            //        g.DrawLine(Pens.Black, view.vert2.x, view.vert2.y, view.vert3.x, view.vert3.y);
            //        g.DrawLine(Pens.Black, view.vert3.x, view.vert3.y, view.vert1.x, view.vert1.y);
            //    }
            //}
        }
    }
}

//Console.WriteLine("there's no sun in the shadow of the Wizard");
