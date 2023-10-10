using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Battleship.src.MainMenu.Buttons.Multiplayer
{
    public class ConnectButton : TextButtonBase
    {
        GameControllers GameControllers;
        public ConnectButton(string Text, Vector2 _position, GameControllers GameControllers)
                        : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public override void onClick()
        {
            
            var IP = GameControllers.MainMenuController.IP_CONNECTION;
            var PORT = 0;
            try
            {
                PORT = int.Parse(GameControllers.MainMenuController.PORT_CONNECTION);
            }
            catch (Exception ex) {
                Console.WriteLine("Error de conexión: " + ex.Message);
                return;
            }

            if (IP == "")
            {
                IP = "localhost";
            }
            Console.WriteLine("IP: { " + IP + " }" + " PORT { " + PORT + " }");
            GameControllers.GameNetworking.Client.Connect(IP, PORT, int.Parse(GameControllers.MainMenuController.GAMESESSIONID));
            

            //GameControllers.GameNetworking.clientSocket.Connect(GameControllers);

        }
    }
}
