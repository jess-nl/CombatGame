using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CombatGame
{
    public class Sprite
    {
        public Texture2D texture;
        public Vector2 position;
        public float speed;

        public Sprite(Texture2D texture, Vector2 position, float speed)
        {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
        }
    }
}
