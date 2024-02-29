using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FakeAgar.io.Files
{
    public class Player
    {
        public Vector2 _position;
        public Color _color;
        public int _size = 30;
        public int _speed = 2;

        public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, _size, _size);

        public Player()
        {
        }

        public Player(Color color, Vector2 position)
        {
            _position = position;
            _color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(Bounds, _color);
        }

        public void Move(Vector2 direction)
        {
            _position += direction;

            _position.X = MathHelper.Clamp(_position.X, 0, Main.worldWidth - _size);
            _position.Y = MathHelper.Clamp(_position.Y, 0, Main.worldHeight - _size);
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

        public bool IsCollidingWithBorder(int worldWidth, int worldHeight)
        {
            if (_position.X <= 0 || _position.X + _size >= worldWidth ||
                _position.Y <= 0 || _position.Y + _size >= worldHeight)
            {
                return true;
            }
            return false;
        }
    }
}
