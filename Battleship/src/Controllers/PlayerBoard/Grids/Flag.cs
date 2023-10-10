using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace Battleship.src.Controllers.Grids
{

    public class Flag :Entity
    {
        private SpriteRenderer SpriteRenderer { get; set; }
        public Flag(Texture2D cellTexture, Vector2 position) 
        {
            SpriteRenderer = new SpriteRenderer(cellTexture);
            SpriteRenderer.RenderLayer = -3;
            SpriteRenderer.Origin = new Vector2(cellTexture.Width / 2, cellTexture.Height / 2);
            AddComponent(SpriteRenderer);
            Position = position;
        }
    }

}
