using System;
using System.IO;
using AsepriteDotNet.Aseprite;
using AsepriteDotNet.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;

namespace Platformer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteSheet _characterSpriteSheet;
        private AnimatedSprite _walk;
        private AnimatedSprite _jump;

        public Game1()
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

            AsepriteFile aseFile;
            using (Stream stream = TitleContainer.OpenStream("Content/sprites/girl.aseprite"))
            {
                aseFile = AsepriteFileLoader.FromStream("girl", stream);
            }

            _characterSpriteSheet = aseFile.CreateSpriteSheet(GraphicsDevice, onlyVisibleLayers: true);
            _walk = _characterSpriteSheet.CreateAnimatedSprite("walk");
            _walk.Scale = new Vector2(10.0f);
            _jump = _characterSpriteSheet.CreateAnimatedSprite("jump");
            _jump.Scale = new Vector2(10.0f);

            _walk.Play();
            _jump.Play();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _walk.Update(gameTime);
            _jump.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkViolet);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _spriteBatch.Draw(_walk, new Vector2(10, 10));
            _spriteBatch.Draw(_jump, new Vector2(100, 10));

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
