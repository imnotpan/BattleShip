using Battleship.src.Controllers;
using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Battleship.src.Controllers.UI.GameLoopButtons
{
    public class AttackButton : TextButtonBase
    {
        GameControllers GameControllers;

        public AttackButton(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public override void onClick()
        {
            base.onClick();
            GameControllers.GameStatesSystem.PlayerEndTurn();
        }
    }
}
