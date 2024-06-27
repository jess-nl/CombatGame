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
            get { return CreateRectangle(position); }
        }

        public virtual Rectangle Hitbox
        {
            get { return CreateRectangle(position, 8, 5); }
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

        private Rectangle CreateRectangle(Vector2 position, int paddingX = 0, int paddingY = 0)
        {
            var spriteW = am.numFrames == 1 ? texture.Width : DEFAULT_WIDTH;
            var spriteH = am.numFrames == 1 ? texture.Height : DEFAULT_HEIGHT;

            var hitboxW = spriteW - (2 * paddingX);
            var hitboxH = spriteH - (2 * paddingY);

            return new Rectangle(
                (int)position.X + paddingX * (int)SCALE,
                (int)position.Y + paddingY * (int)SCALE,
                hitboxW * (int)SCALE,
                hitboxH * (int)SCALE
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
