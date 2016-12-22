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
    public class StartScene : GameScene
    {
        private MenuComponent myMenuComponent;
        private Texture2D tex;
        private Vector2 position;


        public MenuComponent MyMenuComponent
        {
            get
            {
                return myMenuComponent;
            }

            set
            {
                myMenuComponent = value;
            }
        }

        private SpriteBatch spriteBatch;
        string[] menus = {  "Start Game",
                            "How To Play",
                            "Help",
                            "About",
                            "Quit"};

        public StartScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            Shared.startScene = this;
            position = new Vector2(Shared.stage.X / 3 + 60, Shared.stage.Y / 3 + 30);

            //Menu Components
            myMenuComponent = new MenuComponent(game, spriteBatch,
                game.Content.Load<SpriteFont>("fonts/regularFont"),
                game.Content.Load<SpriteFont>("fonts/hilightFont"),
                menus, position, Keys.Up, Keys.Down);

            this.Components.Add(myMenuComponent);

            //Background Image
            tex = game.Content.Load<Texture2D>("images/startScreen");

            //Music
            Song mainMusic = game.Content.Load<Song>("Music/main");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(mainMusic);
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
            spriteBatch.Begin();
            spriteBatch.Draw(tex, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
