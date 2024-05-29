using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CombatGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Sprite _sprite;

        public const float SPRITE_SPEED = 100f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            var texture = Content.Load<Texture2D>("ball");
            _sprite = new Sprite(texture, Vector2.Zero, SPRITE_SPEED);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var kState = Keyboard.GetState();

            if (kState.IsKeyDown(Keys.Up))
                _sprite.position.Y -= _sprite.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.Down))
                _sprite.position.Y += _sprite.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.Left))
                _sprite.position.X -= _sprite.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.Right))
                _sprite.position.X += _sprite.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_sprite.position.X > _graphics.PreferredBackBufferWidth - _sprite.texture.Width / 2)
                _sprite.position.X = _graphics.PreferredBackBufferWidth - _sprite.texture.Width / 2;
            else if (_sprite.position.X < _sprite.texture.Width / 2)
                _sprite.position.X = _sprite.texture.Width / 2;

            if (_sprite.position.Y > _graphics.PreferredBackBufferHeight - _sprite.texture.Height / 2)
                _sprite.position.Y = _graphics.PreferredBackBufferHeight - _sprite.texture.Height / 2;
            else if (_sprite.position.Y < _sprite.texture.Height / 2)
                _sprite.position.Y = _sprite.texture.Height / 2;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(
                _sprite.texture,
                _sprite.position,
                null,
                Color.White,
                0f,
                new Vector2(_sprite.texture.Width / 2, _sprite.texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
