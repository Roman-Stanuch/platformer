using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using PolyLib;
using PolyLib.Graphics;

namespace Platformer
{
    public class Player
    {
        private DirectionalSprite _sprite;
        private float _speed = 100.0f;

        public Player(Vector2 position, DirectionalSprite sprite)
        {
            _sprite = sprite;
            _sprite.Position = position;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardExtended.Update();
            KeyboardStateExtended keyboardState = KeyboardExtended.GetState();

            Vector2 velocity = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.W))
            {
                velocity.Y -= 1;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                velocity.Y += 1;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X -= 1;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity.X += 1;
            }

            if (velocity == Vector2.Zero)
            {
                _sprite.Direction = Direction.None;
            }

            if (velocity.X != 0)
            {
                _sprite.Direction = velocity.X > 0 ? Direction.Right : Direction.Left;
                _sprite.Position.X += velocity.X * gameTime.GetElapsedSeconds() * _speed;
            }

            if (velocity.Y != 0)
            {
                if (velocity.X == 0)
                {
                    _sprite.Direction = velocity.Y > 0 ? Direction.Down : Direction.Up;
                }
                _sprite.Position.Y += velocity.Y * gameTime.GetElapsedSeconds() * _speed;
            }

            _sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }
    }
}
