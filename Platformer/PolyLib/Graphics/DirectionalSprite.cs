using System.IO;
using AsepriteDotNet.Aseprite;
using AsepriteDotNet.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;

namespace PolyLib.Graphics
{
    public class DirectionalSprite
    {
        private AnimatedSprite[] _animations;
        private Direction _direction;
        private AnimatedSprite _currentAnimation;
        private bool _facingLeft = false;
        private float _scale = 1.0f;

        public Vector2 Position = Vector2.Zero;

        public Direction Direction
        {
            get => _direction;
            set
            {
                if (value == _direction)
                {
                    return;
                }

                if (value == Direction.Left)
                {
                    _facingLeft = true;
                }
                else if (value == Direction.Right)
                {
                    _facingLeft = false;
                }

                _direction = value;
                _currentAnimation.Reset();
                _currentAnimation = _animations[(int)value];
                _currentAnimation.FlipHorizontally = _facingLeft;
                _currentAnimation.Play();
            }
        }
        public float Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                foreach(AnimatedSprite animation in _animations)
                {
                    animation.Scale = new Vector2(_scale);
                }
            }
        }
        
        public DirectionalSprite(GraphicsDevice graphicsDevice, string asepritePath, string upName, string downName, string horizontalName, string idleName)
        {
            AsepriteFile aseFile;
            using (Stream stream = TitleContainer.OpenStream(asepritePath))
            {
                aseFile = AsepriteFileLoader.FromStream(asepritePath, stream);
            }

            SpriteSheet spriteSheet = aseFile.CreateSpriteSheet(graphicsDevice, onlyVisibleLayers: true);

            AnimatedSprite up = spriteSheet.CreateAnimatedSprite(upName);
            AnimatedSprite down = spriteSheet.CreateAnimatedSprite(downName);
            AnimatedSprite left = spriteSheet.CreateAnimatedSprite(horizontalName);
            AnimatedSprite right = spriteSheet.CreateAnimatedSprite(horizontalName);
            AnimatedSprite idle = spriteSheet.CreateAnimatedSprite(idleName);

            _animations = [up, down, left, right, idle];
            _currentAnimation = idle;
            Direction = Direction.None;
        }

        public void Update(GameTime gameTime)
        {
            _currentAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_currentAnimation, Position);
        }
    }
}
