using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;

namespace Battleship.src.MainMenu.Buttons.ConnectionsButtons.Host
{
    internal class CloseServerButton : TextButtonBase
    {
        GameControllers GameControllers;

        public CloseServerButton(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public override void onClick()
        {
            GameControllers.MainMenuController.MainMenuInitialize();
            GameControllers.GameNetworking.serverSocket.Stop();

            //GameControllers.GameNetworking.Server.ServerStop();
        }

    }


}
