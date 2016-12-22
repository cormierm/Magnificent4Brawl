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
    public class Projectile : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Rectangle srcRect;
        private Rectangle rect;
        private Vector2 position;
        private Vector2 speed;
        private Vector2 origin;
        private float rotation;
        private Vector2 POSITIONOFFSET = new Vector2(10, 20);
        private const float ROTATIONSPEED = 0.3f;
        private const float SCALE = 0.2f;
        private Vector2 size;
        private float rotationChange;
        private Character owner;
        public Projectile(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed,
            Character owner) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position + POSITIONOFFSET;
            this.speed = speed;
            this.owner = owner;
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            size = new Vector2(tex.Width * SCALE, tex.Height * SCALE);
            if (speed.X > 0)
            {
                rotationChange = ROTATIONSPEED;
            }
            else
            {
                rotationChange = -ROTATIONSPEED;
            }
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            if (position.X + Shared.campos.X < 0 || position.X + Shared.campos.X > Shared.stage.X || position.Y < 0 || position.Y > Shared.stage.Y)
            {
                game.Components.Remove(this);
            }
            foreach (Character player in Shared.players)
            {
                if (rect.Intersects(player.Rect) && player != owner)
                {
                    player.IsJumping = true;
                    if (speed.X > 0)
                    {
                        player.velocity += new Vector2(2, -5);
                    }
                    else
                    {
                        player.velocity += new Vector2(-2, -5);
                    }
                    Random rnd = new Random();
                    player.Health -= rnd.Next(10, 30);
                    game.Components.Remove(this);
                }
            }
            
            rotation += rotationChange;
            position += speed;
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position + Shared.campos, srcRect, Color.White, rotation, origin, SCALE, SpriteEffects.None, 1f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
