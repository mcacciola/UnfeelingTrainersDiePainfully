using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace UTDP
{
    public class Tower : Animation
    {
        //the cost of the tower
        protected int cost;

        protected bool soundOn;

        public bool SoundOn
        {
            get { return soundOn; }
            set { soundOn = value; }
        }

        //How much damage the tower can do to enemies.
        protected int damage;

        //The texture of the bullet fired by a particular tower.
        protected Texture2D bulletTexture;

        //The range of the tower as a radius.
        protected float range;

        //Tracks time since bullet was fired.
        protected float bulletTimer;

        //List of bullets fired by the tower.
        protected List<Bullet> bulletList = new List<Bullet>();

        //the rate of fire for the tower.
        protected float fireRate;

        protected SoundEffect bulletSound;

        //Read-only property of the cost.
        public int Cost
        {
            get { return cost; }
        }

        //Read-only property of the damage
        public int Damage
        {
            get { return damage; }
        }

        //Read-Only property of the Range.
        public float Range
        {
            get { return range; }
        }

        //The closest enemy, and therefore the target.
        protected Enemy target;

        //Read-only property of the target.
        public Enemy Target
        {
            get { return target; }
        }

        private Vector2 centerTower;

        public new Vector2 center
        {
            get { return centerTower; }
        }

        protected Texture2D[] textures;

        public Texture2D[] Textures
        {
            get { return textures; }
        }

        protected bool isUpgraded = false;

        public int UpgradeCost
        {
            get;
            set;
        }

        //Constructor for the Tower, calls the base constructor and initializes bulletTexture.
        public Tower(Texture2D[] newTextures, Texture2D bulletTexture, SoundEffect sound, Vector2 position)
            : base(newTextures[0], position)
        {
            this.bulletTexture = bulletTexture;
            //Initialize(texture, position, 32, 32, 4, 60, Color.White, 1f, true);
            centerTower = new Vector2(Position.X + 16, Position.Y + 16);
            this.textures = newTextures;
            this.bulletSound = sound;
            
        }

        //Checks to see if the object passed is in range.
        public bool IsInRange(Vector2 position)
        {
            if (Vector2.Distance(center, position) <= range)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Checks to see which enemy is the closest and sets it as the target.
        public void GetClosestEnemy(List<Enemy> enemies)
        {
            target = null;
            float smallestRange = range;

            foreach (Enemy enemy in enemies)
            {
                if (Vector2.Distance(center, enemy.Center) < smallestRange)
                {
                    smallestRange = Vector2.Distance(center, enemy.Center);
                    target = enemy;
                }
            }
        }

        //Changes Direction to face target.
        protected void FaceTarget()
        {
            AimBullet();

            Vector2 enemyAt = center - target.Center;
            if (enemyAt.X >= 0 && enemyAt.Y >= 0  && Direction != 3)
            {
                Direction = 3;
            }
            if (enemyAt.X < 0 && enemyAt.Y >= 0 && Direction != 4)
            {
                Direction = 4;
            }
            if (enemyAt.X < 0 && enemyAt.Y < 0 && Direction != 2)
            {
                Direction = 2;
            }
            if( enemyAt.X >= 0 && enemyAt.Y < 0 && Direction != 1)
            {
                Direction = 1;
            }
        }

        private void AimBullet()
        {
            Vector2 direction = center - target.Center;
            direction.Normalize();
            rotation = (float)Math.Atan2(-direction.X, direction.Y);
        }


        //Updates the direction the tower is facing, the target and whether or not a bullet should be fired.
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            bulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (target != null)
            {
                if (target.IsAlive)
                    FaceTarget();

                if (!IsInRange(target.Center) || !target.IsAlive)
                {
                    target = null;
                    bulletTimer = 0;
                }

            }
        }

        //Draws all bullets fired by this tower.
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in bulletList)
                bullet.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        public virtual void Upgrade()
        {
            if (!isUpgraded)
            {
                this.spriteStrip = textures[1];
                isUpgraded = true;
            }
        }
    }
}
