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
        SpriteAnimation loading;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Create player and ships here
        Player thePlayer = new Player();
        Planet[] thePlanets = new Planet[50];

        List<AIShip> theShips = new List<AIShip>();
        List<AIShip> theShipsType2 = new List<AIShip>();
        List<Bullet> myBullets = new List<Bullet>();
        List<Bullet> enemyBullets = new List<Bullet>();


        bool shot = false;
        float score = 0;
        //for score info
        SpriteFont font;

        SoundEffect shotSoundEffect;
        SoundEffectInstance shotSoundInstance;
        SoundEffect backroundMusic;
        SoundEffectInstance backroundMusicInstance;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //set preferred screen dimensions
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;

            //graphic s.IsFullScreen = true;
            //initialise all entities
            
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
            for (int i = 0; i < 5; i++)
            {
                theShips.Add( new AIShip());
                theShips[i].maxSpeed = 280;
            }
            for (int i = 0; i < 10; i++)
            {
                theShipsType2.Add(new AIShip());
                theShipsType2[i].health = 30;
            }
            for (int i = 0; i < thePlanets.Length; i++)
            {
                thePlanets[i] = new Planet();
            }
            for (int i = 0; i < 5; i++)
            {
                myBullets.Add(new Bullet());
            }
            for (int i = 0; i < 10; i++)
            {
                enemyBullets.Add(new Bullet());
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            loading = new SpriteAnimation(Content.Load<Texture2D>("explosion-sprite-sheet-i0"), 30);
            //loading.Position = new Vector2(100, 100);
            loading.IsLooping = true;
            loading.FramesPerSecond = 10;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("MyFont");
            
            // TODO: use this.Content to load your game content here
            thePlayer.LoadContent(this.Content, "FAX-44 MkIII a");

            //myBullets.Add(new Bullet(thePlayer.getPos, new Vector2(0, 0), this.Content, "Bubble"));

            Globals.radarBackground = Content.Load<Texture2D>("radarBackgroundLrg");
            Globals.redPixel=Content.Load<Texture2D>("redPixel");
            Globals.yellowPixel = Content.Load<Texture2D>("yellowPixel");
            Globals.bluePixel = Content.Load<Texture2D>("bluePixel");
            Globals.whitePixel = Content.Load<Texture2D>("whitePixel");

            shotSoundEffect = Content.Load<SoundEffect>("big_gun");
            shotSoundInstance = shotSoundEffect.CreateInstance();

            backroundMusic = Content.Load<SoundEffect>("ingame1Boss");
            backroundMusicInstance = backroundMusic.CreateInstance();
            backroundMusicInstance.IsLooped = true;
            backroundMusicInstance.Play();
 
            for (int i = 0; i < theShips.Count; i++)
            {
                theShips[i].LoadContent(this.Content, "F-15F");
            }
            for (int i = 0; i < theShipsType2.Count; i++)
            {
                theShipsType2[i].LoadContent(this.Content, "F-22B");
            }
            for (int i = 0; i < thePlanets.Length; i++)
            {
                thePlanets[i].LoadContent(this.Content, "star");
            }
            for (int i = 0; i < myBullets.Count; i++)
            {
                myBullets[i].LoadContent(this.Content, "redLaserRayVert");
            }
            for (int i = 0; i < enemyBullets.Count; i++)
            {
                enemyBullets[i].LoadContent(this.Content, "Bubble");
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
            loading.Update(gameTime);
            thePlayer.rateOfFire += .1f;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true)
            {
                if (thePlayer.rateOfFire > 2)
                {
                    for (int i = 0; i < myBullets.Count; i++)
                    {
                        if (myBullets[i].alive != true && shot ==false)
                        {
                            myBullets[i].alive = true;
                            myBullets[i].Init(thePlayer);
                            thePlayer.rateOfFire = 0;
                            shot = true;
                        }
                    }
                    shot = false;
                }
            }


            for (int i = 0; i < myBullets.Count; i++)
            {
                for (int i2 = 0; i2 < theShips.Count; i2++)
                {
                    // if bullet collides with enemyship
                    if (myBullets[i].CheckCollision(myBullets[i], theShips[i2]) == true && myBullets[i].alive == true)
                    {
                        myBullets[i].Init(thePlayer);
                        myBullets[i].resetBullet();
                        theShips.RemoveAt(i2);
                        score += 100;
                        shotSoundInstance.Play();
                        
                    }
                }
                for (int i2 = 0; i2 < theShipsType2.Count; i2++)
                {
                    // if bullet collides with enemyship2
                    if (myBullets[i].CheckCollision(myBullets[i], theShipsType2[i2]) == true && myBullets[i].alive == true)
                    {
                        myBullets[i].Init(thePlayer);
                        myBullets[i].resetBullet();                       
                        theShipsType2[i2].health -= 10;
                        shotSoundInstance.Play();
                    }
                    if (theShipsType2[i2].health <= 0)
                    {
                        theShipsType2.RemoveAt(i2);
                        score += 100;
                    }
                }

                if (myBullets[i].TimeToLive > 100)
                {
                    myBullets[i].Init(thePlayer);
                    myBullets[i].resetBullet();
                }
                myBullets[i].Update(gameTime);
                myBullets[i].TimeToLive++;
            }

            for (int i = 0; i < enemyBullets.Count; i++)
            {
                enemyBullets[i].TimeToLive++;
                enemyBullets[i].Update(gameTime);
                for (int i2 = 0; i2 < theShipsType2.Count; i2++)
                {
                    if (enemyBullets[i].CheckCollision(enemyBullets[i], thePlayer) == true && enemyBullets[i].alive == true)
                    {
                        enemyBullets[i].Init(theShipsType2[i2]);
                        enemyBullets[i].resetBullet();
                        thePlayer.health -= 10;
                        shotSoundInstance.Play();
                    }
                    else if (enemyBullets[i].TimeToLive > 120)
                    {
                        enemyBullets[i].resetBullet();
                    }
                    if (enemyBullets[i].mVelocity.X < 1 && enemyBullets[i].mVelocity.Y < 1)
                    {
                        enemyBullets[i].resetBullet();
                    }

                    //float distanceFromTarget = Vector2.Distance(thePlayer.mPosition, theShipsType2[i2].mPosition);
                    //if (distanceFromTarget < 600 && enemyBullets[i].alive == false )
                    //{
                    //    enemyBullets[i].Init(theShipsType2[i2]);
                    //    enemyBullets[i].alive = true;
                    //}
                }
            }


            for (int i = 0; i < theShipsType2.Count; i++)
            {
                int i2 = i ;
                if (i2 < theShipsType2.Count -1)
                {
                    i2++;
                }
                else if (i2 == theShipsType2.Count - 1)
                {
                    i2 = 0;
                }
                theShipsType2[i].Update(gameTime);

                // check for ship to ship collision
                theShipsType2[i].AvoidCollison(theShipsType2[i2].mTranslation, thePlayer.mTranslation, theShipsType2[i].health);                

                float distanceFromTarget = Vector2.Distance(thePlayer.mPosition, theShipsType2[i].mPosition);
                //if near Player shoot
                if (distanceFromTarget < 500 && enemyBullets[i].alive == false)
                {
                    enemyBullets[i].Init(theShipsType2[i]);
                    enemyBullets[i].alive = true;
                }
            }


            for (int i = 0; i < theShips.Count; i++)
            {
                theShips[i].Update(gameTime);
                theShips[i].wandering(thePlayer.mTranslation);
            }


            thePlayer.Update(gameTime);

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
            if (thePlayer.health > 0)
            {
                for (int i = 0; i < thePlanets.Length; i++)
                {
                    thePlanets[i].Draw(spriteBatch);
                }

                thePlayer.Draw(spriteBatch);
                spriteBatch.DrawString(font, thePlayer.health.ToString(), thePlayer.mTranslation, Color.White);
                spriteBatch.DrawString(font, "Score : " + score.ToString(), Globals.topRight, Color.White);

                for (int i = 0; i < theShips.Count; i++)
                {
                    theShips[i].Draw(spriteBatch);
                }
                for (int i = 0; i < theShipsType2.Count; i++)
                {
                    theShipsType2[i].Draw(spriteBatch);
                }

                for (int i = 0; i < myBullets.Count; i++)
                {
                    if (myBullets[i].alive == true)
                    {
                        myBullets[i].Draw(spriteBatch);
                    }
                }
                for (int i = 0; i < enemyBullets.Count; i++)
                {
                    if (enemyBullets[i].alive == true)
                    {
                        enemyBullets[i].Draw(spriteBatch);
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
                    pos += Globals.radarTranslation;
                    pos = Globals.wrapAround(pos);
                    pos = pos / Globals.grainSize;
                    spriteBatch.Draw(Globals.redPixel, pos, Color.White);
                }
                for (int i = 0; i < theShipsType2.Count; i++)
                {
                    Vector2 pos = theShipsType2[i].mPosition;
                    pos += Globals.radarTranslation;
                    pos = Globals.wrapAround(pos);
                    pos = pos / Globals.grainSize;
                    spriteBatch.Draw(Globals.redPixel, pos, Color.White);
                }
                for (int i = 0; i < thePlanets.Length; i++)
                {
                    Vector2 pos = thePlanets[i].mPosition;
                    pos += Globals.radarTranslation;
                    pos = Globals.wrapAround(pos);
                    pos = pos / Globals.grainSize;
                    spriteBatch.Draw(Globals.yellowPixel, pos, Color.White);
                }
            }
            else
            {
                Vector2 nwGgPosition = new Vector2(thePlayer.mTranslation.X, thePlayer.mTranslation.Y);
                nwGgPosition.X = nwGgPosition.X - 120;
                spriteBatch.DrawString(font, thePlayer.gameOver, nwGgPosition, Color.Red, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 1);
                nwGgPosition.Y = nwGgPosition.Y - 120;
                spriteBatch.DrawString(font, "Score : " +score.ToString(), nwGgPosition, Color.Red, 0, new Vector2(0, 0), 3.0f, SpriteEffects.None, 1);
            }
            //loading.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
