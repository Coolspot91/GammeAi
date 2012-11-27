using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace GeometryWar
{
    class Bullet : Entity
    {
        public float TimeToLive = 0;
        public bool alive = false;

        public void Init(Player player)
        {
            this.mPosition = player.getPos;
            this.mVelocity = player.mVelocity;
            this.maxSpeed = 1;
        }
        public Vector2 getPos
        {
            get { return mPosition; }
        }
        public void Update(GameTime theGameTime)
        {
            mPosition += mVelocity;
            mVelocity = Vector2.Normalize(mVelocity);
            mVelocity = mVelocity * maxSpeed;

            TimeToLive++;
        }
    }
}
