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
using System.Linq;
using System.Text;

namespace Aurora.Math
{
    public class Point
    {
        public float x, y, z;

        public Point(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public Point(Point that)
        {
            x = that.x;
            y = that.y;
            z = that.z;
        }

        public void translate(float ofsX, float ofsY, float ofsZ) 
        {
            x += ofsX;
            y += ofsY;
            z += ofsZ;
        }

        public void scale(float scaleX, float scaleY, float scaleZ)
        {
            x *= scaleX;
            y *= scaleY;
            z *= scaleZ;
        }

    }
}
