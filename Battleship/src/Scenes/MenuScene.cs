using Battleship.src.Networking;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace Battleship.src.Scenes
{
    public class MenuScene : Scene
    {
        Server _servidor;
        Client _cliente;

        public override void Initialize()
        {
            /*
            _servidor = new Servidor();
            _cliente = new Cliente();

            Texture2D singlePlayerTextureButton = Content.Load<Texture2D>("Sprites/HUD/MainMenu/SinglePlayer");
            Texture2D multiPlayerTextureButton = Content.Load<Texture2D>("Sprites/HUD/MainMenu/Multiplayer");

            //SinglePlayerButton
            var singlePlayerButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH/2, Constants.PIX_SCREEN_HEIGHT/2);
            SinglePlayerButton entitySinglePlayerButton = new SinglePlayerButton(singlePlayerTextureButton, singlePlayerButtonPos);
            AddEntity(entitySinglePlayerButton);

            //MultiPlayerButton
            var multiPlayerButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 + 32);
            MultiPlayerButton entityMultiPlayerButton = new MultiPlayerButton(multiPlayerTextureButton, multiPlayerButtonPos);
            AddEntity(entityMultiPlayerButton);
            */
        }

    }
}

