using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UTDP
{
    class WaveManager
    {

        private int numberOfWaves; // How many waves the game will have
        private float timeSinceLastWave; // How long since the last wave ended

        private Queue<EnemyWave> waves = new Queue<EnemyWave>(); // A queue of all our waves

        private Texture2D[] enemyTextures; // The textures used to draw the enemies

        private bool waveFinished = false; // Is the current wave over?

        private Level level; // A reference to our level class

        public EnemyWave CurrentWave // Get the wave at the front of the queue
        {
            get {  return waves.Peek(); }
        }

        public List<Enemy> Enemies // Get a list of the current enemeies
        {
            get { return CurrentWave.Enemies; }
        }

        /*public int Round // Returns the wave number
        {
            get { return CurrentWave.RoundNumber + 1; }
        }*/

        public int printWave { get; set; }



        private bool levelFinished = false;
        public bool LevelFinished
        {
            get { return levelFinished; }
        }
        public int WavesLeft
        {
            get { return waves.Count; }
        }

        //Constructor for the WaveManager class
        public WaveManager(Level level, int numberOfWaves, Texture2D[] enemyTextures, Player player, int startingDirection, float multiplier)
        {
            this.numberOfWaves = numberOfWaves;
            this.enemyTextures = enemyTextures;

            this.printWave = 0;
            this.level = level;

            Console.WriteLine(level.Waypoints.Peek().X.ToString() + ", " + level.Waypoints.Peek().Y.ToString());

            for (int i = 0; i < numberOfWaves; i++)
            {
                int initialNumerOfEnemies = 10;
                Random myRandom = new Random();

                EnemyWave wave = new EnemyWave(i, initialNumerOfEnemies + myRandom.Next(i), level, enemyTextures, player, startingDirection, multiplier);

                waves.Enqueue(wave);
            }
            StartNextWave();
            printWave += 1;
        }

        //Starts the next wave
        private void StartNextWave()
        {
            if (waves.Count > 0) // If there are still waves left
            {
                waves.Peek().Start(); // Start the next one

                timeSinceLastWave = 0; // Reset timer
                waveFinished = false;
            }
        }

        //Updates the WaveManager class, updates the currentwave, checks to see if the wave is over
        //Checks to see how long it's been since the last wave ended if there is no currentwave.|
        //Starts the next wave if enough time has elapsed.
        public void Update(GameTime gameTime)
        {
            if (this.WavesLeft > 0)
            {
                CurrentWave.Update(gameTime); // Update the wave
                if (CurrentWave.RoundOver) // Check if it has finished
                {
                    waveFinished = true;
                    
                }

                if (waveFinished) // If it has finished
                {
                    timeSinceLastWave += (float)gameTime.ElapsedGameTime.TotalSeconds; // Start the timer
                }

                if (timeSinceLastWave > 3.0f) // If 3 seconds have passed
                {
                    waves.Dequeue(); // Remove the finished wave
                    printWave += 1;
                    StartNextWave(); // Start the next wave
                }
            }
            else
            {
                this.levelFinished = true;
                
            }
        }

        //tells the current wave to draw its enemies.
        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.WavesLeft > 0)
            {
                CurrentWave.Draw(spriteBatch);
            }
        }



        
    }
}
