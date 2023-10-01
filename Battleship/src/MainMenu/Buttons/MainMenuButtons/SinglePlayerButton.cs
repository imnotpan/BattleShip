using Battleship.src.Controllers;
using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;
using System;

namespace Battleship.src.MainMenu.Buttons.MainMenuButtons
{
    public class SinglePlayerButton : TextButtonBase
    {
        GameControllers GameControllers;


        public SinglePlayerButton(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public override void onClick()
        {
            base.onClick();
            GameControllers.MainMenuController.StartGame();
            GameControllers.GameStatesSystem.StartGame();
        }
    }
}
