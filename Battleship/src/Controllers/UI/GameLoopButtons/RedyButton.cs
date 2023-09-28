using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Battleship.src.Controllers.UI.GameLoopButtons
{
    public class RedyButton : TextButtonBase
    {
        GameManager GameManager;

        public RedyButton(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameManager = GameControllers.GameManager;
            Console.WriteLine("REDY BUTTON");
        }

        public override void onClick()
        {
            base.onClick();
            GameManager.StartGame();
            Destroy();

        }
    }
}
