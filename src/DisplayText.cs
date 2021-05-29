using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Aesprite
{
    public class DisplayText
    {
        private SpriteFont _font;
        
        public string Text { get; set; }

        public Vector2 Position
        {
            get;
            set;
        }

        public DisplayText()
        {
        }
        
        public void LoadContent(ContentManager content, String font)
        {
            _font = content.Load<SpriteFont>(font);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, $"{this.Text} " , this.Position, Color.White);
            spriteBatch.End();
        }

    }
}