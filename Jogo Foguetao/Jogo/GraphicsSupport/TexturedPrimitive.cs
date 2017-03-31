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

    public class TexturedPrimitive
    {
        protected Texture2D mImage;
        protected Vector2 mPosition;
        protected Vector2 mSize;
        protected float mRotateAngle;
        protected String mLabelString;
        protected Color mLabelColor = Color.Black;

        protected void InitPrimitive(String imageName, Vector2 position,Vector2 size, String label=null)
        {
            mImage = Game1.sContent.Load<Texture2D>(imageName);
            mPosition = position;
            mSize = size;
            mRotateAngle = 0f;
            mLabelString = label;
        }
        

        public TexturedPrimitive(String imageName, Vector2 position, Vector2 size, String label=null)
        {
            InitPrimitive(imageName, position, size, label);
        }

        public TexturedPrimitive(String imageName)
        {
            InitPrimitive(imageName, Vector2.Zero, new Vector2(1f, 1f));
        }

        public Vector2 Position { get { return mPosition; } set { mPosition = value; } }
        public float PositionX { get { return mPosition.X; } set { mPosition.X = value; } }
        public float PositionY { get { return mPosition.Y; } set { mPosition.Y = value; } }
        public Vector2 Size { get { return mSize; } set { mSize = value; } }
        public float Width { get { return mSize.X; } set { mSize.X = value; } }
        public float Height { get { return mSize.Y; } set { mSize.Y = value; } }
        public Vector2 MinBound { get { return mPosition - (0.5f * mSize); } }
        public Vector2 MaxBound { get { return mPosition + (0.5f * mSize); } }
        public float RotateAngleInRadian { get { return mRotateAngle; } set { mRotateAngle = value; } }
        public String Label { get { return mLabelString; } set { mLabelString = value; } }
        public Color LabelColor { get { return mLabelColor; } set { mLabelColor = value; } }

        public void Update(Vector2 deltaTranslate, Vector2 deltaScale, float deltaAngleInRadian)
        {
            mPosition += deltaTranslate;
            mSize += deltaScale;
            mRotateAngle += deltaAngleInRadian;
            
        }
        public void Update(Vector2 deltaTranslate, Vector2 deltaScale)
        {
            mPosition += deltaTranslate;
            mSize += deltaScale;

        }

        public bool PrimitiveTouches(TexturedPrimitive otherPrim)
        {
            Vector2 v = mPosition - otherPrim.Position;
            float dist = v.Length();
            return (dist < ((mSize.X / 2f) + (otherPrim.mSize.X / 2f)));
        }

        virtual public void Draw()
        {
            Rectangle destRect =Camera.ComputerPixelRectangle(Position, Size);
            Vector2 org = new Vector2(mImage.Width / 2, mImage.Height / 2);
            Game1.sSpriteBatch.Draw(mImage, destRect, null, Color.White, mRotateAngle, org, SpriteEffects.None, 0f);
            if (null != Label)
                FontSupport.PrintStatusAt(Position, Label, LabelColor);
        }
    }
}
