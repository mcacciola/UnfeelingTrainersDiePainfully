using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UTDP.GUI
{
    class Toolbar
    {
        // The font of the toolbar
        private SpriteFont font;

        private WaveManager waveManager;


        // The position of the toolbar
        private Vector2 position;

        private int level;
        private int wave; 
        //Constructor
        public Toolbar(SpriteFont font, WaveManager waveManager, Vector2 position, int level)
        {
            this.font = font;
            this.position = position;
            this.waveManager = waveManager;
            this.level = level;
            this.wave = 0;
        }

        //Draws the toolbar
        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            string text = string.Format("Money : {0} Lives : {1}", player.Money, player.Lives);
            Vector2 textMeasure = font.MeasureString(text);
            Vector2 textPosition = new Vector2(315 - textMeasure.X, 332);
            spriteBatch.DrawString(font, text, textPosition, Color.White);
            /*if (wave < 10)
            {
                wave = waveManager.CurrentWave.RoundNumber + 1;
            }
            else
            {
                wave = 0;
            }*/
            String waveInfo = string.Format("Level : {0} Wave : {1}", level, waveManager.printWave);
            Vector2 infoMeasure = font.MeasureString(waveInfo);
            Vector2 infoPosition = new Vector2(315 - infoMeasure.X, 332 + infoMeasure.Y);
            spriteBatch.DrawString(font, waveInfo, infoPosition, Color.White);
        }
    }
}
