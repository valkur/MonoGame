﻿using System;
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
    static public class Camera
    {
        static private Vector2 sOrigin = Vector2.Zero;
        static private float sWidth = 100f;
        static private float sRatio = -1f;
        static private float sHeight = -1f;

        static public Vector2 CameraWindowLowerLeftPosition { get{ return sOrigin; } }
        static public Vector2 CameraWindowUpperRightPosition { get{ return sOrigin + new Vector2(sWidth, sHeight); } }

        static private float cameraWindowToPixelRatio()
        {
            if (sRatio < 0f)
            {
                sRatio = (float)Game1.sGraphics.PreferredBackBufferWidth / sWidth;
                sHeight = sWidth * (float)Game1.sGraphics.PreferredBackBufferHeight / (float)Game1.sGraphics.PreferredBackBufferWidth;
            }
                
            return sRatio;
        }

        static public void SetCameraWindow(Vector2 origin, float width)
        {
            sOrigin = origin;
            sWidth = width;
            cameraWindowToPixelRatio();
        }

        static public void ComputerPixelPosition(Vector2 cameraPosition, out int x,out int y)
        {
            float ratio = cameraWindowToPixelRatio();

            x = (int)(((cameraPosition.X - sOrigin.X) * ratio) + 0.5f);
            y = (int)(((cameraPosition.Y - sOrigin.Y) * ratio) + 0.5f);

            y = Game1.sGraphics.PreferredBackBufferHeight - y;
        }
        static public Rectangle ComputerPixelRectangle(Vector2 position, Vector2 size)
        {
            float ratio = cameraWindowToPixelRatio();

            int width = (int)((size.X * ratio) + 0.5f);
            int height = (int)((size.Y * ratio) + 0.5f);

            int x, y;
            ComputerPixelPosition(position, out x, out y);

            return new Rectangle(x, y, width, height);

        }

        public enum CameraWindowCollisionStatus
        {
            CollideTop=0,
            CollideBottom=1,
            CollideLeft=2,
            CollideRight=3,
            InsideWindow=4
        };

        static public CameraWindowCollisionStatus CollideWithCameraWindow(TexturedPrimitive prim)
        {
            Vector2 min = CameraWindowLowerLeftPosition;
            Vector2 max = CameraWindowUpperRightPosition;

            if (prim.MaxBound.Y > max.Y)
                return CameraWindowCollisionStatus.CollideTop;
            if (prim.MinBound.X < min.X)
                return CameraWindowCollisionStatus.CollideLeft;
            if (prim.MaxBound.X > max.X)
                return CameraWindowCollisionStatus.CollideRight;
            if (prim.MinBound.Y < min.Y)
                return CameraWindowCollisionStatus.CollideBottom;

            return CameraWindowCollisionStatus.InsideWindow;
        }

        static public Vector2 RandomPosition()
        {
            float rangeX = 0.8f * sWidth;
            float offsetX = 0.1f * sWidth;
            float rangeY = 0.8f * sHeight;
            float offsetY = 0.1f * sWidth;

            float x = (float)(Game1.sRan.NextDouble()) * rangeX + offsetX + sOrigin.X;
            float y = (float)(Game1.sRan.NextDouble()) * rangeY + offsetY + sOrigin.Y;

            return new Vector2(x, y);

        }
    }
}
