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
            try
            {


                var PORT = Convert.ToInt32(GameControllers.MainMenuController.PORT_CONNECTION);
                var IP = GameControllers.MainMenuController.IP_CONNECTION;

                GameControllers.GameNetworking.clientSocket.Connect(IP, PORT);

                var msg = GameControllers.GameNetworking.GameDataJSON.ClientJSON("s", 0);
                GameControllers.GameNetworking.clientSocket.sendData(msg);


            }
            catch (Exception ex)
            {
                Console.WriteLine("[ vs Bot] " + ex);
            }
        }
    }
}
