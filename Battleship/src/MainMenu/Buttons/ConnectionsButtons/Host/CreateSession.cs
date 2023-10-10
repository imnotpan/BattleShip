using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;
using System;

namespace Battleship.src.MainMenu.Buttons.ConnectionsButtons.Host
{
    public class CreateSession : TextButtonBase
    {
        GameControllers GameControllers;
        public CreateSession(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public override void onClick()
        {
            base.onClick();

            var Networking = GameControllers.GameNetworking;
            Networking.Server.GameSessionManager.CreateNewGame(GameControllers);
        }
    }
}
