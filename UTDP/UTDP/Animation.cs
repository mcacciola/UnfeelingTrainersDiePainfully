﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace UTDP
{
    public class Animation : Sprite
    {

        //The image representing the collection of images used for animation 
        protected Texture2D spriteStrip;

        //The scale used to display the sprite strip
        float scale;

        //The time since we last updated the frame
        int elapsedTime;

        //The time we display a frame until the next one
        int frameTime;

        //The number of frames that the animation contains
        int frameCount;

        //The index of the current frame we are displaying
        int currentFrame;

        //The color of the frame we will be displaying
        Color color;

        //The area of the image strip we want to display
        Rectangle sourceRect = new Rectangle();

        //The area where we want to display the image strip in the game
        Rectangle destinationRect = new Rectangle();

        //Width of a given frame
        public int FrameWidth;

        //Height of a given frame
        public int FrameHeight;

        //The state of the animation
        public bool Active;

        //Determines if the animation will keep playing or deactivate after one run
        public bool Looping;

        //x,y position of a given frame
        public Vector2 FramePosition;


        //Direction of the animation. 1(South, SW), 2 (East, SE), 3 (North, NW), 4(West, NE)
        protected int direction;

        public int Direction
        {
            get { return direction; }
            set 
            { direction = value;
            if (direction == 1)
            {
                currentFrame = 0;
            }
            else if (direction == 2)
            {
                currentFrame = (int)(frameCount * .25);
            }
            else if (direction == 3)
            {
                currentFrame = (int)(frameCount * .5);
            }
            else if (direction == 4)
            {
                currentFrame = (int)(frameCount * .75);
            }
            
            }
        }

        public Animation(Texture2D texture, Vector2 position)
            : base(texture, position)
        {

        }

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int FrameHeight, int frameCount, int frameTime, Color color, float scale, bool looping)
        {
            //Keep a local copy of the values passed in
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = FrameHeight;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.scale = scale;

            Looping = looping;
            FramePosition = position;
            spriteStrip = texture;

            //Set the time to zero
            elapsedTime = 0;
            currentFrame = 0;

            //Set the animation to active by default
            Active = true;

            //Set default direction to 1(North, NE)
            Direction = 1;
        }
        public override void Update(GameTime gameTime)
        {
            //Do not update the game if we are not active
            if (!Active)
                return;

            //Update the elapsed time
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;


            if (Direction == 1)
            {
                //currentFrame = 0;

                if (elapsedTime > this.frameTime)
                {
                    currentFrame++;

                    if (currentFrame == (frameCount * .25))
                    {
                        currentFrame = 0;

                        if (!Looping)
                            Active = false;

                    }
                    elapsedTime = 0;
                    
                }
            }

            if (Direction == 2)
            {
                //currentFrame = (int)(frameCount * .25);

                if (elapsedTime > frameTime)
                {
                    currentFrame++;

                    if (currentFrame == (frameCount * .5))
                    {
                        currentFrame = (int)(frameCount * .25);

                        if (!Looping)
                            Active = false;

                    }
                    elapsedTime = 0;
                    
                }
            }

            if (Direction == 3)
            {

                if (elapsedTime > frameTime)
                {
                    currentFrame++;

                    if (currentFrame == (frameCount * .75))
                    {
                        currentFrame = (int)(frameCount * .5); 

                        if (!Looping)
                            Active = false;

                    }
                    elapsedTime = 0;
                   
                }
            }

            if (Direction == 4)
            {

                if (elapsedTime > frameTime)
                {
                    currentFrame++;

                    if (currentFrame == (frameCount))
                    {
                        currentFrame = (int)(frameCount * .75);

                        if (!Looping)
                            Active = false;

                    }
                    elapsedTime = 0;
                    
                }
            }

            //Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);

            //Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            destinationRect = new Rectangle((int)Position.X, (int)Position.Y, (int)(FrameWidth * scale), (int)(FrameHeight * scale));

            /*//If the elapsed time is larger than the frametime
            //we need to switch frames
            if (elapsedTime > frameTime)
            {
                //Move to the next frame
                currentFrame++;

                //If the currentFrame is equal to frameCount reset currentFrame to zero
                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    //If we are not looping, deactivate the animation
                    if (!Looping)
                        Active = false;
                }

                //REset the elapsed time to zero
                elapsedTime = 0;
            }*/

        }

        
        


        //Draw the animation strip
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Only draw the animation when we are active
            if (Active)
            {
                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color);
        }


    }
}
