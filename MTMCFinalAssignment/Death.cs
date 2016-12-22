using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MTMCFinalAssignment
{
    public class Death : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private double delay;
        private int delayCounter;
        private Game game;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex;
        private const int ROW = 1;
        private const int COL = 8;
        public Death(Game game,
            SpriteBatch spriteBatch,
            Vector2 position) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.position = position + new Vector2(-10,-20);
            tex = game.Content.Load<Texture2D>("images/bloodydeath4");
            dimension = new Vector2(tex.Width / COL, tex.Height / ROW);
            createFrames();
            frameIndex = -1;
            delay = 15;

        }
        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            if (frameIndex < ROW * COL - 1)
            {
                delayCounter++;
                if (delayCounter > delay)
                {
                    frameIndex++;
                    delayCounter = 0;
                }
            }
            else
            {
                delayCounter++;
                if (delayCounter > 150)
                {
                    game.Components.Remove(this);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                spriteBatch.Draw(tex, position + Shared.campos, frames[frameIndex], Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
