using Microsoft.ProjectOxford.Common;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFormApplication
{
    public static class Extensions
    {

        public static Rectangle ToRectangle(this FaceRectangle faceRectangle)
        {
           return new Rectangle() { Top = faceRectangle.Top, Height = faceRectangle.Height, Left = faceRectangle.Left, Width = faceRectangle.Width };
        }
    }
}
