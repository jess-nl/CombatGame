using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatGame
{
    public class Sprite
    {
        public Texture2D texture;
        public Vector2 position;
        public AnimationManager am;

        private const int DEFAULT_WIDTH = 40;
        private const int DEFAULT_HEIGHT = 50;
        private static readonly float SCALE = 3.5f;

        public Rectangle Rect
        {
            get
            {
                var sizeW = am.numFrames == 1 ? texture.Width : DEFAULT_WIDTH;
                var sizeH = am.numFrames == 1 ? texture.Height : DEFAULT_HEIGHT;

                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    sizeW * (int)SCALE,
                    sizeH * (int)SCALE
                    );
            }
        }

        public Sprite(Texture2D texture, Vector2 position, AnimationManager am)
        {
            this.texture = texture;
            this.position = position;
            this.am = am;
        }

        public virtual void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            am.Update();
            // VerifyWindowBounds(graphics); // TODO: Fix window bounds functionality
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                Rect,
                am.GetFrame(),
                Color.White
                );
        }

        public void VerifyWindowBounds(GraphicsDeviceManager graphics)
        {
            // Right boundary
            if (position.X > graphics.PreferredBackBufferWidth - texture.Width / 2)
                position.X = graphics.PreferredBackBufferWidth - texture.Width / 2;
            // Left boundary
            else if (position.X < 0)
                position.X = 0;

            var spriteHeight = texture.Height * (2 * SCALE);
            // Bottom boundary
            if (position.Y > graphics.PreferredBackBufferHeight - spriteHeight / 2)
                position.Y = graphics.PreferredBackBufferHeight - spriteHeight / 2;
            // Top boundary
            else if (position.Y < texture.Height / 2)
                position.Y = texture.Height / 2;
        }
    }
}
