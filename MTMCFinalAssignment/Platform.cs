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
    public class Platform : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Rectangle rect;
        private const int COLLISIONMARGIN = 5;
        private const int TOPCOLLISIONMARGIN = 15;
        private Character[] players;
        
        public Platform(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Rectangle rect,
            Character[] players
            ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.rect = rect;
            this.players = players;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Character player in players)
            {
                if (player.Rect.Bottom > rect.Top &&
                        player.Rect.Bottom <= rect.Top + TOPCOLLISIONMARGIN &&
                        player.Rect.Left <= rect.Right - COLLISIONMARGIN &&
                        player.Rect.Right >= rect.Left + COLLISIONMARGIN)
                {
                    player.fallLeft = rect.Left - COLLISIONMARGIN;
                    player.fallRight = rect.Right + COLLISIONMARGIN;
                    player.position.Y = rect.Top - player.Rect.Height;
                    player.velocity.Y = 0f;
                    player.IsJumping = false;
                }
                else if (player.velocity.Y < 0 && player.Rect.Top > rect.Bottom - TOPCOLLISIONMARGIN &&
                        player.Rect.Top < rect.Bottom &&
                        player.Rect.Left <= rect.Right &&
                        player.Rect.Right >= rect.Left)
                {
                    player.position.Y = rect.Bottom;
                    player.velocity.Y = 0f;
                }
                else if (player.Rect.Bottom > rect.Top + COLLISIONMARGIN &&
                    player.Rect.Top <= rect.Bottom - COLLISIONMARGIN &&
                    ((player.Rect.Right >= rect.Left - COLLISIONMARGIN &&
                    player.Rect.Right <= rect.Left + COLLISIONMARGIN) ||
                    (player.Rect.Left >= rect.Right - COLLISIONMARGIN &&
                    player.Rect.Left <= rect.Right + COLLISIONMARGIN)))
                {
                    player.velocity.X = 0f;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, new Rectangle((int)(rect.X + Shared.campos.X), (int)(rect.Y + Shared.campos.Y), rect.Width, rect.Height), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
