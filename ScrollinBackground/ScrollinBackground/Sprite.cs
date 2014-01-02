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
    class Sprite
    {
        public Texture2D texture;
        public Rectangle rectangle;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
    class Player : Sprite
    {
        public Texture2D railTexture;
        public Texture2D standardTexture;
        public Player(Texture2D StandardTexture, Texture2D RailTexture, Rectangle Rectangle)
        {
            texture = StandardTexture;
            standardTexture = StandardTexture;
            railTexture = RailTexture;
            rectangle = Rectangle;
        }
    }
    class Box : Sprite
    {
        public Box(Texture2D Texture, Rectangle Rectangle)
        {
            texture = Texture;
            rectangle = Rectangle;
        }
    }
    class Rail : Sprite
    {
        public Rail(Texture2D Texture, Rectangle Rectangle)
        {
            texture = Texture;
            rectangle = Rectangle;
        }
    }
}
