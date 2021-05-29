using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using MonoGame.Aseprite.Documents;
using MonoGame.Aseprite.Graphics;

namespace Aesprite
{
    public class Enemy
    {
        public Vector2 EnemyVector  { get; set; }
        
        private AsepriteDocument _Enemyaseprite;
        private AnimatedSprite _Enemysprite;

        public Enemy()
        {

        }
        public void LoadContent(ContentManager content, String texture)
        {
            _Enemyaseprite = content.Load<AsepriteDocument>(texture);
            _Enemysprite = new AnimatedSprite(_Enemyaseprite,EnemyVector);
            _Enemysprite.Scale = new Vector2(2, 2);
        }

        public void Update(GameTime gameTime, KeyboardState currentkey)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _Enemysprite.Update(deltaTime);

            HandleInput(currentkey,deltaTime);
        }

        private void HandleInput(KeyboardState currentkey, float deltaTime)
        {
            
            _Enemysprite.OnAnimationLoop = () => 
            {
                _Enemysprite.Play("right");
                _Enemysprite.OnAnimationLoop = null;
            };
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _Enemysprite.Render(spriteBatch);
            spriteBatch.End();
        }
    }
}