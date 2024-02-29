using Microsoft.Xna.Framework;
using System;

namespace FakeAgar.io.Files
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        private int _viewportWidth;
        private int _viewportHeight;
        private Vector2 _position;
        private float _zoom = 1.0f;
        public float _playerReferenceSize;

        public Camera(int viewportWidth, int viewportHeight, float playerReferenceSize)
        {
            _viewportWidth = viewportWidth;
            _viewportHeight = viewportHeight;
            _position = Vector2.Zero;
            _playerReferenceSize = playerReferenceSize;
        }

        public void Follow(Player target)
        {
            float desiredZoomDistance = 100.0f;

            float currentZoomDistance = Math.Max(target._size * 2, desiredZoomDistance);

            _zoom = desiredZoomDistance / currentZoomDistance;

            float x = target._position.X + target._size / 2 - _viewportWidth / (2 * _zoom);
            float y = target._position.Y + target._size / 2 - _viewportHeight / (2 * _zoom);

            x = MathHelper.Clamp(x, 0, Main.worldWidth - _viewportWidth / _zoom);
            y = MathHelper.Clamp(y, 0, Main.worldHeight - _viewportHeight / _zoom);

            _position = new Vector2(x, y);

            Transform = Matrix.CreateTranslation(new Vector3(-_position.X, -_position.Y, 0)) *
                        Matrix.CreateScale(new Vector3(_zoom, _zoom, 1));
        }
    }
}
