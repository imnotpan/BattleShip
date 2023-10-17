using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Battleship.src.Networking;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Battleship.src.MainMenu.Buttons.ConnectionsButtons.Client
{
    public class ClientStartGameButton : TextButtonBase
    {
        GameControllers GameControllers;
        public ClientStartGameButton(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public override void onClick()
        {
            base.onClick();
            
            //var GameNetworking = GameControllers.GameNetworking;
            //var msg = GameNetwor
            //GameNetworking.Client.SendDataToServer(JSON);
            
        }
    }   
}
