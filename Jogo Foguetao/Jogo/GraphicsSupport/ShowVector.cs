using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Design;

namespace Jogo
{
    public class ShowVector
    {
        protected static Texture2D sImage = null;
        private static float kLenToWidthRatio = 0.2f;

        static private void LoadImage()
        {
            if (null == sImage) ShowVector.sImage = Game1.sContent.Load<Texture2D>("Arrow");
        }

        static public void DrawPointVector(Vector2 from, Vector2 dir)
        {
            LoadImage();
            float length = dir.Length();
            float theta = 0f;
            if (length > 0.001f)
            {
                dir /= length;
                theta = (float)Math.Acos((double)dir.X);
                if (dir.X < 0.0f)
                {
                    if (dir.Y > 0.0f) theta = -theta;
                }
                else
                {
                    if (dir.Y > 0.0f) theta = -theta;
                }
            }
            Vector2 size = new Vector2(length, kLenToWidthRatio * length);
            Rectangle destRect = Camera.ComputerPixelRectangle(from, size);
            Vector2 org = new Vector2(0f, ShowVector.sImage.Height / 2f);
            Game1.sSpriteBatch.Draw(ShowVector.sImage, destRect, null, Color.White, theta, org, SpriteEffects.None, 0f);

            String msg;
            msg = "Direction=" + dir + "\nSize=" + length;
            FontSupport.PrintStatusAt(from + new Vector2(2, 5), msg, Color.Black);
        }
        static public void DrawFromTo(Vector2 from, Vector2 to)
        {
            DrawPointVector(from, to - from);
        }
        static public Vector2 RotateVectorByAngle(Vector2 v,float angleInRadian)
        {
            float sinTheta = (float)(Math.Sin((double)angleInRadian));
            float cosTheta = (float)(Math.Cos((double)angleInRadian));
            float x, y;
            x = cosTheta * v.X + sinTheta * v.Y;
            y = -sinTheta * v.X + cosTheta * v.Y;
            return new Vector2(x, y);
        }
    }

    
}
