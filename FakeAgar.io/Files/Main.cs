using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FakeAgar.io.Files
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        public static Player _myPlayer = new Player();
        private List<Player> _enemies = new List<Player>();
        private List<FloatingPoints> _floatingPoints = new List<FloatingPoints>();

        private Camera _camera;

        public static int worldWidth = 10000;
        public static int worldHeight = 10000;
        public static int viewportWidth;
        public static int viewportHeight;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            viewportWidth = GraphicsDevice.Viewport.Width;
            viewportHeight = GraphicsDevice.Viewport.Height;

            _myPlayer = new Player(Color.Red, new Vector2((worldWidth - _myPlayer._size) / 2, (worldHeight - _myPlayer._size) / 2));


            for (int i = 0; i < 2000; i++)
            {
                _enemies.Add(new Player(Color.Blue, GenerateRandomPosition()));
            }

            for (int i = 0; i < 10000; i++)
            {
                _floatingPoints.Add(new FloatingPoints(GenerateRandomPosition()));
            }

            _camera = new Camera(viewportWidth, viewportHeight, _myPlayer._size);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("FontDefault");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;

            if (state.IsKeyDown(Keys.Up))
                direction.Y -= 1;
            if (state.IsKeyDown(Keys.Down))
                direction.Y += 1;
            if (state.IsKeyDown(Keys.Left))
                direction.X -= 1;
            if (state.IsKeyDown(Keys.Right))
                direction.X += 1;

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                _myPlayer.Move(direction * _myPlayer._speed);
            }

            if (_myPlayer.IsCollidingWithBorder(worldWidth, worldHeight))
            {
                _myPlayer._size = 30;
            }

            for (int i = 0; i <= _floatingPoints.Count - 1; i++)
            {
                if (_floatingPoints[i].Intersects(_myPlayer, _camera))
                {
                    _myPlayer._size += _floatingPoints[i]._size;
                    _floatingPoints.RemoveAt(i);
                }
            }

            for (int i = 0; i <= _enemies.Count - 1; i++)
            {
                if (_enemies[i].Intersects(_myPlayer, _camera))
                {
                    _myPlayer._size += _enemies[i]._size;
                    _enemies.RemoveAt(i);
                }
            }

            _camera.Follow(_myPlayer);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            _spriteBatch.DrawRectangleOutline(new Rectangle(0, 0, worldWidth, worldHeight), Color.Black, 8);

            _myPlayer.Draw(_spriteBatch);

            foreach (Player enemy in _enemies)
            {
                enemy.Draw(_spriteBatch);
            }

            foreach (FloatingPoints floatingPoint in _floatingPoints)
            {
                floatingPoint.Draw(_spriteBatch);
            }

            _spriteBatch.DrawString(_font, "Pontos: " + _myPlayer._size, new Vector2(_myPlayer._position.X, _myPlayer._position.Y - 20), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Vector2 GenerateRandomPosition()
        {
            return new Vector2(new Random().Next(1, worldWidth), new Random().Next(1, worldHeight));
        }
    }
}