
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Battleship.src.Controllers.UI.Buttons
{
    public class SinglePlayerButton : ButtonBase
    {
        GameManager GameManager;
        public SinglePlayerButton(Texture2D buttonTexture, Vector2 _position, GameManager GameManager) 
                : base(buttonTexture, _position)
        {
            this.GameManager = GameManager;
        }
        public override void onClick()
        {
            base.onClick();
            GameManager.GameState = "STARTING";
            
        }
    }
}
