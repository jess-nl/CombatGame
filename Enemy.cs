using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatGame
{
    public class Enemy : Sprite
    {
        public int Health { get; set; }

        public Enemy(Texture2D texture, Vector2 position, AnimationManager am, int health) : base(texture, position, am)
        {
            Health = health;
        }
    }
}
