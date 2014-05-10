using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UTDP
{
    class EnemyWave
    {
        private int numOfEnemies; // Number of enemies to spawn
        private int waveNumber; // What wave is this?
        private float spawnTimer = 0; // When should we spawn an enemy
        private int enemiesSpawned = 0; // How many enemies have spawned

        private bool enemyAtEnd; // Has an enemy reached the end of the path?
        private bool spawningEnemies; // Are we still spawning enemies?
        private Level level; // A reference of the level
        private Texture2D[] enemyTextures; // textures for the enemies
        public List<Enemy> enemies = new List<Enemy>(); // List of enemies

        private float multiplier;

        private Player player; // A reference to the player.

        //Read-only property that returns true if the count = 0 and enemiesSpawned = number of enemies to spawn
        public bool RoundOver
        {
            get
            {
                return enemies.Count == 0 && enemiesSpawned == numOfEnemies;
            }
        }

        //Read-Only property that returns the round number
        public int RoundNumber
        {
            get { return waveNumber; }
        }

        //Returns if an enemy made it to the end.
        public bool EnemyAtEnd
        {
            get { return enemyAtEnd; }
            set { enemyAtEnd = value; }
        }
        //Public read-only property of the enemies that are in the world.
        public List<Enemy> Enemies
        {
            get { return enemies; }
        }
        private int enemyStartDirection;

        //Constructor for EnemyWave
        public EnemyWave(int waveNumber, int numOfEnemies,
            Level level, Texture2D[] enemyTextures, Player player, int startingDirection, float multiplier)
        {
            this.waveNumber = waveNumber;
            this.numOfEnemies = numOfEnemies;

            this.level = level;
            this.enemyTextures = enemyTextures;
            this.player = player;
            this.enemyStartDirection = startingDirection;
            this.multiplier = multiplier;
        }

        //Adds an enemy to the world.
        private void AddEnemy()
        {
            Enemy enemy = new Enemy(enemyTextures,
            level.Waypoints.Peek(), enemyStartDirection, waveNumber, multiplier);
            enemy.SetWaypoints(level.Waypoints);
            enemies.Add(enemy);
            spawnTimer = 0;

            enemiesSpawned++;
        }

        //Starts spawning enemies.
        public void Start()
        {
            spawningEnemies = true;
        }

        public void Update(GameTime gameTime)
        {
            if (enemiesSpawned == numOfEnemies)
                spawningEnemies = false; // We have spawned enough enemies
            if (spawningEnemies)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (spawnTimer > 2)
                    AddEnemy(); // Time to add a new enemey
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                enemy.Update(gameTime);
                if (!enemy.IsAlive)
                {
                    if (enemy.CurrentHealth > 0) // Enemy is at the end
                    {
                        enemyAtEnd = true;
                        player.Lives -= 1;
                    }
                    else
                    {
                        player.Money += enemy.BountyGiven;
                        player.Score += enemy.BountyGiven;
                    }

                    enemies.Remove(enemy);
                    i--;
                }
            }
        }

        //Draws the enemies.
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch);
        }
    }
}
