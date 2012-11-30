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
    class Player : Entity
    {

        public Vector2 getPos
        {
            get { return mPosition; }

        }

        public void Update(GameTime theGameTime)
        {
            mVelocity.X *= 0.9f;
            mVelocity.Y *= 0.9f;
            mAcceleration = Vector2.Zero;
                KeyboardState aCurrentKeyboardState = Keyboard.GetState();
                if (aCurrentKeyboardState.IsKeyDown(Keys.Left) == true)
                {
                    mAcceleration.X = -1f;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Right) == true)
                {
                    mAcceleration.X = 1f;
                }
                if (aCurrentKeyboardState.IsKeyDown(Keys.Up) == true)
                {
                    mAcceleration.Y = -1f;
                }
                else if (aCurrentKeyboardState.IsKeyDown(Keys.Down) == true)
                {
                    mAcceleration.Y = 1f;
                }
                mVelocity += mAcceleration;
                mOrientation = Vector2.Normalize(mVelocity);
                mPosition += mVelocity;
                //do wrap-around if necessary
                if (mPosition.X > Globals.Universe.X) mPosition.X -= Globals.Universe.X;
                else if (mPosition.X < 0) mPosition.X += Globals.Universe.X;
                if (mPosition.Y > Globals.Universe.Y) mPosition.Y -= Globals.Universe.Y;
                else if (mPosition.Y < 0) mPosition.Y += Globals.Universe.Y;

        }
    }
}
