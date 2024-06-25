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
        public int points = 0;
        public Attack attack;
        public bool isIntersectingX = false;

        public Player(Texture2D texture, Vector2 position, AnimationManager am, List<Sprite> collisionGroup) : base(texture, position, am)
        {
            this.collisionGroup = collisionGroup;
            this.oldState = Keyboard.GetState();
            this.attack = new Attack(4, 6, 8);
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
            
            isIntersectingX = false;
            foreach (var sprite in collisionGroup)
            {
                if (sprite != this && sprite.Rect.Intersects(Rect))
                {
                    position.X -= changeX;
                    isIntersectingX = true;
                    break;
                }
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
            if (kState.IsKeyDown(Keys.V) && !oldState.IsKeyDown(Keys.V))
            {
                if (isLeftDirection)
                    am.ChangeFrames(16, 20, true);

                if (isRightDirection)
                    am.ChangeFrames(20, 24, true);

                if (isIntersectingX)
                    points += attack.Kick;
            }
        }

        private void Spin(KeyboardState kState, bool isLeftDirection, bool isRightDirection)
        {
            if (kState.IsKeyDown(Keys.B) && !oldState.IsKeyDown(Keys.B))
            {
                if (isLeftDirection)
                    am.ChangeFrames(8, 12, true);

                if (isRightDirection)
                    am.ChangeFrames(12, 16, true);

                if (isIntersectingX)
                    points += attack.Spin;
            }
        }

        private void WandAttack(KeyboardState kState, bool isLeftDirection, bool isRightDirection)
        {
            if (kState.IsKeyDown(Keys.G) && !oldState.IsKeyDown(Keys.G))
            {
                if (isLeftDirection)
                    am.ChangeFrames(24, 28, true);

                if (isRightDirection)
                    am.ChangeFrames(28, 32, true);

                if (isIntersectingX)
                    points += attack.WandAttack;
            }
        }
    }
}
