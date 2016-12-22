using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace MTMCFinalAssignment
{
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        Character[] players;
        ScoreBoard scoreBoard;
        Texture2D texPlayerP1;
        Texture2D texPlayerP2;
        Texture2D texP1Head;
        Texture2D texP2Head;
        string nameP1;
        string nameP2;
        Background background;
        List<Platform> platforms = new List<Platform>();

        public ActionScene(Game game,
            SpriteBatch spriteBatch,
            Texture2D texPlayerP1,
            Texture2D texPlayerP2,
            Texture2D texP1Head,
            Texture2D texP2Head,
            string nameP1,
            string nameP2) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texPlayerP1 = texPlayerP1;
            this.texPlayerP2 = texPlayerP2;
            this.texP1Head = texP1Head;
            this.texP2Head = texP2Head;
            this.nameP1 = nameP1;
            this.nameP2 = nameP2;
            Shared.actionScene = this;

            Random rnd = new Random();
            int level = rnd.Next(0, 2);

            //Sound Effects
            SoundEffect kickSound = game.Content.Load<SoundEffect>("Sounds/click");
            SoundEffect swooshSound = game.Content.Load<SoundEffect>("Sounds/swoosh");
            SoundEffect deadSound = game.Content.Load<SoundEffect>("Sounds/dead");
            SoundEffect gruntSound = game.Content.Load<SoundEffect>("Sounds/grunt");

            // Loads Background
            Texture2D texBG;
            if (level == 0)
            {
                texBG = game.Content.Load<Texture2D>("images/buildingbg");
            }
            else
            {
                texBG = game.Content.Load<Texture2D>("images/mariobg1");
            }
            background = new Background(game, spriteBatch, texBG);
            this.Components.Add(background);

            //Character
            players = new Character[2];
            players[0] = new Character(game, spriteBatch, texPlayerP1, new Vector2(40, 320), Keys.Left, Keys.Right, Keys.Up, Keys.Delete, Keys.End, 
                nameP1, kickSound, swooshSound, deadSound, gruntSound);
            this.Components.Add(players[0]);

            players[1] = new Character(game, spriteBatch, texPlayerP2, new Vector2(620, 120), Keys.A, Keys.D, Keys.W, Keys.Tab, Keys.Q, nameP2, 
                kickSound, swooshSound, deadSound, gruntSound);
            this.Components.Add(players[1]);
            Shared.players = players;

            //Scoreboard
            scoreBoard = new ScoreBoard(game, spriteBatch, texP1Head, texP2Head, players);
            this.Components.Add(scoreBoard);

            //Music
            Song mainMusic = game.Content.Load<Song>("Music/action");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(mainMusic);

            // Loads platforms
            if (level == 0)
            {
                Texture2D tex = game.Content.Load<Texture2D>("images/building");
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(300, 350, 200, 300), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(50, 280, 160, 300), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(600, 280, 200, 300), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(850, 350, 150, 300), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(1100, 420, 250, 300), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(1400, 350, 200, 300), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(1700, 330, 200, 300), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(2050, 280, 160, 300), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(2300, 300, 200, 300), players));
            }
            else
            {
                Texture2D tex = game.Content.Load<Texture2D>("images/marioplatform");
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(30, 180, 50, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(0, 100, 50, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(30, 400, 250, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(80, 320, 50, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(120, 260, 100, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(350, 350, 250, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(350, 190, 100, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(600, 280, 150, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(690, 130, 50, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(800, 330, 250, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(820, 200, 50, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(1100, 350, 200, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(1150, 250, 100, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(1350, 380, 250, 20), players));
                platforms.Add(new Platform(game, spriteBatch, tex, new Rectangle(1530, 300, 50, 20), players));
            }
            foreach (Platform platform in platforms)
            {
                this.Components.Add(platform);
            }
            
            Shared.campos = Vector2.Zero;
            CountDown cd = new CountDown(game, spriteBatch);
            this.Components.Add(cd);
            EndGame eg = new EndGame(game, spriteBatch);
            this.Components.Add(eg);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
