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
    public class MyGame
    {
        TexturedPrimitive mHero;
        Vector2 kHeroSize = new Vector2(25, 25);
        Vector2 kHeroPosition = new Vector2(10,10);

        List<BasketBall> mBBallList;
        TimeSpan mCreationTimeStamp;
        int mTotalBBallCreated = 0;
        const int kBallMSecInterval = 500;

        int mScore = 0;
        int mBBallMissed = 0;
        int mBBallHit = 0;
        const int kBBallTouchScore = 1;
        const int kBBallMissedScore = -2;
        const int kWinScore = 10;
        const int kLossScore = -10;
        TexturedPrimitive mFinal = null;

        public MyGame()
        {
            mHero = new TexturedPrimitive("Me", kHeroPosition, kHeroSize);

            mCreationTimeStamp = new TimeSpan(0);
            mBBallList = new List<BasketBall>();
        }

        public void UpdateGame(GameTime gameTime)
        {
            if (null != mFinal)
                return;

            mHero.Update(Input.InputWrapper.ThumbSticks.Right, new Vector2(0.0f));

            for (int b = mBBallList.Count - 1; b >= 0; b--)
            {
                if(mBBallList[b].UpdateAndExplode())
                {
                    mBBallList.RemoveAt(b);
                    mBBallMissed++;
                    mScore += kBBallMissedScore;
                }
            }
            for (int b = mBBallList.Count - 1; b >= 0; b--)
            {
                if (mHero.PrimitiveTouches(mBBallList[b]))
                {
                    mBBallList.RemoveAt(b);
                    mBBallHit++;
                    mScore += kBBallTouchScore;
                }
            }
            TimeSpan timePassed = gameTime.TotalGameTime;
            timePassed = timePassed.Subtract(mCreationTimeStamp);
            if(timePassed.TotalMilliseconds>kBallMSecInterval)
            {
                mCreationTimeStamp = gameTime.TotalGameTime;
                BasketBall b = new BasketBall();
                mTotalBBallCreated++;
                mBBallList.Add(b);
            }
            if (mScore > kWinScore)
                mFinal = new TexturedPrimitive("winner", new Vector2(75, 50), new Vector2(100, 65));
            else if (mScore < kLossScore)
                mFinal = new TexturedPrimitive("loser", new Vector2(75, 50), new Vector2(100, 65));

        }

        public void DrawGame()
        {
            mHero.Draw();
            foreach (BasketBall b in mBBallList)
                b.Draw();
            if (null != mFinal)
                mFinal.Draw();
            FontSupport.PrintStatus("Status: " + "Score:" + mScore + "Basketball:Generated(" + mTotalBBallCreated + ") Collected(" + mBBallHit + ") Missed (" + mBBallMissed + ")", null);
        }
    }
}
