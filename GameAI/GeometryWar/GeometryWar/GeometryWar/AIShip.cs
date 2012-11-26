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
        Vector2 maxRot = new Vector2(40, 40);
        float count = 0;
        float timePrediction = 5;        

        //seek with arrive
        public void seek(Player target)
        {
            //With Arrive          
            mVelocity = target.mTranslation - mTranslation;
            if (mVelocity.Length() < 200)
            {
                mVelocity = mVelocity / 4; //timeToTarget
            }
            if (mVelocity.Length() > maxSpeed)
            {
                mVelocity = Vector2.Normalize(mVelocity);
                mVelocity = mVelocity * maxSpeed;
                mOrientation = mVelocity;
            }

        }

        public void seek2(Player target)
        {
            mAcceleration = target.mTranslation - mTranslation;
            //mVelocity += mAcceleration;

            if (!(mAcceleration.X == 0 && mAcceleration.Y == 0))
            {
                 mAcceleration = Vector2.Normalize(mAcceleration);
                 mAcceleration = mAcceleration * maxAccel;
                 // apply a drag force(air res)
                 mVelocity -= mVelocity * 0.01f;

                 //mOrientation = mVelocity;
            }
                 //mOrientation = mVelocity;

            //if (mAcceleration.Length() > maxAccel)
            //{
            //    mAcceleration = Globals.AngleToVector(maxAccel);
            //    // mVelocity = Globals.AngleToVector(maxSpeed);
            //}

            //float mVel = Globals.VectorToAngle(mVelocity);

            //if (mVel > maxSpeed)
            //{
            //    mVelocity = Globals.AngleToVector(maxSpeed);
            //}
            //if (Globals.VectorToAngle(mAcceleration) > maxAccel && mAcceleration.Length() > maxAccel)
            //{
            //   mAcceleration = Globals.AngleToVector(maxAccel);
            //    //mVelocity = Globals.AngleToVector(maxSpeed);
            //}

            if (mVelocity.Length() > maxSpeed)
            {
                mVelocity.Normalize();
                mVelocity = mVelocity * maxSpeed;
            }


        }
        public void flee(Player target)
        {
            mVelocity = mTranslation - target.mTranslation;
            if (!(mVelocity.X == 0 && mVelocity.Y == 0))
            {
                mVelocity.Normalize();
            }
            mVelocity = mVelocity * maxSpeed;
            mOrientation = Vector2.Normalize(mVelocity);
        }


        public void wandering(Player target)
        {
            if (count > 50)
            {

                myran = Globals.random.Next(-1, 1);
                count = 0;
            }
            count++;

            ////mVelocity = mTranslation - target.mTranslation;
            //mVelocity = target.mTranslation - mTranslation;

            //if (mVelocity.Length() > maxSpeed)
            //{
            //    mVelocity = Vector2.Normalize(mVelocity);
            //}
            //if (mVelocity.Length() < 200)
            //{
            //    mVelocity = mVelocity / 5; //timeToTarget                       
            //}
            //mVelocity = mVelocity * maxSpeed;

            seek2(target);
            mOrientation = mVelocity;
            mOrientation = mOrientation + maxRot * myran;
            orient = VectorToAngle(mOrientation);
            mVelocity = new Vector2((float)Math.Sin(orient), -(float)Math.Cos(orient)) * maxSpeed;

        }


        public void AvoidingObsSeek(Player target,AIShip AItarget)
        {
            // Check ditance of enemy ships
            distanceFromTarget = Vector2.Distance(AItarget.mPosition, mPosition);

            if (distanceFromTarget < 60)
            {
                //collided = true;
                flee(target);
            }
            else 
            {
                seek2(target);
            }

            //if (collided == false)
            //{
            //    mVelocity = target.mTranslation - mTranslation;

            //    if (mVelocity.Length() > maxSpeed)
            //    {
            //        mVelocity = Vector2.Normalize(mVelocity);
            //        mVelocity = mVelocity * maxSpeed;
            //        mOrientation = mVelocity;
            //    }
            //}
            //else if (collided == true)
            //{
            //    flee(target);
            //}
        }







        //public void seek(Player target)
        //{
        //    distanceFromTarget = Vector2.Distance(target.getPos, mPosition);
        //    //mVelocity = target.mTranslation - mTranslation;
        //    //if (!(mVelocity.X == 0 && mVelocity.Y == 0))
        //    //{
        //    //    mVelocity.Normalize();
        //    //}
        //    //mVelocity = mVelocity * maxSpeed;
        //    //mOrientation = Vector2.Normalize(mVelocity);
        //    //mOrientation = mVelocity;


        //    mVelocity = target.mTranslation - mTranslation;
        //    if (mVelocity.Length() < 200)
        //    {
        //        mVelocity = mVelocity / 5; //timeToTarget
        //        if (mVelocity.Length() > maxSpeed)
        //        {
        //            mVelocity = Vector2.Normalize(mVelocity);
        //            mVelocity = mVelocity * maxSpeed;
        //            mOrientation = mVelocity;

        //            //mOrientation = mVelocity;
        //            //mOrientation = mOrientation + new Vector2(2, 2) * Globals.random.Next(-1, 1);
        //            //float orient = VectorToAngle(mOrientation);
        //            //mVelocity = new Vector2((float)Math.Cos(orient), (float)Math.Sin(orient));
        //            //return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        //        }
        //    }

        //}


        //public void pursue(Player target)
        //{
        //    mOrientation = target.mVelocity - mTranslation;
        //    float  distance = mOrientation.Length();
        //    float speed = mVelocity.Length();
        //    if (speed <= distance / 20)
        //    {
        //        timePrediction = 20;
        //    }
        //    else
        //        timePrediction = distance / speed;
        //    target.nwPos = target.mTranslation + target.mVelocity * timePrediction;
        //    seek(target);

        //}
    }
}
