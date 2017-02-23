using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace platformer2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Game State
        public enum GameState
        {
            Menu,
            Playing,
            GameOver
        }

        enum Character
        {
            Adam,
            Brian,
            David,
            Matty,
            Mike,
            Rob,
            Sam,
            Sheffield
        }

        // Returns PlayerList to be used for Player Class.
        public List<Player> ReturnPlayerList()
        {
            return Players;
        }
        public List<Projectile> ReturnProjectileList()
        {
            return Projectiles;
        }
        public List<Platform> ReturnPlatformList()
        {
            return Classic.Platforms;
        }

        public GameState currentGameState = GameState.Playing;

        #region Game World

        // Size of the Screen
        public const int GAME_WIDTH = 1920;
        public const int GAME_HEIGHT = 1080;

        // Sprite Lists
        public List<Sprite> Sprites = new List<Sprite>();
        List<Player> Players = new List<Player>();
        public List<Projectile> Projectiles = new List<Projectile>();

        Player player1;
        Player player2;
        Player player3;
        Player player4;
        Projectile player1projectile;
        Projectile player2projectile;
        Projectile player3projectile;
        Projectile player4projectile;

        PlayerOverlay p1Overlay;
        Texture2D p1OverlayTexture;
        PlayerOverlay p2Overlay;
        Texture2D p2OverlayTexture;
        PlayerOverlay p3Overlay;
        Texture2D p3OverlayTexture;
        PlayerOverlay p4Overlay;
        Texture2D p4OverlayTexture;

        Texture2D PlatformTexture;
        Level1_Classic Classic;

        #endregion
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Mouse Settings
            IsMouseVisible = true;

            // Video Settings
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            graphics.PreferredBackBufferWidth = GAME_WIDTH;
            graphics.IsFullScreen = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Textures
            PlatformTexture = Content.Load<Texture2D>("block");

            // Load Levels
            Classic = new Level1_Classic(PlatformTexture);
            Classic.LoadContent(Content);

            // Projectiles
            player1projectile = new Projectile();
            player2projectile = new Projectile();
            player3projectile = new Projectile();
            player4projectile = new Projectile();
            Projectiles.Add(player1projectile);
            Projectiles.Add(player2projectile);
            Projectiles.Add(player3projectile);
            Projectiles.Add(player4projectile);
            Sprites.Add(player1projectile);
            Sprites.Add(player2projectile);
            Sprites.Add(player3projectile);
            Sprites.Add(player4projectile);

            

            // Players - Needs to go last for draw order
            player1 = assignPlayer(Character.Rob, PlayerIndex.One);
            Sprites.Add(player1);
            Players.Add(player1);
            player2 = assignPlayer(Character.Mike, PlayerIndex.Two);
            Sprites.Add(player2);
            Players.Add(player2);
            player3 = assignPlayer(Character.Adam, PlayerIndex.Three);
            Sprites.Add(player3);
            Players.Add(player3);
            player4 = assignPlayer(Character.Matty, PlayerIndex.Four);
            Sprites.Add(player4);
            Players.Add(player4);

            p1OverlayTexture = Content.Load<Texture2D>("Player1Bar");
            p1Overlay = new PlayerOverlay(player1, p1OverlayTexture, new Vector2(0, 0), 1);
            p2OverlayTexture = Content.Load<Texture2D>("Player2Bar");
            p2Overlay = new PlayerOverlay(player2, p2OverlayTexture, new Vector2(1200, 0), 2);
            p3OverlayTexture = Content.Load<Texture2D>("Player3Bar");
            p3Overlay = new PlayerOverlay(player3, p3OverlayTexture, new Vector2(0, 150), 3);
            p4OverlayTexture = Content.Load<Texture2D>("Player4Bar");
            p4Overlay = new PlayerOverlay(player4, p4OverlayTexture, new Vector2(1200, 150), 4);

            
            Sprites.Add(p1Overlay);
            Sprites.Add(p2Overlay);
            Sprites.Add(p3Overlay);
            Sprites.Add(p4Overlay);
            foreach (Sprite s in Sprites)
            {
                s.LoadContent(Content);
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            KeyboardState keystate = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                keystate.IsKeyDown(Keys.Escape))
                this.Exit();

            switch (currentGameState)
            {
                case GameState.Menu:
                    
                    break;
                case GameState.Playing:
                    foreach (Sprite s in Sprites)
                    {
                        s.Update(this);
                    }
                    
                    int livingPlayers = 2;
                    foreach (Player p in Players)
                    {
                        if (p.Lives <= 0)
                        {
                            livingPlayers--;
                        }
                        if (livingPlayers <= 1)
                        {
                            currentGameState = GameState.GameOver;
                        }
                    }
                    break;
                case GameState.GameOver:
                    break;
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            switch (currentGameState)
            {
                case GameState.Menu:
                    break;
                case GameState.Playing:
                    Classic.Draw(spriteBatch);
                    foreach (Sprite s in Sprites)
                        s.Draw(spriteBatch);
                    break;
                case GameState.GameOver:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        Player assignPlayer(Character character, PlayerIndex index)
        {
            Player player;
            Projectile proj;
            if (index == PlayerIndex.One)
                proj = player1projectile;
            else if (index == PlayerIndex.Two)
                proj = player2projectile;
            else if (index == PlayerIndex.Three)
                proj = player3projectile;
            else
                proj = player4projectile;

            switch (character)
            {
                case Character.Adam:
                    return player = new Player_Adam(index, proj);
                case Character.Brian:
                    return player = new Player_Brian(index, proj);
                case Character.David:
                    return player = new Player_DavidParker(index, proj);
                case Character.Matty:
                    return player = new Player_Matty(index, proj);
                case Character.Mike:
                    return player = new Player_Mike(index, proj);
                case Character.Rob:
                    return player = new Player_Rob(index, proj);
                case Character.Sam:
                    return player = new Player_Sam(index, proj);
                case Character.Sheffield:
                    return player = new Player_Sheffield(index, proj);
            }
            return null;
        }
    }
}
