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
    public class ChaserGameObject : GameObject
    {
        protected TexturedPrimitive mTarget;
        protected bool mHitTarget;
        protected float mHomeInRate;

        public float HomeInRate { get { return mHomeInRate; } set { mHomeInRate = value; } }
        public bool HitTarget { get { return mHitTarget; } }
        public bool HasValidTarget { get { return null != mTarget; } }
        public TexturedPrimitive Target
        {
            get { return mTarget; }
            set
            {
                mTarget = value;
                mHitTarget = false;
                if(null!=mTarget)
                {
                    FrontDirection = mTarget.Position - Position;
                    VelocityDirection = FrontDirection;
                }
            }
        }

        public ChaserGameObject(String imageName, Vector2 position,Vector2 size, TexturedPrimitive target) : base(imageName,position,size,null)
        {
            Target = target;
            mHomeInRate = 0.05f;
            mHitTarget = false;
            mSpeed = 0.1f;
        }

        public void ChaseTarget()
        {
            if (null == mTarget)
                return;
            base.Update();

            mHitTarget = PrimitiveTouches(mTarget);

            if(!mHitTarget)
            {
                Vector2 targetDir = mTarget.Position - Position;
                float distToTargetSq = targetDir.LengthSquared();
                targetDir /= (float)Math.Sqrt(distToTargetSq);
                float cosTheta = Vector2.Dot(FrontDirection, targetDir);
                float theta = (float)Math.Acos(cosTheta);

                if (theta > float.Epsilon)
                {
                    Vector3 fIn3D = new Vector3(FrontDirection, 0f);
                    Vector3 tIn3D = new Vector3(targetDir, 0f);
                    Vector3 sign = Vector3.Cross(tIn3D, fIn3D);

                    RotateAngleInRadian += Math.Sign(sign.Z) * theta * mHomeInRate;
                    VelocityDirection = FrontDirection;
                }
            }

        }
    }
}
