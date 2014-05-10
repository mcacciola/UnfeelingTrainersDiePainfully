using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UTDP
{
    public class Enemy :Animation //Sprite //
    {
        //The health the enemy starts with
        protected float startHealth;
        
        //The health the enemy currently has.
        protected float currentHealth;

        //Whether or not the enemy is alive.
        protected bool alive = true;

        //The speed of the enemy.
        protected float speed;

        //The amount of money the player receives for killing this enemy.
        protected int bountyGiven;

        //The waypoints the enemy will use.
        private Queue<Vector2> waypoints = new Queue<Vector2>();

        //Public Property for the current health of the enemy.
        public float CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        //Public property that returns whether the enemy is dead or not.
        public bool IsAlive
        {
            get { return alive; }
        }

        //Public read-only property that returns the amount of money the player should make on a successful kill.
        public int BountyGiven
        {
            get { return bountyGiven; }
        }

        private float speedModifier;

        private float modifierDuration;
        private float modiferCurrentTime;

        /// <summary>
        /// Alters the speed of the enemy.
        /// </summary>
        public float SpeedModifier
        {
            get { return speedModifier; }
            set { speedModifier = value; }
        }
        /// <summary>
        /// Defines how long the speed modification will last.
        /// </summary>
        public float ModifierDuration
        {
            get { return modifierDuration; }
            set
            {
                modifierDuration = value;
                modiferCurrentTime = 0;
            }
        }

        private float fireCurrentTime;

        private float fireDuration;

        public float FireDuration
        {
            get { return fireDuration; }
            set { fireDuration = value; }
        }

        private float fireDamage;

        public float FireDamage
        {
            get { return fireDamage; }
            set { fireDamage = value; }
        }

        private bool onFire = false;
        public bool OnFire
        {
            get { return onFire; }
            set { onFire = value; }
        }

        public new Vector2 Center
        {
            get { return this.center; }
        }

        static Random myRandom = new Random();

        //Constructor that initializes data members
        public Enemy(Texture2D[] textures, Vector2 position, int startingDirection, int waveNumber, float multiplier)
            : base(textures[myRandom.Next(6)], position)
        {
            int modifier = waveNumber + 1;
            this.startHealth = modifier * 100 + myRandom.Next(modifier * 80 );
            this.currentHealth = startHealth * multiplier;
            int intSpeed = myRandom.Next(modifier, modifier * 2);
            if (intSpeed < 5)
            {
                intSpeed = 5;
            }
            this.speed = intSpeed;
            this.speed = (speed / 10) * multiplier;
            int healthBounty = (int) startHealth / 100;
            float speedBounty = this.speed * 10;
            this.bountyGiven = healthBounty + (int)speedBounty;
            this.bountyGiven = (this.bountyGiven / 2) + 2;
            //alive = true;
            Initialize(texture, position, 32, 32, 16, 120, Color.White, 1f, true);//Testing Child animation
            this.Direction = startingDirection;
        }

        //Sets the waypoints for the enemy.
        public void SetWaypoints(Queue<Vector2> waypoints)
        {
            foreach (Vector2 waypoint in waypoints)
                this.waypoints.Enqueue(waypoint);

            this.position = this.waypoints.Dequeue();
        }



        //Checks to see if enemy is at its next waypoint yet.
        public float DistanceToDestination
        {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }

        //Tracks if the enemy is alive, updates position via Sprite superclass.
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.center = new Vector2(Position.X + 16, Position.Y + 16);

            if (waypoints.Count > 0 && IsAlive)
            {
                if (DistanceToDestination < speed)
                {
                    position = waypoints.Peek();
                    waypoints.Dequeue();

                    if (waypoints.Count > 0)
                    {
                        Vector2 direction = waypoints.Peek() - position;
                        direction.Normalize();
                        //Facing of animation
                        
                        if (direction.X < 0)
                            Direction = 3;//Left
                        if (direction.X > 0)
                            Direction = 4;//Right

                        if (direction.Y < 0)
                            Direction = 1;//Up
                        if (direction.Y > 0)
                            Direction = 2;//Down    
                    }
                }

                else
                {
                    Vector2 direction = waypoints.Peek() - position;
                    direction.Normalize();

                    // Store the original speed.
                    float temporarySpeed = speed;

                    // If the modifier has finished,
                    if (modiferCurrentTime >= modifierDuration)
                    {
                        // reset the modifier.
                        speedModifier = 0;
                        modiferCurrentTime = 0;
                    }

                    if (fireCurrentTime >= FireDuration)
                    {
                        OnFire = false;
                        FireDamage = 0;
                        fireCurrentTime = 0;

                    }

                    if (speedModifier != 0 && modiferCurrentTime < modifierDuration)
                    {
                        // Modify the speed of the enemy.
                        temporarySpeed *= speedModifier;
                        // Update the modifier timer.
                        modiferCurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    if (FireDamage != 0 && fireCurrentTime < FireDuration)
                    {
                        this.onFire = true;
                        this.currentHealth -= FireDamage;
                        fireCurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }

                    velocity = Vector2.Multiply(direction, temporarySpeed);

                    position += velocity;
                }
            }
            else
                alive = false;

            if (currentHealth <= 0)
                alive = false;
        }

        //Draws the enemy with animation.

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                if (OnFire)
                {
                    base.Draw(spriteBatch, Color.OrangeRed);
                }
                else
                {
                    base.Draw(spriteBatch);
                }
            }
        }
        /*public override void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                float healthPercentage = (float)currentHealth / (float)startHealth;

                

                base.Draw(spriteBatch, color);
            }
        }*/
    }
}
