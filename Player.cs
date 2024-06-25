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

            if (IsNewKeyDown(kState, Keys.Left) || IsKeyReleased(ref kState, Keys.Left))
                am.ChangeFrames(0, 1, true);

            if (IsKeyHeldDown(ref kState, Keys.Left))
            {
                am.ChangeFrames(0, 4);
                changeX -= SPEED;
                lastDirection = Direction.Left;
            }

            // Right

            if (IsNewKeyDown(kState, Keys.Right) || IsKeyReleased(ref kState, Keys.Right))
                am.ChangeFrames(4, 5, true);

            if (IsKeyHeldDown(ref kState, Keys.Right))
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
            if (IsNewKeyDown(kState, Keys.V))
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
            if (IsNewKeyDown(kState, Keys.B))
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
            if (IsNewKeyDown(kState, Keys.G))
            {
                if (isLeftDirection)
                    am.ChangeFrames(24, 28, true);

                if (isRightDirection)
                    am.ChangeFrames(28, 32, true);

                if (isIntersectingX)
                    points += attack.WandAttack;
            }
        }

        private bool IsNewKeyDown(KeyboardState kState, Keys key)
        {
            return kState.IsKeyDown(key) && !oldState.IsKeyDown(key);
        }

        private bool IsKeyReleased(ref KeyboardState kState, Keys key)
        {
            return !kState.IsKeyDown(key) && oldState.IsKeyDown(key);
        }

        private bool IsKeyHeldDown(ref KeyboardState kState, Keys key)
        {
            return kState.IsKeyDown(key) && oldState.IsKeyDown(key);
        }
    }
}
