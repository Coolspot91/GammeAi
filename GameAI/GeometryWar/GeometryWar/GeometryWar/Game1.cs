using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GeometryWar
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Create player and ships here
        Player thePlayer = new Player();
        AIShip[] theShips = new AIShip[Globals.EnemyCount];
        AIShip[] theShipsFlee = new AIShip[Globals.EnemyCountT2];
        Planet[] thePlanets = new Planet[50];
        //for score info
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //set preferred screen dimensions
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;

            //graphics.IsFullScreen = true;
            //initialise all entities
            //thePlayer.mVelocity = new Vector2(2, 2);
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            for (int i = 0; i < theShips.Length; i++)
            {
                theShips[i] = new AIShip();
            }
            for (int i = 0; i < theShipsFlee.Length; i++)
            {
                theShipsFlee[i] = new AIShip();
            }
            for (int i = 0; i < thePlanets.Length; i++)
            {
                thePlanets[i] = new Planet();
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //load font
            font = Content.Load<SpriteFont>("MyFont");
            // TODO: use this.Content to load your game content here
            thePlayer.LoadContent(this.Content, "FAX-44 MkIII a");
            
            Globals.radarBackground = Content.Load<Texture2D>("radarBackgroundLrg");
            Globals.redPixel=Content.Load<Texture2D>("redPixel");
            Globals.yellowPixel = Content.Load<Texture2D>("yellowPixel");
            Globals.bluePixel = Content.Load<Texture2D>("bluePixel");
            Globals.whitePixel = Content.Load<Texture2D>("whitePixel");
            for (int i = 0; i < theShips.Length; i++)
            {
                theShips[i].LoadContent(this.Content, "F-15F");
            }
            for (int i = 0; i < theShipsFlee.Length; i++)
            {
                theShipsFlee[i].LoadContent(this.Content, "F-22B");
            }
            for (int i = 0; i < thePlanets.Length; i++)
            {
                thePlanets[i].LoadContent(this.Content, "Bubble");
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            thePlayer.Update(gameTime);
            for (int i = 0; i < theShips.Length; i++)
            {
                theShips[i].Update(gameTime);

                theShipsFlee[i].Update(gameTime);
                //theShips[i].seek2(thePlayer);
                //theShipsFlee[i].wandering(thePlayer);


                // Loops through shipFlee, if its not colliding with self move one ship away from other
                for (int i2 = 0; i2 < theShipsFlee.Length; i2++)
                {
                    if (i != i2)
                    {
                        theShipsFlee[i].AvoidingObsSeek(thePlayer, theShipsFlee[i2]);
                    }
                }
            }


            //// Loops through shipFlee, if its not colliding with self move one ship away from other
            //for (int i2 = 0; i2 < theShipsFlee.Length; i2++)
            //{
            //    if (i != i2)
            //    {
            //        theShipsFlee[i].AvoidingObsSeek(thePlayer, theShipsFlee[i2]);
            //    }
            //}
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //Calculate translation to centre screen
            Globals.translation = Globals.Centre - thePlayer.mPosition;
            spriteBatch.Begin();
            thePlayer.Draw(spriteBatch);
            for (int i = 0; i < theShips.Length; i++)
            {
                theShips[i].Draw(spriteBatch);
                theShipsFlee[i].Draw(spriteBatch);
                Vector2 pos = new Vector2(theShips[i].mPosition.X + Globals.translation.X, theShips[i].mPosition.Y + Globals.translation.Y);

                Vector2 posForFlee = new Vector2(theShipsFlee[i].mPosition.X + Globals.translation.X, theShips[i].mPosition.Y + Globals.translation.Y);

                spriteBatch.DrawString(font, thePlayer.mPosition.ToString(), thePlayer.mTranslation, Color.White);

                spriteBatch.DrawString(font, theShipsFlee[i].mVelocity.ToString(), theShipsFlee[i].mTranslation, Color.White);
            }
            for (int i = 0; i < thePlanets.Length; i++)
            {
                thePlanets[i].Draw(spriteBatch);
            }
            //spriteBatch.DrawString(font, thePlayer.mPosition.ToString(), new Vector2(20, 45), Color.White);
            
            //Draw Radar!!
            //one pixel per ship/planet
            //radar is in top left corner
            Globals.radarTranslation = Globals.Universe / 2 - thePlayer.mPosition;
            spriteBatch.Draw(Globals.radarBackground, Vector2.Zero, Color.White);
            for (int i = 0; i < theShips.Length; i++)
            {
                Vector2 pos = theShips[i].mPosition;
                pos+=Globals.radarTranslation;
                pos = Globals.wrapAround(pos);
                pos = pos / Globals.grainSize;
                spriteBatch.Draw(Globals.redPixel, pos,Color.White);
            }
            for (int i = 0; i < thePlanets.Length; i++)
            {
                Vector2 pos = thePlanets[i].mPosition;
                pos += Globals.radarTranslation;
                pos = Globals.wrapAround(pos);
                pos = pos / Globals.grainSize; 
                spriteBatch.Draw(Globals.yellowPixel, pos, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
