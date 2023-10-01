using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;

namespace Battleship.src.MainMenu.Buttons.MultiplayerButtons
{
    public class IPTextButton : TextButtonBase
    {
        GameControllers GameControllers;

        public IPTextButton(string Text, Vector2 _position, GameControllers GameControllers) 
                            : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public override void onClick()
        {
            base.onClick();
            GameControllers.MainMenuController.writeTargetButton = this;
            GameControllers.MainMenuController.ClikedTextButton = "IP";

        }
        public override void Update()
        {
            base.Update();
            if (this._textEntity._textComponent.Text == "")
            {
                this._textEntity._textComponent.Text = "INSERT IP";
            }
        }
    }
}
