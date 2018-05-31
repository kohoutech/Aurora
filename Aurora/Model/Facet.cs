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

namespace Aurora.Model
{
    public class Facet
    {
        Vector normal;
        public Point3 vert1, vert2, vert3;

        public Facet(Vector norm, Point3 v1, Point3 v2, Point3 v3)
        {
            normal = norm;
            vert1 = v1;
            vert2 = v2;
            vert3 = v3;
        }

        //copy cons
        public Facet (Facet that)
        {
            normal = new Vector(that.normal);
            vert1 = new Point3(that.vert1);
            vert2 = new Point3(that.vert2);
            vert3 = new Point3(that.vert3);
        }

        public void translate(float x, float y, float z)
        {
            vert1.translate(x, y, z);
            vert2.translate(x, y, z);
            vert3.translate(x, y, z);
        }

        public void scale(float x, float y, float z)
        {
            vert1.scale(x, y, z);
            vert2.scale(x, y, z);
            vert3.scale(x, y, z);
        }

    }
}
