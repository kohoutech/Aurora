﻿/* ----------------------------------------------------------------------------
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

using Aurora.Math;

namespace Aurora.Model
{
    public class AuModel
    {
        List<Face> facets;

        public static AuModel loadModel(String filename)
        {
            STLParser parser = new STLParser(filename);
            AuModel result = parser.parseFile();
            return result;
        }

        public AuModel()
        {
            facets = new List<Face>();
        }

        public void addFacet(Face facet)
        {
            facets.Add(facet);
        }
    }

    public class Face
    {
        Vector normal, vert1, vert2, vert3;

        public Face(Vector norm, Vector v1, Vector v2, Vector v3)
        {
            normal = norm;
            vert1 = v1;
            vert2 = v2;
            vert3 = v3;
        }
    }

}
