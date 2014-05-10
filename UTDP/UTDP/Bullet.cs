using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UTDP
{
    public class Bullet : Sprite
    {
        //The damage the bullet does.
        private int damage;

        //How long the bullet has existed.
        private int age;

        //The speed of the bullet.
        private int speed;

        //Read-only property of bullet damage
        public int Damage
        {
            get { return damage; }
        }

        //Returns that the bullet is dead if its age is over 100.
        public bool IsDead()
        {
            return age > 100;
        }

        //Constructor initializes values.
        public Bullet(Texture2D texture, Vector2 position, float rotation,
            int speed, int damage)
            : base(texture, position)
        {
            this.rotation = rotation;
            this.damage = damage;

            this.speed = speed;
        }

        //Destroys the bullet object.
        public void Kill()
        {
            this.age = 200;
        }

        //Updates the bullet by moving it and aging it.
        public override void Update(GameTime gameTime)
        {
            age++;
            position += velocity;
            base.Update(gameTime);
        }

        //Aims the bullet towards the enemy.
        public void SetRotation(float value)
        {
            rotation = value;

            velocity = Vector2.Transform(new Vector2(0, -speed),
                Matrix.CreateRotationZ(rotation));
        }
    }
}
