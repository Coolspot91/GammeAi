
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
    class Entity
    {
        //The texture object used when drawing the sprite
        private Texture2D mSpriteTexture;
        //The asset name for the Sprite's Texture
        public string mAssetName;
        //Position of Entity
        public Vector2 mPosition;// { get; set; }
        //Velocity of Entity
        public Vector2 mVelocity; //{ get; set; }
        //Orientation of Entity
        public Vector2 mOrientation; // { get; set; }
        //Acceleration of Entity
        public Vector2 mAcceleration;// { get; set; }
        //translated position!
        public Vector2 mTranslation;// { get; set; }

        public Vector2 nwPos;

        public float distanceFromTarget = 0; 
        public float maxSpeed = 180;
        public float maxAccel = 5.5f;
        public bool collided;

        public Entity()
        {
            mPosition = Vector2.Zero;
            mVelocity = Vector2.Zero;
            mOrientation = Vector2.Zero;
            mAcceleration = Vector2.Zero;
            mTranslation = Vector2.Zero;
            nwPos = Vector2.Zero;
            collided = false;

        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            mAssetName = theAssetName;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            mTranslation = mPosition + Globals.translation;
            //do wrap-around if necessary
            if (mTranslation.X > Globals.Universe.X) mTranslation.X -= Globals.Universe.X;
            else if (mTranslation.X < 0) mTranslation.X += Globals.Universe.X;
            if (mTranslation.Y > Globals.Universe.Y) mTranslation.Y -= Globals.Universe.Y;
            else if (mTranslation.Y < 0) mTranslation.Y += Globals.Universe.Y;

            theSpriteBatch.Draw(mSpriteTexture, mTranslation,
                new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
                Color.White, Globals.VectorToAngle(mOrientation), new Vector2(mSpriteTexture.Width / 2, mSpriteTexture.Height / 2), 0.4f, SpriteEffects.None, 0);
            
        }

        public void Update(GameTime theGameTime)
        {
                mVelocity += mAcceleration;
                mOrientation = Vector2.Normalize(mVelocity);
                mPosition += mVelocity * (float)theGameTime.ElapsedGameTime.TotalSeconds;
                //do wrap-around if necessary
                if (mPosition.X > Globals.Universe.X) mPosition.X -= Globals.Universe.X;
                else if (mPosition.X < 0) mPosition.X += Globals.Universe.X;
                if (mPosition.Y > Globals.Universe.Y) mPosition.Y -= Globals.Universe.Y;
                else if (mPosition.Y < 0) mPosition.Y += Globals.Universe.Y;
        }







        public static float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X) + (float)Math.PI / 2;
        }


    }
}
