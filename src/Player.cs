using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using MonoGame.Aseprite.Documents;
using MonoGame.Aseprite.Graphics;

namespace Aesprite
{
    public class Player
    {
        private KeyboardState _prekey;
        public Vector2 PlayerVector { get; set; }
        
        private AsepriteDocument _playerAseprite;
        private AnimatedSprite _playerSprite;
        
        enum State
        {
            Walking,
            Jumping
        }
        
        enum Facing
        {
            Left, 
            Right
        }
        
        private State _currentState = State.Walking;
        private Facing _facingState = Facing.Right;
        private Vector2 _startingPosition = Vector2.Zero;
        
        public Player()
        {

        }
        public void LoadContent(ContentManager content, String texture)
        {
            _playerAseprite = content.Load<AsepriteDocument>(texture);
            _playerSprite = new AnimatedSprite(_playerAseprite,PlayerVector);
            _playerSprite.Scale = new Vector2(2, 2);
        }

        public void Update(GameTime gameTime, KeyboardState currentkey)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _playerSprite.Update(deltaTime);
            HandleInput(currentkey,deltaTime);
        }

        private void HandleInput(KeyboardState currentkey, float deltaTime)
        {
            if (currentkey.IsKeyDown(Keys.A))
            {
                _playerSprite.Play("walkleft");
                _currentState = State.Walking;
                _facingState = Facing.Left;
                _playerSprite.X -= 1;
            }
            if (currentkey.IsKeyDown(Keys.W) && currentkey.IsKeyDown(Keys.A))
            {
                _playerSprite.Play("walkleft");
                _currentState = State.Walking;
                _playerSprite.Y -= 1;
            }
            if (currentkey.IsKeyDown(Keys.S) && currentkey.IsKeyDown(Keys.A))
            {
                _playerSprite.Play("walkleft");
                _currentState = State.Walking;
                _playerSprite.Y += 1;
            }
         
            if (currentkey.IsKeyDown(Keys.D))
            {
                _playerSprite.Play("walkright");
                _currentState = State.Walking;
                _facingState = Facing.Right;
                _playerSprite.X += 1;
            }
            
            if (currentkey.IsKeyDown(Keys.W) && currentkey.IsKeyDown(Keys.D))
            {
                _playerSprite.Play("walkright");
                _currentState = State.Walking;
                _playerSprite.Y -= 1;
            }
            if (currentkey.IsKeyDown(Keys.S) && currentkey.IsKeyDown(Keys.D))
            {
                _playerSprite.Play("walkright");
                _currentState = State.Walking;
                _playerSprite.Y += 1;
            }
            
            UpdateJump(currentkey);

            _playerSprite.OnAnimationLoop = () => 
            {
                if (_facingState == Facing.Right)
                {
                    _playerSprite.Play("right");
                }
                else
                {
                    _playerSprite.Play("left");
                }

                _playerSprite.OnAnimationLoop = null;
            };
            _prekey = currentkey;
        }
        
        private void UpdateJump(KeyboardState aCurrentKeyboardState)
        {

            if (_currentState == State.Walking)
            {
                if (aCurrentKeyboardState.IsKeyDown(Keys.Space) && _prekey.IsKeyDown(Keys.Space) == false)
                {
                    if (_currentState != State.Jumping)
                    {
                        _currentState = State.Jumping;
                        _startingPosition = _playerSprite.Position;
                        _playerSprite.Y -= 100;
                        
                        if (_facingState == Facing.Right)
                        {
                            _playerSprite.X += 40;
                            _playerSprite.Play("jumpright");

                        }
                        else
                        {
                            _playerSprite.X -= 40;
                            _playerSprite.Play("jumpleft");

                        }
                    }
                }
            }                    

            if (_currentState == State.Jumping)
            {
                Console.WriteLine(_startingPosition);

                if (_startingPosition.Y - _playerSprite.Y < 150)
                {
                    _playerSprite.Y += 10;
                    Console.WriteLine(_playerSprite.Y);
                    Console.WriteLine(_startingPosition.Y );
                    Console.WriteLine(_startingPosition.Y - _playerSprite.Y);
                }
 
                if (_playerSprite.Y > _startingPosition.Y)
                {
                    _playerSprite.Y = _startingPosition.Y;
                    _currentState = State.Walking;
                    Console.WriteLine(_currentState);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _playerSprite.Render(spriteBatch);
            spriteBatch.End();
        }

    }
}