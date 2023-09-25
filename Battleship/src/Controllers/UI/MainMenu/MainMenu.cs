using Battleship.src.Controllers.UI.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace Battleship.src.Controllers.UI.MainMenu
{
    public class MainMenu
    {
        GameManager GameManager;
        Scene Scene;
        MultiPlayerButton MultiPlayerButton;
        SinglePlayerButton SinglePlayerButton;

        public MainMenu(Scene Scene, GameManager GameManager) 
        {
            this.GameManager = GameManager; 
            this.Scene = Scene;
            Initialize();
        }

        public void Initialize()
        {
            Texture2D singlePlayerTextureButton = Scene.Content.Load<Texture2D>("Sprites/HUD/MainMenu/SinglePlayer");
            Texture2D multiPlayerTextureButton = Scene.Content.Load<Texture2D>("Sprites/HUD/MainMenu/Multiplayer");

            //SinglePlayerButton
            var singlePlayerButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2);
            SinglePlayerButton = new SinglePlayerButton(singlePlayerTextureButton, singlePlayerButtonPos, GameManager);
            Scene.AddEntity(SinglePlayerButton);

            //MultiPlayerButton
            var multiPlayerButtonPos = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 + 32);
            MultiPlayerButton = new MultiPlayerButton(multiPlayerTextureButton, multiPlayerButtonPos);
            Scene.AddEntity(MultiPlayerButton);
        }
        public void Update()
        {
            if(GameManager.GameState != "MAINMENU") 
            { 
                if(!MultiPlayerButton.IsDestroyed) { MultiPlayerButton.onDestroy(); }
                if (!SinglePlayerButton.IsDestroyed) { SinglePlayerButton.onDestroy(); }
            }
        }
    }
}
