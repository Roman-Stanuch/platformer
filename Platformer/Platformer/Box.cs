
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace Platformer
{
    public class Box : ICollisionActor
    {
        private RectangleF _bounds;
        private Color _color;

        public IShapeF Bounds => _bounds;
        public Color Color => _color;

        public Box(float x, float y, float width, float height, Color color)
        {
            _bounds = new RectangleF(x, y, width, height);
            _color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(_bounds, _color);
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            return;
        }
    }
}
