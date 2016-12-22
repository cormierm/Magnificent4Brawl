using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MTMCFinalAssignment
{
    public class CountDown : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private SpriteFont countFont;
        private Character[] players;
        private Vector2 position;
        private Color color;
        private int timer;
        private const int TIMERINTERVAL = 80;
        private string text;
        public CountDown(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.countFont = game.Content.Load<SpriteFont>("fonts/countFont");
            this.players = Shared.players;
            color = Color.Black;
            timer = 0;
            text = "";
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            timer++;
            if (timer > TIMERINTERVAL * 4)
            {
                this.Visible = false;
                game.Components.Remove(this);
            }
            else if (timer > TIMERINTERVAL * 3)
            {
                text = "FIGHT!!";
            }
            else if (timer > TIMERINTERVAL * 2)
            {
                text = "1";
            }
            else if (timer > TIMERINTERVAL * 1)
            {
                text = "2";
            }
            else
            {
                text = "3";
            }
            position = new Vector2((int)(Shared.stage.X / 2) - (countFont.MeasureString(text).X / 2), (int)(Shared.stage.Y / 2) - (countFont.MeasureString(text).Y / 2));
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(countFont, text, position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
