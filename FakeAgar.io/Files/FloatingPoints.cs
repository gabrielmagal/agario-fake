using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FakeAgar.io.Files
{
    public class FloatingPoints
    {
        public Vector2 _position;
        public int _size = 10;
        public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, _size, _size);

        public FloatingPoints(Vector2 position)
        {
            _position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(Bounds, Color.Green);
        }

        public bool Intersects(Player player, Camera camera)
        {
            Rectangle playerBounds = player.Bounds;
            Rectangle thisBounds = Bounds;
            playerBounds.X -= (int)camera.Transform.Translation.X;
            playerBounds.Y -= (int)camera.Transform.Translation.Y;
            thisBounds.X -= (int)camera.Transform.Translation.X;
            thisBounds.Y -= (int)camera.Transform.Translation.Y;
            Rectangle intersection = Rectangle.Intersect(thisBounds, playerBounds);
            float intersectionArea = intersection.Width * intersection.Height;
            float thisArea = thisBounds.Width * thisBounds.Height;
            float coveragePercentage = intersectionArea / thisArea;
            return coveragePercentage >= 0.65f && player._size >= _size * 1.3f;
        }
    }
}
