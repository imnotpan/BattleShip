using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;
using System;

namespace Battleship.src.MainMenu.Buttons.ConnectionsButtons.Host
{
    public class ServerStartGameButton : TextButtonBase
    {
        GameControllers GameControllers;
        public ServerStartGameButton(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public override void onClick()
        {
            base.onClick();

            var MainMenu = GameControllers.MainMenuController;
            var Networking = GameControllers.GameNetworking;
            /*
            if (GameControllers.GameNetworking.Server.ClientIsReady)
            {
                
                GameControllers.GameStatesSystem.StartGame();
                MainMenu.StartGame();
                

                var JSON = GameControllers.GameDataJSON.ClientJSON("c", 0);
                Networking.Server.SendDataToClient(Networking.Server.server.FirstPeer, JSON);

                Console.WriteLine("[ SERVER ] StartingGame");


            }
            */
        }
    }
}
