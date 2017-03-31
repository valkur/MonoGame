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
    public class BasketBall : TexturedPrimitive
    {
        private const float kIncreseRate = 1.001f;
        private Vector2 kInitSize = new Vector2(5, 5);
        private const float kFinalSize = 15f;

        public BasketBall():base("BasketBall")
        {
            mPosition = Camera.RandomPosition();
            mSize = kInitSize;
        }

        public bool UpdateAndExplode()
        {
            mSize *= kIncreseRate;
            return mSize.X > kFinalSize;
        }
    }
}
