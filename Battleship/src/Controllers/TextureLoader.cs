using Microsoft.Xna.Framework.Graphics;
using Nez.Systems;
using System.Collections.Generic;

namespace Battleship.src.Controllers
{
    public  class TextureLoader
    {
        public Dictionary<string, Texture2D> _gameTextures = new Dictionary<string, Texture2D>();
        private NezContentManager _content;

        public TextureLoader(NezContentManager content) { 
            this._content = content;
        }


        public void loadTexture(string pathTexture)
        {
            var gridTexture = _content.Load<Texture2D>(pathTexture);
            string[] splitText = pathTexture.Split('/');
            string textureName = splitText[splitText.Length - 1];

            _gameTextures.Add(textureName, gridTexture);

        }
    }
}
