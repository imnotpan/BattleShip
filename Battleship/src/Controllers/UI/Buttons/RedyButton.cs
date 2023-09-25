using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Battleship.src.Controllers.UI.Buttons
{
    public class RedyButton : ButtonBase
    {
        GameManager GameManager;
        public RedyButton(Texture2D buttonTexture, Vector2 _position, GameManager GameManager)
                : base(buttonTexture, _position)
        {
            this.GameManager = GameManager;
        }
        public override void onClick()
        {
            base.onClick();
            GameManager.StartGame();
            this.Destroy();

        }
    }
}
