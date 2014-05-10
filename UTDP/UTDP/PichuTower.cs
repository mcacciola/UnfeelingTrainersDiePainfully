using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace UTDP
{
    class PichuTower : Tower
    {
        //Constructor
        public PichuTower(Texture2D[] textures, Texture2D bulletTexture, SoundEffect sound, Vector2 position, bool soundOn)
            : base(textures, bulletTexture, sound, position)
        {
            this.damage = 10; // Set the damage
            this.cost = 15;   // Set the initial cost

            this.fireRate = .5f;

            this.range = 64; // Set the radius

            this.UpgradeCost = 22;
            this.soundOn = soundOn;
            Initialize(textures[0], position, 32, 32, 16, 120, Color.White, 1f, true);
           
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (bulletTimer >= fireRate && target != null)
            {
                Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(center,
                    new Vector2(bulletTexture.Width / 2)), rotation, 6, damage);

                bulletList.Add(bullet);
                if (soundOn)
                {
                    bulletSound.Play();
                }
                bulletTimer = 0;
            }


            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];

                bullet.SetRotation(rotation);
                bullet.Update(gameTime);

                if (!IsInRange(bullet.Center))
                    bullet.Kill();

                if (target != null && Vector2.Distance(bullet.Center, target.Center) < bullet.HitBoxRadius + target.HitBoxRadius)
                {
                    target.CurrentHealth -= bullet.Damage;
                    bullet.Kill();
                }

                if (bullet.IsDead())
                {
                    bulletList.Remove(bullet);
                    i--;
                }
            }
        }

        public override void Upgrade()
        {
            this.damage += 5;
            this.range += 32;
            this.fireRate -= .1f;
            base.Upgrade();
        }
    }
}