using Battleship.src.Controllers;
using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Battleship.src.Networking;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Battleship.src.MainMenu.Buttons.MainMenuButtons
{
    public class MultiPlayerButton : TextButtonBase
    {
        GameControllers GameControllers;

        public MultiPlayerButton(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public override void onClick()
        {
            base.onClick();
            //GameControllers.MainMenuController.MultiplayerMenuInitialize();
        }
    }
}
