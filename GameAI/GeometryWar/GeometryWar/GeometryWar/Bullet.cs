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
        private Entity creator;

        public void Init(Player player)
        {
            this.mPosition = player.getPos;
            this.mVelocity = player.mVelocity;
            this.mOrientation = player.mOrientation;
            this.maxSpeed = 14;
        }
        public void Init(AIShip enemy)
        {
            this.mPosition = enemy.getPosEnemy;
            this.mVelocity = new Vector2(enemy.mVelocity.X*0.1f, enemy.mVelocity.Y*0.1f);
            this.maxSpeed = 10;
        }
        public Vector2 getPos
        {
            get { return mPosition; }
        }
        public void Update(GameTime theGameTime)
        {
            mPosition += mVelocity;
            if (mVelocity.X != 0 && mVelocity.Y != 0)
            {
                mVelocity = Vector2.Normalize(mVelocity);
            }
            mVelocity = mVelocity * maxSpeed;
        }
        public void resetBullet()
        {
            TimeToLive = 0;
            alive = false;
        }

        public Entity getCreator()
        {
            return creator;
        }
    }
}
