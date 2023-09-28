using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Battleship.src.Networking;
using Battleship.src.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battleship.src.MainMenu.Buttons.Multiplayer
{
    public class HostButton : TextButtonBase
    {
        GameNetworking GameNetworking { get; set; }
        public HostButton(string Text, Vector2 _position, GameControllers GameControllers)
                        : base(Text, _position, GameControllers)
        {
            GameNetworking = GameControllers.GameNetworking;
        }
        public override void onClick()
        {
            base.onClick();
            GameNetworking.HostServer();

        }

    }


}
