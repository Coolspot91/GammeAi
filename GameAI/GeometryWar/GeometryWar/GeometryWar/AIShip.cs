
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace GeometryWar
{
    class AIShip : NPC
    {
        float myran = 0;
        float orient = 0;
        Vector2 maxRot = new Vector2(30, 30);
        float count = 0;
        float timePrediction = 5;        

        
        //seek with arrive
        public Vector2 getPosEnemy
        {
            get { return mPosition; }

        }
        public void seek(Player target)
        {
            //With Arrive          
            mVelocity = target.mTranslation - mTranslation;
            // if near player slow down
            if (mVelocity.Length() < 200)
            {
                mVelocity = mVelocity / 4; // 4 = timeToTarget
            }
            if (mVelocity.Length() > maxSpeed)
            {
                mVelocity = Vector2.Normalize(mVelocity);
                mVelocity = mVelocity * maxSpeed;
                mOrientation = mVelocity;
            }

        }

        public void seek2(Vector2 playerPosition)
        {
            // accelerate towards player
            mAcceleration = playerPosition - mTranslation;

            if (!(mAcceleration.X == 0 && mAcceleration.Y == 0))
            {
                 mAcceleration = Vector2.Normalize(mAcceleration);
                 mAcceleration = mAcceleration * maxAccel;
                 // apply a drag force(air res)
                 mVelocity -= mVelocity * 0.01f;
            }

            if (mVelocity.Length() > maxSpeed)
            {
                mVelocity.Normalize();
                mVelocity = mVelocity * maxSpeed;
            }


        }
        public void flee(Vector2 playerPos)
        {
            mVelocity = mTranslation - playerPos;
            if (!(mVelocity.X == 0 && mVelocity.Y == 0))
            {
                mVelocity.Normalize();
            }
            mVelocity = mVelocity * maxSpeed;
            mOrientation = Vector2.Normalize(mVelocity);
        }


        public void wandering(Vector2 position)
        {
            if (count > 50)
            {

                myran = Globals.random.Next(-10, 10);
                count = 0;
            }
            count++;

            flee(position);
            mOrientation = mVelocity;
            mOrientation = mOrientation + maxRot * myran;
            orient = VectorToAngle(mOrientation);
            mVelocity = new Vector2((float)Math.Sin(orient), -(float)Math.Cos(orient)) * maxSpeed;

        }


        public void AvoidCollison(Vector2 collsionPosTarget, Vector2 playerPos, float health)
        {
            distanceFromTarget = Vector2.Distance(mTranslation, collsionPosTarget);

            // if two ships are near each other, flee each other
            if (distanceFromTarget < 80)
            {
                flee(collsionPosTarget);
            }
            // if healthy seek player
            else if (health > 10)
            {
                seek2(playerPos);
            }
            // if weak flee
            else
            {
                flee(playerPos);
            }
        }

    }
}
