using Battleship.src.Controllers;
using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Battleship.src.Controllers.UI.GameLoopButtons
{
    public class AttackButton : ButtonBase
    {
        GameManager GameManager;
        public AttackButton(Texture2D buttonTexture, Vector2 _position, GameManager GameManager)
                : base(buttonTexture, _position)
        {
            this.GameManager = GameManager;
        }
        public override void onClick()
        {
            base.onClick();
            GameManager.EndTurn();
        }
    }
}
