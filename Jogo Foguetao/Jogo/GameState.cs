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
    public class GameState
    {
        ChaserGameObject mChaser;
        int mChaserHit, mChaserMissed;

        Vector2 kInitRocketPosition = new Vector2(10, 10);
        GameObject mRocket;
        GameObject mArrow;



        public GameState()
        {
            mChaser = new ChaserGameObject("Chaser", Vector2.Zero, new Vector2(15f, 4.3f), null);
            mChaser.InitialFrontDirection = -Vector2.UnitX;
            mChaser.Speed = 0.2f;
            mChaserHit = 0;
            mChaserMissed = 0;


            mRocket = new GameObject("Rocket", kInitRocketPosition, new Vector2(8, 20));
            mArrow = new GameObject("Arrow", new Vector2(100, 60), new Vector2(20, 8));
            mArrow.InitialFrontDirection = Vector2.UnitX;

        }

        public void UpdateGame()
        {
            if (mChaser.HasValidTarget)
            {
                mChaser.ChaseTarget();

                if (mChaser.HitTarget)
                {
                    mChaserHit++;
                    mChaser.Target = null;
                }



                if (Camera.CollideWithCameraWindow(mChaser) != Camera.CameraWindowCollisionStatus.InsideWindow)
                {
                    mChaserMissed++;
                    mChaser.Target = null;
                }

            }
            if (Input.InputWrapper.Buttons.A == ButtonState.Pressed)
            {
                mChaser.Target = mRocket;
                mChaser.Position = mArrow.Position;
            }

            mRocket.RotateAngleInRadian += MathHelper.ToRadians(Input.InputWrapper.ThumbSticks.Right.X);
            mRocket.Speed -= Input.InputWrapper.ThumbSticks.Left.Y * 0.1f;
            mRocket.VelocityDirection = mRocket.FrontDirection;

            if (Camera.CollideWithCameraWindow(mRocket) != Camera.CameraWindowCollisionStatus.InsideWindow)
            {
                mRocket.Speed = 0f;
                mRocket.Position = kInitRocketPosition;
            }
            mRocket.Update();

            Vector2 toRocket = mRocket.Position - mArrow.Position;

            mArrow.FrontDirection = toRocket;

            mRocket.Position -= Input.InputWrapper.ThumbSticks.Left;

        }
        public void DrawGame()
        {
            mRocket.Draw();
            mArrow.Draw();

            if (mChaser.HasValidTarget)
                mChaser.Draw();


            FontSupport.PrintStatus("Chaser Hit=" + mChaserHit + "Missed=" + mChaserMissed, null);
            FontSupport.PrintStatusAt(mRocket.Position, mRocket.Position.ToString(), Color.White);


        }
    }
}
