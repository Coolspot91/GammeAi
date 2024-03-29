﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


    public class SpriteManager
    {
        protected Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;
        protected Rectangle[] Rectangles;
        protected int FrameIndex = 0;

        public SpriteManager(Texture2D Texture, int frames)
        {
            this.Texture = Texture;
            int width = Texture.Width / 5;
            int height = Texture.Width / 5;
            Rectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
                Rectangles[i] = new Rectangle(
                    i * width, i*height, width, height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Rectangles[FrameIndex],
                Color, Rotation, Origin, Scale, SpriteEffect, 0f);
        }
    }
