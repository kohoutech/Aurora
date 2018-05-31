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

namespace Aurora.Model
{
    public class AuModel
    {
        public List<Facet> facets;
        public List<Facet> views;
        float centerX, centerY;
        float zoom;

        public static AuModel loadModel(String filename)
        {
            STLParser parser = new STLParser(filename);
            AuModel result = parser.parseFile();
            return result;
        }

        public AuModel()
        {
            facets = new List<Facet>();
            views = new List<Facet>();
            centerX = 0.0f;
            centerY = 0.0f;
            zoom = 1.0f;
        }

        public void addFacet(Facet facet)
        {
            facets.Add(facet);
        }

        public void setCenter(float _centerX, float _centerY)
        {
            centerX = _centerX;
            centerY = _centerY;
            updateView();
        }

        public void setZoom(float _zoom)
        {
            zoom = _zoom;
            updateView();
        }

        public void updateView()
        {
            List<Facet> newviews = new List<Facet>();
            foreach (Facet face in facets)
            {
                Facet view = new Facet(face);
                view.scale(zoom, zoom, 1.0f);
                view.translate(centerX, centerY, 0.0f);
                newviews.Add(view);
            }
            views = newviews;            
        }
    }

}
