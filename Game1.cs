using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Aesprite
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private KeyboardState _currentkey;

        private Player player = new Player();

        private Enemy enemy = new Enemy();

        private DisplayText _playerDisplayText = new DisplayText();
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.PreferredBackBufferWidth = 800;
            
            // _graphics.PreferredBackBufferHeight = 1080;
            // _graphics.PreferredBackBufferWidth = 1920;
            Window.AllowUserResizing = true;
            //this.graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            
            base.Initialize();
        }

        protected override void Initialize()
        {
            _playerDisplayText.Position = new Vector2(200, 100);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice); 
            player.LoadContent( Content, "Pikachu" );
            player.PlayerVector = new Vector2(200, 300);
            
            enemy.LoadContent(Content, "Pikachu");
            enemy.EnemyVector = new Vector2(400, 300);
            
            _playerDisplayText.LoadContent(Content,"default");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            _playerDisplayText.Text = $"Player: {player}";
            
            _currentkey = Keyboard.GetState();

            player.Update(gameTime,_currentkey);
            enemy.Update(gameTime,_currentkey);

            base.Update(gameTime);
        }
    
        protected override void Draw(GameTime gameTime)
        { 
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            player.Draw(_spriteBatch);
            enemy.Draw(_spriteBatch);
            _playerDisplayText.Draw(_spriteBatch);
            
            base.Draw(gameTime);
        }
    }
}
