using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ScrollinBackground
{
    class Physics
    {
        int screenHeight;
        int screenWidth; 
        TimeSpan lastCollision;
        public Physics(int ScreenHeight, int ScreenWidth)
        {
            screenHeight = ScreenHeight;
            screenWidth = ScreenWidth;
        }

        public void Player(ref Player player, KeyboardState keyboardState)
        {
            // user input
            if (keyboardState.IsKeyDown(Keys.Right))
                player.rectangle.X += 3;
            if (keyboardState.IsKeyDown(Keys.Left))
                player.rectangle.X += -3;
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (player.rectangle.Y >= screenHeight - player.rectangle.Height - 35)
                    player.rectangle.Y -= 65;
            }
            if(keyboardState.IsKeyDown(Keys.Up))
            {
                if (player.rectangle.Y <= screenHeight - player.rectangle.Height - 35)
                    player.texture = player.railTexture;
            }

            // falling
            player.rectangle.Y += 3;
            if (player.rectangle.Y >= screenHeight - player.rectangle.Height - 35)
            {
                player.rectangle.Y = screenHeight - player.rectangle.Height - 35;
                player.texture = player.standardTexture;
            }

            // right/left edges
            if (player.rectangle.X >= 800 - player.rectangle.Width)
                player.rectangle.X = 800 - player.rectangle.Width;
            if (player.rectangle.X <= 0 + player.rectangle.Width)
                player.rectangle.X = 0 + player.rectangle.Width;
        }

        public List<Sprite> Sprite(List<Sprite> spriteList)
        {
            int objectsOnScreen = 0;

            foreach (Sprite s in spriteList)
            {
                // move
                s.rectangle.X -= 3;
                // check if outside screen
                if (s.rectangle.X >= 0 && s.rectangle.X <= 800)
                    objectsOnScreen++;
            }
            if (objectsOnScreen < 1)
            {
                // if screen is empty add a random object
                Random random = new Random();
                spriteList[random.Next(0, spriteList.Count)].rectangle.X = 800;
            }

            return spriteList;
        }

        public int[] Intersect(ref Player player, List<Sprite> spriteList,
            SoundEffect ouchSound, TimeSpan gameTime, ref int[] gameValues)
        {
            // game values: lives left, rail points, sheep-slam's
            foreach (Sprite s in spriteList)
            {
                if (s is Box)
                {
                    if (s.rectangle.Intersects(player.rectangle))
                    {
                        if (gameTime - lastCollision >= TimeSpan.FromMilliseconds(400))
                        {
                            lastCollision = gameTime;
                            ouchSound.Play();
                            gameValues[0]--;
                            player.rectangle.Y -= 35;
                        }
                    }
                }
                if (s is Rail)
                {
                    if (s.rectangle.Intersects(player.rectangle))
                    {
                        if (gameTime - lastCollision >= TimeSpan.FromMilliseconds(400) &&
                            player.texture!=player.railTexture)
                        {
                            lastCollision = gameTime;
                            ouchSound.Play();
                            gameValues[0]--;
                            player.rectangle.Y -= 35;
                        }
                        if (player.texture == player.railTexture)
                        {
                            player.rectangle.Y -= 3;
                            gameValues[1]++;
                        }
                    }
                }
            }

            return gameValues;
        }
    }
}
