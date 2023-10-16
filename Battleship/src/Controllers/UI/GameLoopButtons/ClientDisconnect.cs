using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;


namespace Battleship.src.Controllers.UI.GameLoopButtons
{
    public class ClientDisconnect : TextButtonBase
    {

        GameControllers GameControllers;
        public ClientDisconnect(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public override void onClick()
        {
            base.onClick();
            var msg = GameControllers.GameNetworking.GameDataJSON.ClientJSON("d", 0);
            GameControllers.GameNetworking.clientSocket.sendData(msg);

        }
    }
}
