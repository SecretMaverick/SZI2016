using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{

        class Thing
        {
            public String shape, color, weight, size;

            public Thing()
            {
                this.shape = "";
                this.color = "";
                this.weight = "";
                this.size = "";
            }

            public Thing(String sh, String c, String w, String si)
            {
                this.shape = sh;
                this.color = c;
                this.weight = w;
                this.size = si;
            }
            public void SetShape(String tmpshape)
            {
                this.shape = tmpshape;
            }
            public void SetColor(String tmpcolor)
            {
                this.color = tmpcolor;
            }
            public void SetWeight(String tmpweight)
            {
                this.weight = tmpweight;
            }
            public void SetSize(String tmpsize)
            {
                this.size = tmpsize;
            }
            public String GetShape()
            {
                return this.shape;
            }
            public String GetColor()
            {
                return this.color;
            }
            public String GetWeight()
            {
                return this.weight;
            }
            public String GetSize()
            {
                return this.size;
            }
        


    }
}
