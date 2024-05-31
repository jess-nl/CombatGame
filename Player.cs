using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CombatGame
{
    public class Player : Sprite
    {
        private static readonly float SPEED = 5;
        List<Sprite> collisionGroup;

        public Player(Texture2D texture, Vector2 position, List<Sprite> collisionGroup) : base(texture, position)
        {
            this.collisionGroup = collisionGroup;
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            base.Update(gameTime, graphics);

            var kState = Keyboard.GetState();

            float changeX = 0;
            if (kState.IsKeyDown(Keys.Right))
                changeX += SPEED;
            if (kState.IsKeyDown(Keys.Left))
                changeX -= SPEED;
            position.X += changeX;

            foreach (var sprite in collisionGroup)
            {
                if (sprite != this && sprite.Rect.Intersects(Rect))
                    position.X -= changeX;
            }

            float changeY = 0;
            if (kState.IsKeyDown(Keys.Up))
                changeY -= SPEED;
            if (kState.IsKeyDown(Keys.Down))
                changeY += SPEED;
            position.Y += changeY;

            foreach (var sprite in collisionGroup)
            {
                if (sprite != this && sprite.Rect.Intersects(Rect))
                    position.Y -= changeY;
            }
        }
    }
}
