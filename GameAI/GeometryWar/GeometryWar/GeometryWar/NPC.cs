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
            mPosition = new Vector2(Globals.random.Next(300, 2000), Globals.random.Next(300, 1500)); //2300,1700
            mVelocity = new Vector2(Globals.random.Next(-3, 3),Globals.random.Next(-3,3));
            mOrientation = mVelocity;
            mAcceleration = Vector2.Zero;
        }

    }
}
