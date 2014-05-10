using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace UTDP
{
    class Player
    {

        private bool soundOn = true;

        //The money the player has to buy towers
        private int money = 100;

        //The number of lives the player has
        private int lives = 15;

        //The score of the player
        private int score = 0;

        //The towers the player has
        private List<Tower> towers = new List<Tower>();

        private MouseState mouseState; // Mouse state for the current frame
        private MouseState oldState; // Mouse state for the previous frame

        private KeyboardState keyboardState;
        private KeyboardState oldKeyState;

        //The cell location
        private int cellX;
        private int cellY;

        //The tile location
        private int tileX;
        private int tileY;

        //The image of the tower.
        private Texture2D[][] towerTextures;

        //The image of the bullet
        private Texture2D[] bulletTextures;


        //The level the player is on.
        private Level level;

        //The Property of the level.
        public Level CurrentLevel
        {
            get {return this.level;}
            set {this.level = value;}
        }

        //The amount of the money the player has
        public int Money
        {
            get { return money; }
            set { money = value; }
        }
        //The amount of Lives the player has
        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }


        // The type of tower to add.
        private string newTowerType;

        public string NewTowerType
        {
            set { newTowerType = value; }
        }

        private SoundEffect[] bulletSounds;

        //Player constructor, instantiates the level and current tower/bullet textures.
        public Player(Level level, Texture2D[][] towerTextures, Texture2D[] bulletTextures, SoundEffect[] sounds)
        {
            this.level = level;

            this.towerTextures = towerTextures;
            this.bulletTextures = bulletTextures;
            this.bulletSounds = sounds;
        }


        //The update method for the player, keeps track of mouse location
        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            mouseState = Mouse.GetState();

            keyboardState = Keyboard.GetState();

            cellX = (int)(mouseState.X / 32); // Convert the position of the mouse
            cellY = (int)(mouseState.Y / 32); // from array space to level space

            tileX = cellX * 32; // Convert from array space to level space
            tileY = cellY * 32; // Convert from array space to level space

            if (keyboardState.IsKeyUp(Keys.S) && oldKeyState.IsKeyDown(Keys.S))
            {
                soundOn = !soundOn;
                foreach (Tower tower in towers)
                {
                    tower.SoundOn = this.soundOn;
                }
            }

            if (mouseState.LeftButton == ButtonState.Released
                && oldState.LeftButton == ButtonState.Pressed)
            {
                if (string.IsNullOrEmpty(newTowerType) == false)
                {
                    AddTower();
                }
            }

            if (mouseState.RightButton == ButtonState.Released
                && oldState.RightButton == ButtonState.Pressed)
            {
                UpgradeTower();
            }

            foreach (Tower tower in towers)
            {
                if (tower.Target == null)
                {
                    tower.GetClosestEnemy(enemies);
                }

                tower.Update(gameTime);
            }

            oldState = mouseState; // Set the oldState so it becomes the state of the previous frame.
            oldKeyState = keyboardState;
        }

        //checks to see if the position is clear.
        private bool IsCellClear()
        {
            bool inBounds = cellX >= 0 && cellY >= 0 && // Make sure tower is within limits
                cellX < level.Width && cellY < level.Height;

            bool spaceClear = true;

            foreach (Tower tower in towers) // Check that there is no tower here
            {
                spaceClear = (tower.Position != new Vector2(tileX, tileY));

                if (!spaceClear)
                    break;
            }

            bool onPath = (level.GetIndex(cellX, cellY) == 0);

            return inBounds && spaceClear && onPath; // If both checks are true return true
        }
        
        //Draws all towers owned by player.
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in towers)
            {
                tower.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Adds a tower to the player's collection.
        /// </summary>
        public void AddTower()
        {
            Tower towerToAdd = null;

            switch (newTowerType)
            {
                case "Charmander Tower":
                    {
                        towerToAdd = new CharmanderTower(towerTextures[0],
                            bulletTextures[0], bulletSounds[0], new Vector2(tileX, tileY), soundOn);
                        break;
                    }
                case "Pichu Tower":
                    {
                        towerToAdd = new PichuTower(towerTextures[1],
                            bulletTextures[1], bulletSounds[1], new Vector2(tileX, tileY), soundOn);
                        break;
                    }
                case "Treeko Tower":
                    {
                        towerToAdd = new TreekoTower(towerTextures[2],
                            bulletTextures[2], bulletSounds[2], new Vector2(tileX, tileY), soundOn);
                        break;
                    }
                case "Squirtle Tower":
                    {
                        towerToAdd = new SquirtleTower(towerTextures[3],
                            bulletTextures[3], bulletSounds[3], new Vector2(tileX, tileY), soundOn);
                        break;
                    }
            }

            // Only add the tower if there is a space and if the player can afford it.
            if (towerToAdd != null)
            {
                if (IsCellClear() == true && towerToAdd.Cost <= money)
                {
                    towers.Add(towerToAdd);
                    money -= towerToAdd.Cost;
                }
            }
        }

        public void UpgradeTower()
        {
            if (!IsCellClear())
            {
                foreach (Tower tower in towers)
                {
                    if (tower.Position == new Vector2(tileX, tileY) && this.money >= tower.UpgradeCost)
                    {
                        tower.Upgrade();
                        this.money -= tower.UpgradeCost;
                        break;
                    }
                }
            }
        }
        public void ClearTowers()
        {
            towers = new List<Tower>();
        }
    }
}
