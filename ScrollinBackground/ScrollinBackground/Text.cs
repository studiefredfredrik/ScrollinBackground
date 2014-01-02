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
    class Text
    {
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        SpriteFont Font1;
        Color fontColor = Color.Black;

        // display text message
        public Text(GraphicsDeviceManager Graphics, SpriteFont Font)
        {
            spriteBatch = new SpriteBatch(Graphics.GraphicsDevice);
            graphics = Graphics;
            Font1 = Font;
        }

        public Color FontColor
        {
            get { return fontColor; }
            set { fontColor = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 5,
                    graphics.GraphicsDevice.Viewport.Height / 5);
            string text = "Skate game v0.11\nPress ESC to escape..."; 
            // Find the center of the string
            Vector2 FontOrigin = Font1.MeasureString(text) / 2;
            // Draw the string
            spriteBatch.DrawString(Font1, text, FontPos, fontColor,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

        public void Draw(SpriteBatch spriteBatch, string text, Vector2 FontPos)
        {
            // Find the center of the string
            Vector2 FontOrigin = Font1.MeasureString(text) / 2;
            // Draw the string
            spriteBatch.DrawString(Font1, text, FontPos, fontColor,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }
        public void Draw(SpriteBatch spriteBatch, string text)
        {
            // Find the center of the string
            Vector2 FontOrigin = Font1.MeasureString(text) / 2;
            // Draw the string
            spriteBatch.DrawString(Font1, text, new Vector2(80f,20f), fontColor,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
}
