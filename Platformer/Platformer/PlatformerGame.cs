using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using PolyLib.Graphics;

namespace Platformer
{
    public class PlatformerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private CollisionComponent _collisionComponent;

        private DirectionalSprite _characterSprite;
        private Player _player;
        private Box _box;
        private Box _ground;

        public PlatformerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _collisionComponent = new CollisionComponent(new RectangleF(-10, -10, 900, 900));

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

            _characterSprite = new DirectionalSprite(GraphicsDevice, "Content/sprites/girl.aseprite", "jump", "walk", "walk", "idle");
            _characterSprite.Scale = 10f;
            _player = new Player(new Vector2(10f, 10f), _characterSprite);
            _box = new Box(100, 200, 100, 100, Color.Blue);
            _ground = new Box(0, 450, 800, 50, Color.ForestGreen);

            _collisionComponent.Insert(_player);
            _collisionComponent.Insert(_box);
            _collisionComponent.Insert(_ground);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime);

            _collisionComponent.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _player.Draw(_spriteBatch);
            _box.Draw(_spriteBatch);
            _ground.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
