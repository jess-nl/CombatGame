using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CombatGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        List<Sprite> sprites;
        Player player;

        Texture2D ssSailorMoon;
        AnimationManager amSailorMoon;
        AnimationManager amEnemy;

        Texture2D healthBarTexture;
        AnimationManager amHealthBar;
        Rectangle recHealthBar;

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
            sprites = new();

            var enemyTexture = Content.Load<Texture2D>("enemy-arms-crossed");
            amEnemy = new(1, 1, new Vector2(16, 40));
            sprites.Add(new Sprite(enemyTexture, new Vector2(40, 40), amEnemy));
            sprites.Add(new Sprite(enemyTexture, new Vector2(250, 200), amEnemy));
            sprites.Add(new Sprite(enemyTexture, new Vector2(500, 100), amEnemy));

            ssSailorMoon = Content.Load<Texture2D>("playersheet-sailor-moon");
            amSailorMoon = new(4, 4, new Vector2(40, 50), 0, 1);
            player = new Player(ssSailorMoon, new Vector2(600, 300), amSailorMoon, sprites, 100);
            sprites.Add(player);

            healthBarTexture = Content.Load<Texture2D>("health-bar");
            amHealthBar = new(1, 1, new Vector2(8, 8));
            recHealthBar = new Rectangle(650, 30, player.health, 8);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime, _graphics);
            }

            recHealthBar.Width = player.health;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightPink);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            foreach (var sprite in sprites)
            {
                sprite.Draw(_spriteBatch);
            }
            _spriteBatch.Draw(healthBarTexture, recHealthBar, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
