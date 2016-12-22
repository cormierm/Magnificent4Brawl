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
    public class Character : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Variables, Constants and Properties
        private Game game;
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Texture2D texProjectile;
        public Vector2 position;
        private string name;
        private Color color;
        private Rectangle rect;
        private bool isJumping;
        private bool canShoot;
        private int shotTimer;
        private const int SHOTDELAY = 50;
        private bool canKick;
        private int kickTimer;
        private int deadTimer;
        private int godTimer;
        private bool isDead;
        private bool isGod;
        private const int DEADDELAY = 300;
        private const int KICKDELAY = 50;
        private const int GODDELAY = 300;
        private const int BLINKDELAY = 3;
        private int blinkTimer;
        public Vector2 velocity;
        private const float MOVESPEED = 0.3f;
        private const float JUMPHEIGHT = 7f;
        private const float FALLSPEED = 0.25f;
        private const float SLOWMOVEMENT = 0.95f;
        private float rotation;
        public int fallRight, fallLeft;
        Keys keyMoveLeft, keyMoveRight, keyJump, keyShot, keyKick;
        private int health;
        private int lives;
        private Vector2 projSpeed = new Vector2(7, 0);
        private bool facingRight;
        private SpriteEffects spriteEffect;
        private int playerHeight;
        private int playerWidth;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int frameStart = 0;
        private int frameLast = 4;
        private bool frameRepeatOnce;
        private double delay = 5;
        private int delayCounter;
        private const int ROW = 2;
        private const int COL = 12;
        private SoundEffect kickSound;
        private SoundEffect swooshSound;
        private SoundEffect deadSound;
        private SoundEffect gruntSound;


        public Rectangle Rect
        {
            get
            {
                return rect;
            }

            set
            {
                rect = value;
            }
        }

        public bool IsJumping
        {
            get
            {
                return isJumping;
            }

            set
            {
                isJumping = value;
            }
        }
        

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }


        public int Lives
        {
            get
            {
                return lives;
            }

            set
            {
                lives = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        #endregion


        public Character(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Keys keyMoveLeft, Keys keyMoveRight, Keys keyJump, Keys keyShot, Keys keyKick,
            string name,
            SoundEffect kickSound,
            SoundEffect swooshSound,
            SoundEffect deadSound,
            SoundEffect gruntSound) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.keyMoveLeft = keyMoveLeft;
            this.keyMoveRight = keyMoveRight;
            this.keyJump = keyJump;
            this.keyShot = keyShot;
            this.keyKick = keyKick;
            this.name = name;
            this.kickSound = kickSound;
            this.swooshSound = swooshSound;
            this.deadSound = deadSound;
            this.gruntSound = gruntSound;



            spriteEffect = SpriteEffects.None;
            isJumping = true;
            facingRight = true;
            rotation = 0;
            health = 100;
            color = Color.White;

            playerWidth = tex.Width / COL;
            playerHeight = tex.Height / ROW;
            rect = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);

            canShoot = true;
            canKick = true;
            texProjectile = game.Content.Load<Texture2D>("images/NinjaStar");

            this.Enabled = true;

            this.Visible = true;

            dimension = new Vector2(tex.Width / COL, tex.Height / ROW);
            createFrames();

            isDead = true;
            deadTimer = 0;
            health = 0;
            lives = 3;
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
            if (isDead || lives < 0)
            {
                deadTimer++;
                
                if (deadTimer > DEADDELAY)
                {
                    isDead = false;
                    velocity = new Vector2(0, -3);
                    position = new Vector2(Shared.stage.X / 2 - Shared.campos.X, -tex.Height);
                    rotation = 0;
                    isGod = true;
                    godTimer = 0;
                    color = Color.Red;
                }
            }
            else
            {
                if (isGod)
                {
                    godTimer++;
                    health = 100;
                    if (godTimer > GODDELAY)
                    {
                        isGod = false;
                        color = Color.White;
                    }
                    else
                    {
                        blinkTimer++;
                        if (blinkTimer > BLINKDELAY)
                        {
                            if (this.color == Color.White)
                            {
                                this.color = Color.Red;
                            }
                            else
                            {
                                this.color = Color.White;
                            }
                            blinkTimer = 0;
                        }
                    }
                }
                if (health < 1 || position.Y > Shared.stage.Y)
                {
                    Death death = new Death(game, spriteBatch, position);
                    game.Components.Add(death);
                    isDead = true;
                    deadTimer = 0;
                    health = 0;
                    lives--;
                    deadSound.Play();
                }
                if (position.X + Shared.campos.X > Shared.stage.X - 200)
                {
                    Shared.campos.X -= 4;
                }
                if (position.X + Shared.campos.X < 200)
                {
                    Shared.campos.X += 4;
                }
                delayCounter++;
                if (delayCounter > delay)
                {
                    frameIndex++;
                    if (frameIndex > frameLast)
                    {
                        frameRepeatOnce = false;
                        frameIndex = frameStart;
                        this.Enabled = true;
                        this.Visible = true;
                    }
                    delayCounter = 0;
                }

                KeyboardState ks = Keyboard.GetState();

                if (ks.IsKeyDown(keyJump) && isJumping == false)
                {
                    velocity.Y -= JUMPHEIGHT;
                    isJumping = true;
                    frameRepeatOnce = true;
                    frameIndex = 8;
                    frameStart = 8;
                    frameLast = 9;
                    delay = 15;
                }

                if (Math.Abs(velocity.X) > (MOVESPEED - 0.05f))
                {
                    velocity.X *= SLOWMOVEMENT;
                    if (frameRepeatOnce == false)
                    {
                        if (frameIndex < 1 || frameIndex > 4) frameIndex = 1;
                        frameStart = 1;
                        frameLast = 4;
                        delay = 5;
                    }
                   
                }
                else
                {
                    velocity.X = 0f;
                    if (frameRepeatOnce == false)
                    {
                        frameIndex = 0;
                        frameStart = 0;
                        frameLast = 0;
                        delay = 5;
                    }
                    
                }

                if (isJumping == true)
                {
                    velocity.Y += FALLSPEED;

                }
                if (isJumping == false)
                {

                    velocity.Y = 0f;
                   
                }

                position += velocity;

                if (position.X < fallLeft - playerHeight / 2 || position.X > fallRight - playerWidth / 2)
                {
                    isJumping = true;
                }
                if (ks.IsKeyDown(keyMoveLeft))
                {
                    velocity.X -= MOVESPEED;
                }
                if (ks.IsKeyDown(keyMoveRight))
                {
                    velocity.X += MOVESPEED;
                }

                #region Handles Projectile logic
                if (!canShoot)
                {
                    shotTimer++;
                    if (shotTimer > SHOTDELAY)
                    {
                        canShoot = true;
                    }
                }
                else if (ks.IsKeyDown(keyShot))
                {
                    Projectile shot;
                    if (facingRight)
                    {
                        shot = new Projectile(game, spriteBatch, texProjectile, position, projSpeed, this);
                    }
                    else
                    {
                        shot = new Projectile(game, spriteBatch, texProjectile, position, -projSpeed, this);
                    }
                    frameRepeatOnce = true;
                    frameIndex = 9;
                    frameStart = 9;
                    frameLast = 10;
                    delay = 5;
                    canShoot = false;
                    shotTimer = 0;
                    swooshSound.Play();
                    game.Components.Add(shot);
                }
                #endregion

                if (!canKick)
                {
                    kickTimer++;
                    if (kickTimer > KICKDELAY)
                    {
                        canKick = true;
                    }
                }
                else if (ks.IsKeyDown(keyKick) && canKick == true)
                {
                    frameRepeatOnce = true;
                    frameIndex = 20;
                    frameStart = 20;
                    frameLast = 20;
                    delay = 30;
                    canKick = false;
                    kickTimer = 0;
                    gruntSound.Play();
                    foreach (Character player in Shared.players)
                    {
                        if (player != this)
                        {
                            if (rect.Intersects(player.Rect))
                            {
                                Random rnd = new Random();
                                player.isJumping = true;
                                player.Health -= rnd.Next(15, 35);
                                kickSound.Play();
                                if (facingRight)
                                {
                                    player.velocity += new Vector2(7, -10);
                                }
                                else
                                {
                                    player.velocity += new Vector2(-7, -10);
                                }

                            }
                        }
                    }
                    
                }

                if (velocity.X > 0)
                {
                    facingRight = true;
                    spriteEffect = SpriteEffects.None;
                }
                else if (velocity.X < 0)
                {
                    facingRight = false;
                    spriteEffect = SpriteEffects.FlipHorizontally;
                }

                rect = new Rectangle((int)position.X, (int)position.Y, playerWidth, playerHeight);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0 && isDead == false)
            {
                spriteBatch.Draw(tex, position + Shared.campos, frames[frameIndex], color, rotation, Vector2.Zero, 1f, spriteEffect, 1f);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
