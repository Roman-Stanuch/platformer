using System;
using System.IO;
using System.Reflection.PortableExecutable;
using AsepriteDotNet.Aseprite;
using AsepriteDotNet.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using PolyLib.Graphics;
using PolyLib;

namespace Platformer
{
    public class PlatformerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteSheet _characterSpriteSheet;
        private DirectionalSprite _characterSprite;

        public PlatformerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _characterSprite = new DirectionalSprite(GraphicsDevice, "Content/sprites/girl.aseprite", "jump", "land", "walk", "idle");
            _characterSprite.Scale = 10f;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState keyboardState = Keyboard.GetState();

            bool moved = false;

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                _characterSprite.Direction = Direction.Up;
                moved = true;
            }

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                _characterSprite.Direction = Direction.Down;
                moved = true;
            }

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                _characterSprite.Direction = Direction.Left;
                moved = true;
            }

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                _characterSprite.Direction = Direction.Right;
                moved = true;
            }

            if (!moved)
            {
                _characterSprite.Direction = Direction.None;
            }

            _characterSprite.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkViolet);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _characterSprite.Draw(_spriteBatch, new Vector2(20f, 20f));

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
