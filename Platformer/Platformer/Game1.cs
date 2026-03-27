using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;

namespace Platformer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private AnimatedSprite _adventurer;

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

            Texture2DAtlas atlas = Content.Load<Texture2DAtlas>("sprites/spritesheet");
            SpriteSheet spriteSheet = new SpriteSheet("Spritesheet/adventurer", atlas);

            TimeSpan duration = TimeSpan.FromSeconds(0.1);
            spriteSheet.DefineAnimation("attack", builder =>
            {
                builder.IsLooping(false)
                       .AddFrame("adventurer-attack3-00", duration)
                       .AddFrame("adventurer-attack3-01", duration)
                       .AddFrame("adventurer-attack3-02", duration)
                       .AddFrame("adventurer-attack3-03", duration)
                       .AddFrame("adventurer-attack3-04", duration)
                       .AddFrame("adventurer-attack3-05", duration);
            });

            spriteSheet.DefineAnimation("idle", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame("adventurer-idle-2-00", duration)
                       .AddFrame("adventurer-idle-2-01", duration)
                       .AddFrame("adventurer-idle-2-02", duration)
                       .AddFrame("adventurer-idle-2-03", duration);
            });

            spriteSheet.DefineAnimation("run", builder =>
            {
                builder.IsLooping(true)
                       .AddFrame("adventurer-run-00", duration)
                       .AddFrame("adventurer-run-01", duration)
                       .AddFrame("adventurer-run-02", duration)
                       .AddFrame("adventurer-run-03", duration)
                       .AddFrame("adventurer-run-04", duration)
                       .AddFrame("adventurer-run-05", duration);
            });

            _adventurer = new AnimatedSprite(spriteSheet, "idle");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _adventurer.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            int scale = 3;
            _spriteBatch.Draw(_adventurer, _adventurer.Origin * scale, 0, new Vector2(scale));
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
