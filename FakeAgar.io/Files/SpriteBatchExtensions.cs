using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FakeAgar.io.Files
{
    public static class SpriteBatchExtensions
    {
        private static Texture2D _rectangleTexture;

        private static Texture2D _lineTexture;

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            if (_rectangleTexture == null)
            {
                _rectangleTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _rectangleTexture.SetData(new[] { Color.White });
            }

            spriteBatch.Draw(_rectangleTexture, rectangle, color);
        }

        public static void DrawRectangleOutline(this SpriteBatch spriteBatch, Rectangle rectangle, Color color, int thickness = 4)
        {
            if (_lineTexture == null)
            {
                _lineTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _lineTexture.SetData(new[] { Color.White });
            }

            spriteBatch.Draw(_lineTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, thickness), color);
            spriteBatch.Draw(_lineTexture, new Rectangle(rectangle.Left, rectangle.Bottom - thickness, rectangle.Width, thickness), color);

            spriteBatch.Draw(_lineTexture, new Rectangle(rectangle.Left, rectangle.Top, thickness, rectangle.Height), color);
            spriteBatch.Draw(_lineTexture, new Rectangle(rectangle.Right - thickness, rectangle.Top, thickness, rectangle.Height), color);
        }
    }
}
