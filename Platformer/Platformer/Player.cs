using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Input;
using PolyLib;
using PolyLib.Graphics;

namespace Platformer
{
    public class Player : ICollisionActor
    {
        private DirectionalSprite _sprite;
        private float _speed = 200.0f;
        private float _jumpForce = 1200;
        private float _gravity = 400f;
        private float _timeFalling = 0f;
        private bool _isOnGround = false;
        private Vector2 _velocity = Vector2.Zero;
        private RectangleF _bounds;

        IShapeF ICollisionActor.Bounds => _bounds;

        public Player(Vector2 position, DirectionalSprite sprite)
        {
            _sprite = sprite;
            _bounds = new RectangleF(position.X, position.Y, _sprite.Width, sprite.Height);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardExtended.Update();
            KeyboardStateExtended keyboardState = KeyboardExtended.GetState();

            _velocity.X = 0;

            // Walking input
            if (keyboardState.IsKeyDown(Keys.A))
            {
                _velocity.X -= 1;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                _velocity.X += 1;
            }
            
            // Gravity
            if (!_isOnGround)
            {
                _timeFalling += gameTime.GetElapsedSeconds();
                _velocity.Y += _gravity * _timeFalling;
            }
            else
            {
                _timeFalling = 0;
            }

            if (_velocity == Vector2.Zero)
            {
                _sprite.Direction = Direction.None;
            }

            // Jumping input
            if (keyboardState.IsKeyDown(Keys.Space) && _isOnGround)
            {
                _velocity.Y -= _jumpForce;
                _isOnGround = false;
            }

            Vector2 newPos = _bounds.Position;
            
            // Handle X position change
            if (_velocity.X != 0)
            {
                _sprite.Direction = _velocity.X > 0 ? Direction.Right : Direction.Left;
                newPos.X += _velocity.X * gameTime.GetElapsedSeconds() * _speed;
            }

            // Handle Y position change
            if (_velocity.Y != 0)
            {
                if (_velocity.X == 0)
                {
                    _sprite.Direction = _velocity.Y > 0 ? Direction.Down : Direction.Up;
                }

                newPos.Y += _velocity.Y * gameTime.GetElapsedSeconds();
            }

            if (newPos.Y > 480)
            {
                newPos.Y = 480;
                _isOnGround = true;
            }

            _bounds.Position = newPos;

            _sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, _bounds.Position);
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            _bounds.Position -= collisionInfo.PenetrationVector;

            if (collisionInfo.PenetrationVector.X != 0)
            {
                _velocity = new Vector2(0, _velocity.Y);
            }

            if (collisionInfo.PenetrationVector.Y != 0)
            {
                _velocity = new Vector2(_velocity.X, 0);

                if (collisionInfo.PenetrationVector.Y > 0)
                {
                    _isOnGround = true;
                }
            }
        }
    }
}
