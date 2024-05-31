using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatGame
{
    public class Sprite
    {
        public Texture2D texture;
        public Vector2 position;

        private static readonly float SCALE = 4f;

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    (int)texture.Width * (int)SCALE,
                    (int)texture.Height * (int)SCALE
                    );
            }
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public virtual void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            VerifyWindowBounds(graphics);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rect, Color.White);
        }

        public void VerifyWindowBounds(GraphicsDeviceManager graphics)
        {
            float spriteWidth = texture.Width * (2 * SCALE);
            float spriteHeight = texture.Height * (2 * SCALE);

            if (position.X > graphics.PreferredBackBufferWidth - spriteWidth / 2)
                position.X = graphics.PreferredBackBufferWidth - spriteWidth / 2;
            else if (position.X < texture.Width / 2)
                position.X = texture.Width / 2;

            if (position.Y > graphics.PreferredBackBufferHeight - spriteHeight / 2)
                position.Y = graphics.PreferredBackBufferHeight - spriteHeight / 2;
            else if (position.Y < texture.Height / 2)
                position.Y = texture.Height / 2;
        }
    }
}
