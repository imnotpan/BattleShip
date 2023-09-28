using Battleship.src.Controllers;
using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Battleship.src.Scenes;
using Microsoft.Xna.Framework;
using Nez;
using System;

namespace Battleship.src.MainMenu.Buttons.Multiplayer
{
    public class ConnectButton : TextButtonBase
    {
        GameManager GameManager;
        public ConnectButton(string Text, Vector2 _position, GameControllers GameControllers)
                        : base(Text, _position, GameControllers)
        {
            GameManager = GameControllers.GameManager;
        }

        public override void onClick()
        {

            Console.WriteLine(GameManager.IPCONNECTION);


        }
    }
}
