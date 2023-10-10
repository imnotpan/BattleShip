using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;

namespace Battleship.src.MainMenu.Buttons.MultiplayerButtons
{
    public class BackTextButton : TextButtonBase
    {
        GameControllers GameControllers;
        public BackTextButton(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;

        }

        public override void onClick()
        {
            GameControllers.MainMenuController.MainMenuInitialize();
        }
    }
}
