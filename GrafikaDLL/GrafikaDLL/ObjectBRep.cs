using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaDLL
{
    public class ObjectBRep
    {
        public BRep model;
        public Matrix4 transform;
        public Color color;

        public ObjectBRep()
        {
            model = new BRep();
            transform = new Matrix4();
            transform.LoadIdentity();
            color = Color.Black;
        }
    }
}
