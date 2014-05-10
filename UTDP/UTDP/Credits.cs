using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UTDP
{
    class Credits
    {
        public static void Draw(SpriteBatch spriteBatch, int score, SpriteFont font)
        {
            string zerothLine = "Unfeeling Trainers Die Painfully";
            string firstLine = "A Hawk's Eye Production";
            string secondLine = "In Association with Night Dragon Studios";
            string thirdLine = String.Format("You scored: {0}!  Good job!", score);
            string fourthLine = "And Nurse Joy is very happy with you!";

            float screenWidth = 160;
            Vector2 zerothMeasure = font.MeasureString(zerothLine);
            Vector2 firstMeasure = font.MeasureString(firstLine);
            Vector2 secondMeasure = font.MeasureString(secondLine);
            Vector2 thirdMeasure = font.MeasureString(thirdLine);
            Vector2 fourthMeasure = font.MeasureString(fourthLine);

            spriteBatch.DrawString(font, zerothLine, new Vector2(screenWidth - (zerothMeasure.X / 2), 10f), Color.White);
            spriteBatch.DrawString(font, firstLine, new Vector2(screenWidth - (firstMeasure.X / 2), 35f), Color.White);
            spriteBatch.DrawString(font, secondLine, new Vector2(screenWidth - (secondMeasure.X / 2), 65f), Color.White);
            spriteBatch.DrawString(font, thirdLine, new Vector2(screenWidth - (thirdMeasure.X / 2), 280f), Color.White);
            spriteBatch.DrawString(font, fourthLine, new Vector2(screenWidth - (fourthMeasure.X / 2), 300f), Color.White);
        }

        public static void DrawGameOver(SpriteBatch spriteBatch, int score, SpriteFont font)
        {
            string zerothLine = "Unfeeling Trainers Die Painfully";
            string firstLine = "A Hawk's Eye Production";
            string secondLine = "In Association with Night Dragon Studios";
            string thirdLine = String.Format("You scored: {0}, but it wasn't enough!", score);
            string fourthLine = "And Nurse Joy is very upset with you!";

            float screenWidth = 160;
            Vector2 zerothMeasure = font.MeasureString(zerothLine);
            Vector2 firstMeasure = font.MeasureString(firstLine);
            Vector2 secondMeasure = font.MeasureString(secondLine);
            Vector2 thirdMeasure = font.MeasureString(thirdLine);
            Vector2 fourthMeasure = font.MeasureString(fourthLine);

            spriteBatch.DrawString(font, zerothLine, new Vector2(screenWidth - (zerothMeasure.X / 2), 10f), Color.White);
            spriteBatch.DrawString(font, firstLine, new Vector2(screenWidth - (firstMeasure.X / 2), 35f), Color.White);
            spriteBatch.DrawString(font, secondLine, new Vector2(screenWidth - (secondMeasure.X / 2), 65f), Color.White);
            spriteBatch.DrawString(font, thirdLine, new Vector2(screenWidth - (thirdMeasure.X / 2), 280f), Color.White);
            spriteBatch.DrawString(font, fourthLine, new Vector2(screenWidth - (fourthMeasure.X / 2), 300f), Color.White);
        }
    }
}
