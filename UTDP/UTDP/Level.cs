using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UTDP
{
    class Level
    {
        //An array to determine the path used by enemies.
        int[,] map;

        //Waypoints for enemies to follow
        private Queue<Vector2> waypoints = new Queue<Vector2>();

        //List of Textures to use in drawing the Level
        public List<Texture2D> tileTextures = new List<Texture2D>();

        public List<Texture2D> grassTextures = new List<Texture2D>();

        //Adds textures to our list.
        public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }

        //Generate Grass textures.
        public void GrassTextures(Texture2D texture)
        {
            grassTextures.Add(texture);
        }

        //Read-Only property returning the Width of the map.
        public int Width
        {
            get { return map.GetLength(1); }
        }

        //Read-Only property returning the height of the map.
        public int Height
        {
            get { return map.GetLength(0); }
        }

        //Public read-only property for waypoints.
        public Queue<Vector2> Waypoints
        {
            get { return waypoints; }
        }

        //Constructor initializes the waypoints for the level.
        public Level(int[,] newMap, Queue<Vector2> newPoints)
        {
            map = newMap;
            waypoints = newPoints;
        }

        //Draws the map.
        public void Draw(SpriteBatch batch)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int textureIndex = map[y, x];
                    if (textureIndex == -1)
                        continue;
                    /*if (textureIndex == 0)
                    {
                        Random rand = new Random();
                        int grassIndex = rand.Next(3);

                        Texture2D gtexture = grassTextures[grassIndex];
                        batch.Draw(gtexture, new Rectangle(
                        x * 32, y * 32, 32, 32), Color.White);


                    }*/
                    
                    
                        Texture2D texture = tileTextures[textureIndex];
                        batch.Draw(texture, new Rectangle(
                            x * 32, y * 32, 32, 32), Color.White);
                    
                }
            }
        }

        //Returns what the map has at the given location.
        public int GetIndex(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
            { 
                return 0; 
            }

            return map[cellY, cellX];
        }

    }
}
