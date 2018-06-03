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
using System.Linq;
using System.Text;

using Aurora.ThreeDBB;

namespace Aurora.UI
{
    class Stage
    {
        BBCanvas canvas;

        public void drawFloor()
        {
            float grid;
            float grid_scale = 1.0f;
            int gridlines = 8;
            grid = gridlines * grid_scale;


            for (int i = -gridlines; i < gridlines; i++)
            {
                float line = i * grid_scale;


                BBCanvas.drawArrays(1, 0, 4);
            }

        }
    }
}
