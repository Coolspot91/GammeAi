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
    class NPC : Entity
    {

        public NPC()
        {
            mPosition = new Vector2(Globals.random.Next(-300, 300), Globals.random.Next(-300, 300));
            mVelocity = new Vector2(Globals.random.Next(-03, 3),Globals.random.Next(-3,3));
            mOrientation = mVelocity;
            mAcceleration = Vector2.Zero;
        }


        //public void seek(Player target)
        //{
        //    mVelocity = target.mPosition - mPosition;
        //    mVelocity.Normalize();
        //    //mVelocity = mVelocity * maxSpeed;
        //    mOrientation = Vector2.Normalize(mVelocity);
        //    //mOrientation = mVelocity;

        //}
    }
}
