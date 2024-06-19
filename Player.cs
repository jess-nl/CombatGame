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
        private KeyboardState oldState;

        public Player(Texture2D texture, Vector2 position, AnimationManager am, List<Sprite> collisionGroup) : base(texture, position, am)
        {
            this.collisionGroup = collisionGroup;
            this.oldState = Keyboard.GetState();
        }

        public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            base.Update(gameTime, graphics);

            var kState = Keyboard.GetState();

            float changeX = 0;

            // Spin
            if (kState.IsKeyDown(Keys.B))
                am.ChangeFrames(8, 12, true);

            // Kick
            if (kState.IsKeyDown(Keys.V))
                am.ChangeFrames(12, 16, true);

            // Wand attack
            if (kState.IsKeyDown(Keys.G))
                am.ChangeFrames(16, 20, true);

            // Walk left
            if (kState.IsKeyDown(Keys.Left) && oldState.IsKeyDown(Keys.Left))
            {
                am.ChangeFrames(0, 4);
                changeX -= SPEED;
            }

            // Walk right
            if (kState.IsKeyDown(Keys.Right) && oldState.IsKeyDown(Keys.Right))
            {
                am.ChangeFrames(4, 8);
                changeX += SPEED;
            }

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

            oldState = kState;
        }
    }
}
