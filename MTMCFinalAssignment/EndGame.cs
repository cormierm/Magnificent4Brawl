using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MTMCFinalAssignment
{
    public class EndGame : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private SpriteFont countFont;
        private Character[] players;
        private Vector2 position;
        private Color color;
        private int timer;
        private const int TIMEREND = 350;
        private string text;
        public EndGame(Game game, SpriteBatch spriteBatch) : base(game)
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
            if (timer > TIMEREND )
            {
                timer = 0;
                Shared.actionScene.hide();
                Shared.startScene.show();
                
                game.Components.Remove(this);
            }
            if ((players[0].Lives == -1 && players[1].Lives == -1))
            {
                timer++;
                text = "TIE GAME!!!";
            }
            else if (players[0].Lives == -1)
            {
                text = players[1].Name + " WIN!!!!!";
                timer++;
            }
            else if (players[1].Lives == -1)
            {
                text = players[0].Name + " WIN!!!!!";
                timer++;
            }
            position = new Vector2((int)((Shared.stage.X / 2) - (countFont.MeasureString(text).X / 2)), (int)((Shared.stage.Y / 2) - (countFont.MeasureString(text).Y / 2)));
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
