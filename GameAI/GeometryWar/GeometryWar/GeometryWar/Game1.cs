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
        //AIShip[] theShips = new AIShip[Globals.EnemyCount];
        AIShip[] theShipsFlee = new AIShip[Globals.EnemyCountT2];
        Planet[] thePlanets = new Planet[50];

        List<AIShip> theShips = new List<AIShip>();
        List<Bullet> myBullets = new List<Bullet>();

        int counter = 0;
        float rateOfFire = 0;
        bool shot = false;
        //Texture2D bulTexture;
        //Bullet[] myBullets = new Bullet[50];
        
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
            for (int i = 0; i < 2; i++)
            {
                theShips.Add( new AIShip());
            }
            for (int i = 0; i < theShipsFlee.Length; i++)
            {
                theShipsFlee[i] = new AIShip();
            }
            for (int i = 0; i < thePlanets.Length; i++)
            {
                thePlanets[i] = new Planet();
            }
            for (int i = 0; i < 5; i++)
            {
                myBullets.Add(new Bullet());
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

            //for (int i = 0; i < myBullets.Count; i++)
            //{
            //    myBullets[i].LoadContent(this.Content, "Bubble");
            //}
            //myBullets.Add(new Bullet(thePlayer.getPos, new Vector2(0, 0), this.Content, "Bubble"));

            Globals.radarBackground = Content.Load<Texture2D>("radarBackgroundLrg");
            Globals.redPixel=Content.Load<Texture2D>("redPixel");
            Globals.yellowPixel = Content.Load<Texture2D>("yellowPixel");
            Globals.bluePixel = Content.Load<Texture2D>("bluePixel");
            Globals.whitePixel = Content.Load<Texture2D>("whitePixel");
            //bulTexture = Content.Load<Texture2D>("Bubble");
            for (int i = 0; i < theShips.Count; i++)
            {
                theShips[i].LoadContent(this.Content, "F-15F");
            }
            for (int i = 0; i < theShipsFlee.Length; i++)
            {
                theShipsFlee[i].LoadContent(this.Content, "F-22B");
            }
            for (int i = 0; i < thePlanets.Length; i++)
            {
                thePlanets[i].LoadContent(this.Content, "asteroid");
            }
            for (int i = 0; i < myBullets.Count; i++)
            {
                myBullets[i].LoadContent(this.Content, "Bubble");
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
            rateOfFire += .1f;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true)
            {
                if (rateOfFire > 2)
                {
                    for (int i = 0; i < myBullets.Count; i++)
                    {
                        if (myBullets[i].alive != true && shot ==false)
                        {
                            myBullets[i].alive = true;
                            myBullets[i].Init(thePlayer);
                            rateOfFire = 0;
                            shot = true;
                        }
                    }
                    shot = false;
                }
            }

            //for (int i = 0; i < myBullets.Count; i++)
            //{
            //    //if (myBullets[i].TimeToLive > 400)
            //    //{
            //    //    //myBullets.RemoveAt(i);
            //    //    myBullets[i].Init(thePlayer);
            //    //    myBullets[i].TimeToLive =0;
            //    //    myBullets[i].alive = false;
            //    //    counter--;
            //    //}
            //}

            for (int i = 0; i < myBullets.Count; i++)
            {
                for (int i2 = 0; i2 < theShips.Count; i2++)
                {
                    if (myBullets[i].CheckCollision(myBullets[i], theShips[i2]) == true)
                    {
                        //myBullets.RemoveAt(i);
                        myBullets[i].Init(thePlayer);
                        myBullets[i].TimeToLive = 0;
                        myBullets[i].alive = false;
                        theShips.RemoveAt(i2);

                    }
                    else if (myBullets[i].TimeToLive > 400)
                    {
                        //myBullets.RemoveAt(i);
                        myBullets[i].Init(thePlayer);
                        myBullets[i].TimeToLive = 0;
                        myBullets[i].alive = false;
                    }
                }
            }

            //for (int i = 0; i < myBullets.Count; i++)
            //{
            //    for (int i2 = 0; i2 < theShips.Length; i2++)
            //    {
            //        if (shot == true)
            //        {

            //            if (myBullets[i].CheckCollision(myBullets[i], theShips[i2]) == true)
            //            {
            //                myBullets.RemoveAt(i);
            //            }
            //            else if (myBullets[i].TimeToLive > 400)
            //            {
            //                myBullets.RemoveAt(i);
            //            }
            //        }
            //    }
            //}


            // TODO: Add your update logic here
            thePlayer.Update(gameTime);
            for (int i = 0; i < theShips.Count; i++)
            {
                theShips[i].Update(gameTime);

                theShipsFlee[i].Update(gameTime);
                theShips[i].seek2(thePlayer);
                //theShipsFlee[i].wandering(thePlayer);


                // Loops through shipFlee, if its not colliding with self move one ship away from other
                //for (int i2 = 0; i2 < theShipsFlee.Length; i2++)
                //{
                //    if (i != i2)
                //    {
                //        theShipsFlee[i].AvoidingObsSeek(thePlayer, theShipsFlee[i2]);
                //    }
                //}
            }

            for (int i = 0; i < myBullets.Count; i++)
            {
                myBullets[i].Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            //Calculate translation to centre screen
            Globals.translation = Globals.Centre - thePlayer.mPosition;
            spriteBatch.Begin();
            thePlayer.Draw(spriteBatch);
            for (int i = 0; i < theShips.Count; i++)
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

            for (int i = 0; i < myBullets.Count; i++)
            {
                if (myBullets[i].alive == true)
                {
                    myBullets[i].Draw(spriteBatch);
                }
            }


            //spriteBatch.DrawString(font, thePlayer.mPosition.ToString(), new Vector2(20, 45), Color.White);            
            //Draw Radar!!
            //one pixel per ship/planet
            //radar is in top left corner
            Globals.radarTranslation = Globals.Universe / 2 - thePlayer.mPosition;
            spriteBatch.Draw(Globals.radarBackground, Vector2.Zero, Color.White);
            for (int i = 0; i < theShips.Count; i++)
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
