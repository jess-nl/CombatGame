using Microsoft.Xna.Framework;

namespace CombatGame
{
    public class AnimationManager
    {
        public int numFrames;
        int numColumns;
        Vector2 size;

        int counter;
        int activeFrame;
        int interval;

        int startFrame;
        int? endFrame;

        int rowPos;
        int colPos;

        bool resetOnChange;

        private bool isPlaying = false;

        public AnimationManager(
            int numFrames,
            int numColumns,
            Vector2 size,
            int startFrame = 0,
            int? endFrame = null,
            bool resetOnChange = true
            )
        {
            this.numFrames = numFrames;
            this.numColumns = numColumns;
            this.size = size;
            this.startFrame = startFrame;
            this.endFrame = endFrame ?? numFrames - 1;
            this.resetOnChange = resetOnChange;

            counter = 0;
            interval = 18;

            ResetAnimation();
        }

        public void Update()
        {
            if (!isPlaying) return;

            counter++;
            if (counter > interval)
            {
                counter = 0;
                NextFrame();
            }
        }

        public void NextFrame()
        {
            if (!isPlaying)
                return;

            activeFrame++;
            colPos++;
            if (activeFrame >= endFrame)
            {
                isPlaying = false;
                ResetAnimation();
            }

            if (colPos >= numColumns)
            {
                colPos = 0;
                rowPos++;
            }
        }

        public void ChangeFrames(int start, int end, bool resetAnimation = false)
        {
            startFrame = start;
            endFrame = end;
            if (resetAnimation)
                ResetAnimation();

            isPlaying = true;
        }

        private void ResetAnimation()
        {
            activeFrame = startFrame;
            colPos = startFrame % numColumns;
            rowPos = startFrame / numColumns;
        }

        public Rectangle GetFrame()
        {
            return new Rectangle(
                colPos * (int)size.X,
                rowPos * (int)size.Y,
                (int)size.X,
                (int)size.Y
                );
        }
    }
}
