using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Battleship.src.Controllers.UI.GameLoopButtons
{
    public class RedyButton : TextButtonBase
    {
        GameControllers GameControllers;

        public RedyButton(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public override void onClick()
        {
            base.onClick();
            GameControllers.GameStatesSystem.ShipsReady();
            Destroy();

        }

    }
}
