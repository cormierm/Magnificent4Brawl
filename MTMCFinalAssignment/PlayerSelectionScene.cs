using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace MTMCFinalAssignment
{
    public class PlayerSelectionScene : GameScene
    {
        Game game;
        Texture2D texPlayerP1;
        Texture2D texPlayerP2;
        Texture2D texP1Head;
        Texture2D texP2Head;
        string nameP1;
        string nameP2;
        SoundEffect kickSound;
        private MenuComponent menuComponentP1;
        private MenuComponent menuComponentP2;
        private SpriteFont font;

        private Texture2D tex;
        private SpriteBatch spriteBatch;
        private Vector2 positionP1;
        private Vector2 positionP2;
        string[] menus = {  "Sabbir",
                            "Margo",
                            "Matt",
                            "Bat Guy Fury"};
        string directions = "Player 1 use UP/DOWN keys, Player 2 use W/S keys, Press space to continue";

        public MenuComponent MenuComponentP1
        {
            get
            {
                return menuComponentP1;
            }

            set
            {
                menuComponentP1 = value;
            }
        }

        public MenuComponent MenuComponentP2
        {
            get
            {
                return menuComponentP2;
            }

            set
            {
                menuComponentP2 = value;
            }
        }

        public PlayerSelectionScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            positionP1 = new Vector2(100, Shared.stage.Y / 3 + 30);
            font = game.Content.Load<SpriteFont>("fonts/regularfont");

            //Menu Components
            menuComponentP1 = new MenuComponent(game, spriteBatch,
                game.Content.Load<SpriteFont>("fonts/regularFont"),
                game.Content.Load<SpriteFont>("fonts/hilightFont"),
                menus, positionP1, Keys.Up, Keys.Down);
            this.Components.Add(menuComponentP1);

            positionP2 = new Vector2(500, Shared.stage.Y / 3 + 30);

            menuComponentP2 = new MenuComponent(game, spriteBatch,
                game.Content.Load<SpriteFont>("fonts/regularFont"),
                game.Content.Load<SpriteFont>("fonts/hilightFont"),
                menus, positionP2, Keys.W, Keys.S);
            this.Components.Add(menuComponentP2);

            //Background Image
            tex = game.Content.Load<Texture2D>("images/startScreen");
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (this.Enabled && ks.IsKeyDown(Keys.Space))
            {
                int selectedIndex = MenuComponentP1.SelectedIndex;
                if (selectedIndex == 0)
                {
                    texPlayerP1 = game.Content.Load<Texture2D>("images/playerSpriteSheetSabbir");
                    texP1Head = game.Content.Load<Texture2D>("images/sabbirLife");
                    nameP1 = "SABBIR";
                }
                if (selectedIndex == 1)
                {
                    texPlayerP1 = game.Content.Load<Texture2D>("images/playerSpriteSheetMargo");
                    texP1Head = game.Content.Load<Texture2D>("images/margoLife");
                    nameP1 = "MARGO";
                }
                if (selectedIndex == 2)
                {
                    texPlayerP1 = game.Content.Load<Texture2D>("images/playerSpriteSheetMatt");
                    texP1Head = game.Content.Load<Texture2D>("images/mattLife");
                    nameP1 = "MATT";
                }
                if (selectedIndex == 3)
                {
                    texPlayerP1 = game.Content.Load<Texture2D>("images/playerSpriteSheetBatman");
                    texP1Head = game.Content.Load<Texture2D>("images/batmanLife");
                    nameP1 = "BAT GUY FURY";
                }

                selectedIndex = MenuComponentP2.SelectedIndex;
                if (selectedIndex == 0)
                {
                    texPlayerP2 = game.Content.Load<Texture2D>("images/playerSpriteSheetSabbir");
                    texP2Head = game.Content.Load<Texture2D>("images/sabbirLife");
                    nameP2 = "SABBIR";
                }
                if (selectedIndex == 1)
                {
                    texPlayerP2 = game.Content.Load<Texture2D>("images/playerSpriteSheetMargo");
                    texP2Head = game.Content.Load<Texture2D>("images/margoLife");
                    nameP2 = "MARGO";
                }
                if (selectedIndex == 2)
                {
                    texPlayerP2 = game.Content.Load<Texture2D>("images/playerSpriteSheetMatt");
                    texP2Head = game.Content.Load<Texture2D>("images/mattLife");
                    nameP2 = "MATT";
                }
                if (selectedIndex == 3)
                {
                    texPlayerP2 = game.Content.Load<Texture2D>("images/playerSpriteSheetBatman");
                    texP2Head = game.Content.Load<Texture2D>("images/BatmanLife");
                    nameP2 = "BAT GUY FURY";
                }

                ActionScene actionScene = new ActionScene(game, spriteBatch, texPlayerP1,
                    texPlayerP2, texP1Head, texP2Head, nameP1, nameP2);
                game.Components.Add(actionScene);
                actionScene.show();
                this.hide();
                this.Enabled = false;
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), Color.White);
            spriteBatch.DrawString(font, directions, new Vector2(25, 300), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
