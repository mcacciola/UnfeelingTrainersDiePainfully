using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UTDP
{
    public class Sprite
    {
        //The image of the Sprite.
        protected Texture2D texture;

        //Where the sprite is
        protected Vector2 position;

        //How fast the sprite is moving.
        protected Vector2 velocity;

        //The center of the sprite.
        protected Vector2 center;

        //The top left corner of the sprite.
        protected Vector2 origin;

        //The rotation of the sprite.
        protected float rotation;

        //Read-only property of the center of the sprite.
        public Vector2 Center
        {
            get { return center; }
        }

        //Read-only property of the sprite's position.
        public Vector2 Position
        {
            get { return position; }
        }

        public int HitBoxRadius;

        //Sprite constructor, sets initial values for data members.
        public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;

            position = pos;
            velocity = Vector2.Zero;

            center = new Vector2(position.X + 16, position.Y + 16);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            HitBoxRadius = 6;//tex.Width - 16;//tex.Width / 2;
        }

        //Update method tracks the location of the center of the sprite.
        public virtual void Update(GameTime gameTime)
        {
            this.center = new Vector2(position.X + texture.Width / 2,
            position.Y + texture.Height / 2);
        }

        //Draws the sprite using the data members.
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, center, null, Color.White,
                rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        //Draws the sprite with a different color.
        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, center, null, color, rotation,
                origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
