
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace Platformer
{
    public class Box : ICollisionActor
    {
        private RectangleF _bounds;

        public IShapeF Bounds => _bounds;

        public Box(float x, float y, float width, float height)
        {
            _bounds = new RectangleF(x, y, width, height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(_bounds, Color.Blue);
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            return;
        }
    }
}
