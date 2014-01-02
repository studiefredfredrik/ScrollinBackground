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

namespace ScrollinBackground
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Scrolling scrolling1;
        Scrolling scrolling2;

        Player player;
        Physics physics;

        // items
        List<Sprite> spriteList = new List<Sprite>();

        // sound effexz
        SoundEffect ouchSound;

        // text
        Text text;
        SpriteFont Font1;
        int endscore = 0;
        int[] gameValues = {5,0,0}; // lives left, rail points, sheep-slam's

        int screenHeight;
        int screenWidth;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // text
            Font1 = Content.Load<SpriteFont>("SpriteFont1");
            text = new Text(graphics, Font1);
            // statics
            screenHeight = GraphicsDevice.Viewport.Height;
            screenWidth = GraphicsDevice.Viewport.Width;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //backgrounds
            scrolling1 = new Scrolling(Content.Load<Texture2D>("Backgrounds/background1"), new Rectangle(0, 0, 800, 500));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("Backgrounds/background2"), new Rectangle(800, 0, 800, 500));
            // player
            player = new Player(Content.Load<Texture2D>("Player/playerBoardBlue"), 
                Content.Load<Texture2D>("Player/playerRail"), new Rectangle(50, 450, 30, 30));
            // physics
            physics = new Physics(screenHeight, screenWidth);

            //items
            spriteList.Add(new Box(Content.Load<Texture2D>("Objects/smallBox1"), new Rectangle(0, screenHeight - 11 - 35, 17, 11)));
            spriteList.Add(new Rail(Content.Load<Texture2D>("Objects/rail1"), new Rectangle(0, screenHeight - 11 - 35, 55, 11)));

            // sound effecxz
            ouchSound = Content.Load<SoundEffect>("Sounds/ouch_wave");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // esc to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            // scrollable background
            if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0)
                scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.rectangle.Width;

            if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0)
                scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.rectangle.Width;
            scrolling1.Update();
            scrolling2.Update();

            // physics
            physics.Player(ref player, Keyboard.GetState());            //player
            spriteList = physics.Sprite(spriteList);                    // objects-move
            physics.Intersect(ref player, spriteList,
                ouchSound, gameTime.TotalGameTime, ref gameValues);       //player/objects collisions

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            // draw background
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);

            // draw objects
            foreach (Sprite s in spriteList)
            {
                s.Draw(spriteBatch);
            }

            // draw player
            player.Draw(spriteBatch);


            // text
            if (gameTime.TotalGameTime < TimeSpan.FromSeconds(3))
                text.Draw(spriteBatch);
            else
            {
                if (gameValues[0] >= 1)
                {
                    text.Draw(spriteBatch, "Lives left: " + gameValues[0] +
                        "\nRail points: " + gameValues[1], new Vector2(screenWidth / 5, screenHeight / 5));
                }
                if (gameValues[0] <= 0)
                {
                    endscore = gameValues[1];
                    text.Draw(spriteBatch, "You died skatin'!\nYou railed " + endscore + " points",
                        new Vector2(screenWidth / 3, screenHeight / 3));
                }
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
