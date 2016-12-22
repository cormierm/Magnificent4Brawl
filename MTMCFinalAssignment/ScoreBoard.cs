using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MTMCFinalAssignment
{
    public class ScoreBoard : DrawableGameComponent
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private SpriteFont nameFont;
        private Texture2D texHealth;
        private Texture2D texHealthBG;
        private Texture2D texP1Head;
        private Texture2D texP2Head;
        private Character[] players;
        private Vector2 posRightName;
        private Rectangle rectP1Health;
        private Rectangle rectP2Health;
        private Rectangle rectP1HealthBG;
        private Rectangle rectP2HealthBG;
        private int HEALTHWIDTH = 302;
        private int HEALTHHEIGHT = 20;
        private int POSYLIFE = 45;
        private Vector2 posP1Lives;
        private Vector2 posP2Lives;
        public ScoreBoard(Game game, SpriteBatch spriteBatch, Texture2D texP1Head, Texture2D texP2Head, Character[] players) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.players = players;
            this.nameFont = game.Content.Load<SpriteFont>("fonts/nameFont");
            texHealth = game.Content.Load<Texture2D>("images/healthbar");
            texHealthBG = game.Content.Load<Texture2D>("images/healthbarbg2");
            posRightName = new Vector2(Shared.stage.X - nameFont.MeasureString(Shared.players[1].Name).X, 0);
            rectP1HealthBG = new Rectangle(0, 20, HEALTHWIDTH, HEALTHHEIGHT);
            rectP2HealthBG = new Rectangle((int)Shared.stage.X - 302, 20, HEALTHWIDTH, HEALTHHEIGHT);
            this.texP1Head = texP1Head;
            this.texP2Head = texP2Head;
            posP2Lives = new Vector2(Shared.stage.X - texP2Head.Width, POSYLIFE);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            rectP1Health = new Rectangle(1, 21, 3 * Shared.players[0].Health, HEALTHHEIGHT - 2);
            rectP2Health = new Rectangle((int)Shared.stage.X - 3 * Shared.players[1].Health - 1, 21, 3 * Shared.players[1].Health, HEALTHHEIGHT - 2);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(nameFont, Shared.players[0].Name, Vector2.Zero, Color.Blue);
            spriteBatch.DrawString(nameFont, Shared.players[1].Name, posRightName, Color.Blue);
            spriteBatch.Draw(texHealthBG, rectP1HealthBG, Color.White);
            spriteBatch.Draw(texHealthBG, rectP2HealthBG, Color.White);
            spriteBatch.Draw(texHealth, rectP1Health, Color.Red);
            spriteBatch.Draw(texHealth, rectP2Health, Color.Red);
            for (int i = 0; i < Shared.players[0].Lives; i++)
            {
                posP1Lives = new Vector2(i * texP1Head.Width, POSYLIFE);
                spriteBatch.Draw(texP1Head, posP1Lives, Color.White);
            }
            for (int i = 0; i < Shared.players[1].Lives; i++)
            {
                posP2Lives = new Vector2(Shared.stage.X - ((i+1) * texP2Head.Width), POSYLIFE);
                spriteBatch.Draw(texP2Head, posP2Lives, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
