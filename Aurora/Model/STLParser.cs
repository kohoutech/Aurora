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
using System.IO;

using Aurora.Math;

namespace Aurora.Model
{
    class STLParser
    {
        String filename;
        bool isAscii;

        public STLParser(String fname)
        {
            filename = fname;
            isAscii = false;
        }

        //no error checking yet
        public AuModel parseFile()
        {
            AuModel result = null;
            //determine if stl file is ascii or binary
            String sig = null;
            using (StreamReader reader = File.OpenText(filename))
            {
                char[] sigchars = new char[5];
                reader.ReadBlock(sigchars, 0, 5);
                sig = new String(sigchars);
            }
            isAscii = "solid".Equals(sig);
            if (isAscii)
            {
                result = parseAsciiSTL();
            }
            else
            {
                result = parseBinarySTL();
            }
            return result;
        }

//- ascii stl ----------------------------------------------------------------

        String[] srctext;
        int linenum;

        public AuModel parseAsciiSTL()
        {
            srctext = File.ReadAllLines(filename);
            AuModel result = new AuModel();
            linenum = 1;
            while (linenum < (srctext.Length - 1))          //-1 for the last "endsolid model" line
            {
                Face facet = readAsciiFacet();
                result.addFacet(facet);
            }
            return result;
        }

        public Face readAsciiFacet()
        {
            //facet normal
            String[] normaldata = srctext[linenum++].Split();
            float normalx = Convert.ToSingle(normaldata[2]);
            float normaly = Convert.ToSingle(normaldata[3]);
            float normalz = Convert.ToSingle(normaldata[4]);
            Vector normal = new Vector(normalx, normaly, normalz);

            linenum++;      //skip "outer loop"

            Point vert1 = readAsciiPoint();
            Point vert2 = readAsciiPoint();
            Point vert3 = readAsciiPoint();
            Face facet = new Face(normal, vert1, vert2, vert3);

            linenum++;      //skip "endloop"
            linenum++;      //skip "endfacet"

            return facet;
        }

        public Point readAsciiPoint()
        {
            String[] vertexdata = srctext[linenum++].Split();
            float x = Convert.ToSingle(vertexdata[1]);
            float y = Convert.ToSingle(vertexdata[2]);
            float z = Convert.ToSingle(vertexdata[3]);
            Point v = new Point(x, y, z);
            return v;
        }

//- binary stl ----------------------------------------------------------------

        Byte[] srcbuf;
        uint srclen;
        uint srcpos;

        public AuModel parseBinarySTL()
        {
            srcbuf = File.ReadAllBytes(filename);
            srclen = (uint)srcbuf.Length;
            srcpos = 0;

            AuModel result = new AuModel();
            seek(80);                                   //skip 80 byte header
            uint facetcount = getFour();                //get num of facets
            for (int i = 0; i < facetcount; i++)
            {
                Face facet = readBinaryFacet();
                result.addFacet(facet);
            }

            return result;
        }

        public Face readBinaryFacet()
        {
            Vector normal = readBinaryVector();
            Point vert1 = readBinaryPoint();
            Point vert2 = readBinaryPoint();
            Point vert3 = readBinaryPoint();
            Face facet = new Face(normal, vert1, vert2, vert3);

            uint attrByteCount = getTwo();              //unused
            return facet;
        }

        public Vector readBinaryVector()
        {
            float x = getFloat();
            float y = getFloat();
            float z = getFloat();
            Vector v = new Vector(x, y, z);
            return v;
        }

        public Point readBinaryPoint()
        {
            float x = getFloat();
            float y = getFloat();
            float z = getFloat();
            Point v = new Point(x, y, z);
            return v;
        }

        public uint getTwo()
        {
            byte b = srcbuf[srcpos++];
            byte a = srcbuf[srcpos++];
            uint result = (uint)(a * 256 + b);
            return result;
        }

        public uint getFour()
        {
            byte d = srcbuf[srcpos++];
            byte c = srcbuf[srcpos++];
            byte b = srcbuf[srcpos++];
            byte a = srcbuf[srcpos++];
            uint result = (uint)(a * 256 + b);
            result = (result * 256 + c);
            result = (result * 256 + d);
            return result;
        }

        public float getFloat()
        {
            byte[] fbytes = new byte[4];
            fbytes[0] = srcbuf[srcpos++];
            fbytes[1] = srcbuf[srcpos++];
            fbytes[2] = srcbuf[srcpos++];
            fbytes[3] = srcbuf[srcpos++];
            float result = BitConverter.ToSingle(fbytes, 0);
            return result;
        }

        public void seek(uint pos)
        {
            srcpos = pos;
        }

    }
}

//Console.WriteLine("there's no sun in the shadow of the Wizard");
