using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;


namespace Battleship.src.MainMenu.Buttons.ConnectionsButtons.Client
{
    public class DisconnectButton : TextButtonBase
    {
        GameControllers GameControllers;
        public DisconnectButton(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }
        public override void onClick()
        {
            base.onClick();
            GameControllers.MainMenuController.MainMenuInitialize();
           
            GameControllers.GameNetworking.clientSocket.Disconnect();
        }
    }
}
