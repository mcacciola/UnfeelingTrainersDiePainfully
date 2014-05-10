using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using UTDP.GUI;

namespace UTDP
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameLoop : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level;
        WaveManager waveManager;
        Player player;
        Toolbar toolBar;
        Button charButton;
        Button piButton;
        Button treeButton;
        Button squirtButton;
        Button SelectedButton;
        bool firstLevel = true;
        bool secondLevel = false;
        bool showCredits = false;
        bool theGameIsOver = false;
        Level level2;
        Level level3;
        Texture2D[] enemyTextures;
        Texture2D[][] towerTextures;
        Texture2D[] bulletTextures;
        SpriteFont font;
        SoundEffect[] bulletSounds;
        Song gameOver;
        Song victory;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        Music gameMusic;


        

        public GameLoop()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 10 * 32;
            graphics.PreferredBackBufferHeight = 47 + 10 * 32;
            graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameMusic = new Music();
            gameMusic.AddSong(Content.Load<Song>("Sounds\\Music"));
            gameMusic.AddSong(Content.Load<Song>("Sounds\\Anthem"));
            gameMusic.AddSong(Content.Load<Song>("Sounds\\Effect"));
            gameMusic.AddSong(Content.Load<Song>("Sounds\\Hikari"));


            gameOver = Content.Load<Song>("Sounds\\GameOver");
            victory = Content.Load<Song>("Sounds\\Victory");

            int[,] firstMap = CreateFirstLevelMap();
            Queue<Vector2> firstPoints = CreateFirstLevelWaypoints();
            level = new Level(firstMap, firstPoints);
            Texture2D grass = Content.Load<Texture2D>("Level\\grass");
            //Texture2D grass2 = Content.Load<Texture2D>("Level\\grass2");
            //Texture2D grass3 = Content.Load<Texture2D>("Level\\grass3");
            Texture2D path = Content.Load<Texture2D>("Level\\path");
            Texture2D treeTop = Content.Load<Texture2D>("Level\\treetop");
            Texture2D treeBot = Content.Load<Texture2D>("Level\\treebottom");
            Texture2D house1 = Content.Load<Texture2D>("Level\\House\\house1");
            Texture2D house2 = Content.Load<Texture2D>("Level\\House\\house2");
            Texture2D house3 = Content.Load<Texture2D>("Level\\House\\house3");
            Texture2D house4 = Content.Load<Texture2D>("Level\\House\\house4");

            //level.GrassTextures(grass);
            //level.GrassTextures(grass2);
            //level.GrassTextures(grass3);

            level.AddTexture(grass);//0
            level.AddTexture(path);//1
            level.AddTexture(treeTop);//2
            level.AddTexture(treeBot);//3
            level.AddTexture(house1);//4
            level.AddTexture(house2);//5
            level.AddTexture(house3);//6
            level.AddTexture(house4);//7

            int[,] secondMap = CreateSecondLevelMap();
            Queue<Vector2> secondPoints = CreateSecondLevelWaypoints();
            level2 = new Level(secondMap, secondPoints);
            level2.AddTexture(grass);
            level2.AddTexture(path);
            level2.AddTexture(treeTop);
            level2.AddTexture(treeBot);

            int[,] thirdMap = CreateThirdLevelMap();
            Queue<Vector2> thirdPoints = CreateThirdLevelWaypoints();
            level3 = new Level(thirdMap, thirdPoints);
            Texture2D pc1 = Content.Load<Texture2D>("Level\\PokeCenter\\PC1");
            Texture2D pc2 = Content.Load<Texture2D>("Level\\PokeCenter\\PC2");
            Texture2D pc3 = Content.Load<Texture2D>("Level\\PokeCenter\\PC3");
            Texture2D pc4 = Content.Load<Texture2D>("Level\\PokeCenter\\PC4");
            level3.AddTexture(grass);
            level3.AddTexture(path);
            level3.AddTexture(treeTop);
            level3.AddTexture(treeBot);
            level3.AddTexture(house1);//4
            level3.AddTexture(house2);//5
            level3.AddTexture(house3);//6
            level3.AddTexture(house4);//7
            level3.AddTexture(pc1);//8
            level3.AddTexture(pc2);//9
            level3.AddTexture(pc3);//10
            level3.AddTexture(pc4);//11

            enemyTextures = new Texture2D[]
            {
                Content.Load<Texture2D>("children\\kid1_strip16"),
                Content.Load<Texture2D>("children\\kid2_strip16"),
                Content.Load<Texture2D>("children\\kid3_strip16"),
                Content.Load<Texture2D>("children\\kid4_strip16"),
                Content.Load<Texture2D>("children\\kid5_strip16"),
                Content.Load<Texture2D>("children\\kid6_strip16")
            };

            //Texture2D towerTexture = Content.Load<Texture2D>("arrowtower");
            Texture2D[] charTowers = new Texture2D[]
            {
                Content.Load<Texture2D>("Charmander Tower\\CharmanderTower"),
                Content.Load<Texture2D>("Charmander Tower\\CharizardTower_strip16")
            };

            Texture2D[] pikTowers = new Texture2D[]
            {
                Content.Load<Texture2D>("Pikachu Tower\\PichuTower_strip16"),
                Content.Load<Texture2D>("Pikachu Tower\\PikachuTower_strip16")

            };
            Texture2D[] treeTowers = new Texture2D[]
            {
                Content.Load<Texture2D>("Treeko Tower\\TreekoStrip_strip16"),
                Content.Load<Texture2D>("Treeko Tower\\GrovyleTower_strip16")
            };

            Texture2D[] squirtTowers = new Texture2D[]
            {
                Content.Load<Texture2D>("Squirtle Tower\\SquirtleTower_strip16"),
                Content.Load<Texture2D>("Squirtle Tower\\BlastoiseTower_strip16")
            };
            towerTextures = new Texture2D[][]
            {
                charTowers, pikTowers, treeTowers, squirtTowers
            };
            bulletTextures = new Texture2D[]
            {
                Content.Load<Texture2D>("Charmander Tower\\CharmanderStaticBullet"),
                Content.Load<Texture2D>("Pikachu Tower\\PikachuStaticBullet"),
                Content.Load<Texture2D>("Treeko Tower\\TreekoStaticBullet"),
                Content.Load<Texture2D>("Squirtle Tower\\SquirtleStaticBullet"),
            };

            bulletSounds = new SoundEffect[]
            {
                Content.Load<SoundEffect>("Sounds\\fireball"),
                Content.Load<SoundEffect>("Sounds\\zap"),
                Content.Load<SoundEffect>("Sounds\\grass"),
                Content.Load<SoundEffect>("Sounds\\splash")
            };

            player = new Player(level, towerTextures, bulletTextures, bulletSounds);

            //enemyTexture = Content.Load<Texture2D>("enemy");
            waveManager = new WaveManager(level, 10, enemyTextures, player, 2, 1);

            font = Content.Load<SpriteFont>("Arial");

            toolBar = new Toolbar(font, waveManager, new Vector2(0, level.Height * 32), 1);

            // The "Normal" texture for the arrow button.
            Texture2D charNormal = Content.Load<Texture2D>("Charmander Tower\\CharmanderButtonNormal");
            // The "MouseOver" texture for the arrow button.
            Texture2D charHover = Content.Load<Texture2D>("Charmander Tower\\CharmanderButtonHover");
            // The "Pressed" texture for the arrow button.
            Texture2D charPressed = Content.Load<Texture2D>("Charmander Tower\\CharmanderButtonPressed");
          
            
            // Initialize the arrow button.
            charButton = new Button(charNormal, charHover,
             charPressed, new Vector2(0, level.Height * 32), new CharmanderTower(towerTextures[0],
                            bulletTextures[0], bulletSounds[0], new Vector2(0, 0), false), font);

            charButton.Clicked += new EventHandler(charButton_Clicked);

            // The "Normal" texture for the arrow button.
            Texture2D piNormal = Content.Load<Texture2D>("Pikachu Tower\\PichuButtonNormal");
            // The "MouseOver" texture for the arrow button.
            Texture2D piHover = Content.Load<Texture2D>("Pikachu Tower\\PichuButtonHover");
            // The "Pressed" texture for the arrow button.
            Texture2D piPressed = Content.Load<Texture2D>("Pikachu Tower\\PichuButtonPressed");


            // Initialize the arrow button.
            piButton = new Button(piNormal, piHover,
             piPressed, new Vector2(32, level.Height * 32), new PichuTower(towerTextures[0],
                            bulletTextures[0], bulletSounds[0], new Vector2(0, 0), false), font);

            piButton.Clicked += new EventHandler(piButton_Clicked);

            // The "Normal" texture for the arrow button.
            Texture2D treeNormal = Content.Load<Texture2D>("Treeko Tower\\TreekoNormal");
            // The "MouseOver" texture for the arrow button.
            Texture2D treeHover = Content.Load<Texture2D>("Treeko Tower\\TreekoHover");
            // The "Pressed" texture for the arrow button.
            Texture2D treePressed = Content.Load<Texture2D>("Treeko Tower\\TreekoClicked");


            // Initialize the arrow button.
            treeButton = new Button(treeNormal, treeHover,
             treePressed, new Vector2(64, level.Height * 32), new TreekoTower(towerTextures[0],
                            bulletTextures[0], bulletSounds[0], new Vector2(0, 0), false), font);

            treeButton.Clicked += new EventHandler(treeButton_Clicked);

            // The "Normal" texture for the arrow button.
            Texture2D squirtNormal = Content.Load<Texture2D>("Squirtle Tower\\SquirtleNormal");
            // The "MouseOver" texture for the arrow button.
            Texture2D squirtHover = Content.Load<Texture2D>("Squirtle Tower\\SquirtleHover");
            // The "Pressed" texture for the arrow button.
            Texture2D squirtPressed = Content.Load<Texture2D>("Squirtle Tower\\SquirtleClicked");


            // Initialize the arrow button.
            squirtButton = new Button(squirtNormal, squirtHover,
             squirtPressed, new Vector2(96, level.Height * 32), new SquirtleTower(towerTextures[0],
                            bulletTextures[0], bulletSounds[0], new Vector2(0, 0), false), font);

            squirtButton.Clicked += new EventHandler(squirtButton_Clicked);
        }

        private static int[,] CreateFirstLevelMap()
        {
            int[,] firstMap = new int[,] 
        {
            {4,5,1,0,0,4,5,0,0,0},
            {7,6,1,1,0,7,6,2,0,0},
            {0,0,0,1,1,0,2,3,0,0},
            {0,0,0,0,1,0,3,0,0,0},
            {0,0,0,1,1,0,0,0,0,0},
            {0,0,1,1,0,0,0,0,0,2},
            {0,0,1,0,0,0,0,0,0,3},
            {0,0,1,1,1,1,1,1,1,1},
            {2,0,0,0,0,0,0,0,0,0},
            {3,0,0,0,0,0,0,0,0,0}
        };
            return firstMap;
        }
        private static int[,] CreateSecondLevelMap()
        {
            int[,] firstMap = new int[,] 
        {
            {2,3,2,3,0,0,3,3,3,3},
            {3,2,3,2,0,0,0,0,0,0},
            {0,3,2,3,0,1,1,1,1,1},
            {0,0,3,0,0,1,0,0,0,0},
            {0,0,0,0,0,1,0,0,0,0},
            {0,0,0,0,0,1,1,0,2,2},
            {0,0,1,1,1,0,1,0,3,3},
            {1,1,1,0,1,0,1,0,2,2},
            {0,0,0,0,1,1,1,0,3,3},
            {0,2,2,2,0,0,0,0,2,2}
        };
            return firstMap;
        }

        private static int[,] CreateThirdLevelMap()
        {
            int[,] firstMap = new int[,] 
        {
           //0 1 2 3 4 5 6 7 8 9 
            {3,3,0,0,0,0,0,0,4,5},//0
            {0,0,0,1,1,1,0,0,7,6},//1
            {1,1,1,1,0,1,0,0,0,0},//2
            {0,0,0,0,0,1,0,0,8,9},//3
            {2,0,0,1,1,1,0,0,11,10},//4
            {3,0,0,1,0,0,0,1,1,1},//5
            {0,0,0,1,1,0,0,1,0,0},//6
            {0,0,4,5,1,0,0,1,4,5},//7
            {2,0,7,6,1,1,1,1,7,6},//8
            {3,2,2,0,0,0,0,0,2,2} //9
        };
            return firstMap;
        }

        private static Queue<Vector2> CreateFirstLevelWaypoints()
        {
            Queue<Vector2> firstPoints = new Queue<Vector2>();
            firstPoints.Enqueue(new Vector2(2, 0) * 32);
            firstPoints.Enqueue(new Vector2(2, 1) * 32);
            firstPoints.Enqueue(new Vector2(3, 1) * 32);
            firstPoints.Enqueue(new Vector2(3, 2) * 32);
            firstPoints.Enqueue(new Vector2(4, 2) * 32);
            firstPoints.Enqueue(new Vector2(4, 4) * 32);
            firstPoints.Enqueue(new Vector2(3, 4) * 32);
            firstPoints.Enqueue(new Vector2(3, 5) * 32);
            firstPoints.Enqueue(new Vector2(2, 5) * 32);
            firstPoints.Enqueue(new Vector2(2, 7) * 32);
            firstPoints.Enqueue(new Vector2(7, 7) * 32);
            firstPoints.Enqueue(new Vector2(8, 7) * 32);
            firstPoints.Enqueue(new Vector2(9, 7) * 32);
            return firstPoints;
        }

        private static Queue<Vector2> CreateSecondLevelWaypoints()
        {
            Queue<Vector2> secondPoints = new Queue<Vector2>();
            secondPoints.Enqueue(new Vector2(0, 7) * 32);
            secondPoints.Enqueue(new Vector2(1, 7) * 32);
            secondPoints.Enqueue(new Vector2(2, 7) * 32);
            secondPoints.Enqueue(new Vector2(2, 6) * 32);
            secondPoints.Enqueue(new Vector2(3, 6) * 32);
            secondPoints.Enqueue(new Vector2(4, 6) * 32);
            secondPoints.Enqueue(new Vector2(4, 7) * 32);
            secondPoints.Enqueue(new Vector2(4, 8) * 32);
            secondPoints.Enqueue(new Vector2(5, 8) * 32);
            secondPoints.Enqueue(new Vector2(6, 8) * 32);
            secondPoints.Enqueue(new Vector2(6, 7) * 32);
            secondPoints.Enqueue(new Vector2(6, 6) * 32);
            secondPoints.Enqueue(new Vector2(6, 5) * 32);
            secondPoints.Enqueue(new Vector2(5, 5) * 32);
            secondPoints.Enqueue(new Vector2(5, 4) * 32);
            secondPoints.Enqueue(new Vector2(5, 3) * 32);
            secondPoints.Enqueue(new Vector2(5, 2) * 32);
            secondPoints.Enqueue(new Vector2(6, 2) * 32);
            secondPoints.Enqueue(new Vector2(7, 2) * 32);
            secondPoints.Enqueue(new Vector2(8, 2) * 32);
            secondPoints.Enqueue(new Vector2(9, 2) * 32);
            return secondPoints;
        }

        private static Queue<Vector2> CreateThirdLevelWaypoints()
        {
            Queue<Vector2> thirdPoints = new Queue<Vector2>();
            thirdPoints.Enqueue(new Vector2(0, 2) * 32);
            thirdPoints.Enqueue(new Vector2(1, 2) * 32);
            thirdPoints.Enqueue(new Vector2(2, 2) * 32);
            thirdPoints.Enqueue(new Vector2(3, 2) * 32);
            thirdPoints.Enqueue(new Vector2(3, 1) * 32);
            thirdPoints.Enqueue(new Vector2(4, 1) * 32);
            thirdPoints.Enqueue(new Vector2(5, 1) * 32);
            thirdPoints.Enqueue(new Vector2(5, 2) * 32);
            thirdPoints.Enqueue(new Vector2(5, 3) * 32);
            thirdPoints.Enqueue(new Vector2(5, 4) * 32);
            thirdPoints.Enqueue(new Vector2(4, 4) * 32);
            thirdPoints.Enqueue(new Vector2(3, 4) * 32);
            thirdPoints.Enqueue(new Vector2(3, 5) * 32);
            thirdPoints.Enqueue(new Vector2(3, 6) * 32);
            thirdPoints.Enqueue(new Vector2(4, 6) * 32);
            thirdPoints.Enqueue(new Vector2(4, 7) * 32);
            thirdPoints.Enqueue(new Vector2(4, 8) * 32);
            thirdPoints.Enqueue(new Vector2(5, 8) * 32);
            thirdPoints.Enqueue(new Vector2(6, 8) * 32);
            thirdPoints.Enqueue(new Vector2(7, 8) * 32);
            thirdPoints.Enqueue(new Vector2(7, 7) * 32);
            thirdPoints.Enqueue(new Vector2(7, 6) * 32);
            thirdPoints.Enqueue(new Vector2(7, 5) * 32);
            thirdPoints.Enqueue(new Vector2(8, 5) * 32);
            thirdPoints.Enqueue(new Vector2(9, 5) * 32);


            return thirdPoints;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            gameMusic.Play();
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyUp(Keys.M) && oldKeyboardState.IsKeyDown(Keys.M))
            {
                if (gameMusic.IsPlaying)
                {
                    gameMusic.Pause();
                }
                else
                {
                    gameMusic.Resume();
                }
            }
            if (!showCredits && player.Lives > 0)
            {
                waveManager.Update(gameTime);
                if (waveManager.WavesLeft > 0)
                {
                    player.Update(gameTime, waveManager.Enemies);
                }
                charButton.Update(gameTime);
                piButton.Update(gameTime);
                treeButton.Update(gameTime);
                squirtButton.Update(gameTime);
                if (waveManager.LevelFinished && firstLevel)
                {
                    player.ClearTowers();
                    player.Money += 100;
                    player.Lives += 10;
                    level = level2;
                    player.CurrentLevel = level;
                    waveManager = new WaveManager(level, 10, enemyTextures, player, 4, 1.5f);
                    
                    toolBar = new Toolbar(font, waveManager, new Vector2(0, level.Height * 32), 2);
                    firstLevel = false;
                    secondLevel = true;
                }
                else if (waveManager.LevelFinished && secondLevel)
                {
                    player.ClearTowers();
                    player.Money += 100;
                    player.Lives += 10;
                    level = level3;
                    player.CurrentLevel = level;
                    waveManager = new WaveManager(level, 10, enemyTextures, player, 4, 1.5f);
                    toolBar = new Toolbar(font, waveManager, new Vector2(0, level.Height * 32), 3);
                    secondLevel = false;

                }
                else if (waveManager.LevelFinished && !firstLevel && !secondLevel)
                {
                    player.Score += (player.Lives * 15);
                    showCredits = true;
                }
            }
            if (showCredits && !theGameIsOver)
            {
                PlayMusic(victory);
                theGameIsOver = true;
            }
            if (player.Lives <= 0 && !theGameIsOver)
            {
                PlayMusic(gameOver);
                theGameIsOver = true;
            }
            oldKeyboardState = keyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            //Begin drawing here
            if (!showCredits && player.Lives > 0)
            {
                level.Draw(spriteBatch);

                waveManager.Draw(spriteBatch);

                player.Draw(spriteBatch);

                toolBar.Draw(spriteBatch, player);
                charButton.Draw(spriteBatch);
                piButton.Draw(spriteBatch);
                treeButton.Draw(spriteBatch);
                squirtButton.Draw(spriteBatch);
            }
            else if (player.Lives > 0)
            {
                Credits.Draw(spriteBatch, player.Score, font);
            }
            else if (player.Lives <= 0)
            {
                Credits.DrawGameOver(spriteBatch, player.Score, font);
            }
            //End all drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void charButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Charmander Tower";
            SetSelectedButton(charButton);
        }

        private void piButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Pichu Tower";
            SetSelectedButton(piButton);
        }

        private void treeButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Treeko Tower";
            SetSelectedButton(treeButton);
        }

        private void squirtButton_Clicked(object sender, EventArgs e)
        {
            player.NewTowerType = "Squirtle Tower";
            SetSelectedButton(squirtButton);
        }

        private void SetSelectedButton(Button newSelectedButton)
        {
            if (this.SelectedButton != null)
            {
                this.SelectedButton.SetNormal();
            }
            this.SelectedButton = newSelectedButton;
            this.SelectedButton.SetSelected();
        }

        private void PlayMusic(Song song)
        {
            // Due to the way the MediaPlayer plays music,
            // we have to catch the exception. Music will play when the game is not tethered
            try
            {
                gameMusic.Stop();
                MediaPlayer.Play(song);
            }
            catch { }
        }
    }

}
