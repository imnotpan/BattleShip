using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Battleship.src.MainMenu.Buttons.MultiplayerButtons;
using Battleship.src.Networking;
using Battleship.src.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Battleship.src.MainMenu.Buttons.Multiplayer
{
    public class HostButton : TextButtonBase
    {
        GameControllers GameControllers;
        public HostButton(string Text, Vector2 _position, GameControllers GameControllers)
                        : base(Text, _position, GameControllers)
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

                GameControllers.GameNetworking.serverSocket.Start(IP ,PORT);
                GameControllers.MainMenuController.HostServerWaiting();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Host Error: " + ex.Message);
            }
            
            


        }

    }


}
