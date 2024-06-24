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

        public Direction lastDirection = Direction.Right;

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
            float changeY = 0;

            var isLeftDirection = lastDirection == Direction.Left;
            var isRightDirection = lastDirection == Direction.Right;

            HorizontalWalk(kState, changeX);
            VerticalMove(kState, changeY);

            Kick(kState, isLeftDirection, isRightDirection);
            Spin(kState, isLeftDirection, isRightDirection);
            WandAttack(kState, isLeftDirection, isRightDirection);

            oldState = kState;
        }

        private void HorizontalWalk(KeyboardState kState, float changeX)
        {
            // Left

            var isNewLeftPress = kState.IsKeyDown(Keys.Left) && !oldState.IsKeyDown(Keys.Left);
            var isLeftRelease = !kState.IsKeyDown(Keys.Left) && oldState.IsKeyDown(Keys.Left);
            var isLeftKeyDown = kState.IsKeyDown(Keys.Left) && oldState.IsKeyDown(Keys.Left);

            if (isNewLeftPress || isLeftRelease)
                am.ChangeFrames(0, 1, true);

            if (isLeftKeyDown)
            {
                am.ChangeFrames(0, 4);
                changeX -= SPEED;
                lastDirection = Direction.Left;
            }

            // Right

            var isNewRightPress = kState.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right);
            var isRightRelease = !kState.IsKeyDown(Keys.Right) && oldState.IsKeyDown(Keys.Right);
            var isRightKeyDown = kState.IsKeyDown(Keys.Right) && oldState.IsKeyDown(Keys.Right);

            if (isNewRightPress || isRightRelease)
                am.ChangeFrames(4, 5, true);

            if (isRightKeyDown)
            {
                am.ChangeFrames(4, 8);
                changeX += SPEED;
                lastDirection = Direction.Right;
            }

            position.X += changeX;

            // Collision

            foreach (var sprite in collisionGroup)
            {
                if (sprite != this && sprite.Rect.Intersects(Rect))
                    position.X -= changeX;
            }
        }

        private void VerticalMove(KeyboardState kState, float changeY)
        {
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

        private void Kick(KeyboardState kState, bool isLeftDirection, bool isRightDirection)
        {
            if (isLeftDirection && kState.IsKeyDown(Keys.V))
                am.ChangeFrames(16, 20, true);

            if (isRightDirection && kState.IsKeyDown(Keys.V))
                am.ChangeFrames(20, 24, true);
        }

        private void Spin(KeyboardState kState, bool isLeftDirection, bool isRightDirection)
        {
            if (isLeftDirection && kState.IsKeyDown(Keys.B))
                am.ChangeFrames(8, 12, true);

            if (isRightDirection && kState.IsKeyDown(Keys.B))
                am.ChangeFrames(12, 16, true);
        }

        private void WandAttack(KeyboardState kState, bool isLeftDirection, bool isRightDirection)
        {
            if (isLeftDirection && kState.IsKeyDown(Keys.G))
                am.ChangeFrames(24, 28, true);

            if (isRightDirection && kState.IsKeyDown(Keys.G))
                am.ChangeFrames(28, 32, true);
        }
    }

    public enum Direction
    {
        Left,
        Right,
    }
}
