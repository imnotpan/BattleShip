using Battleship.src.Controllers.UI.GameLoopButtons;
using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Battleship.src.MainMenu.Buttons.AbstractClassesButtons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;

namespace Battleship.src.Controllers.UI.GameHud
{
    public class GameHud
    {
        public RedyButton RedyButton;
        public AttackButton AttackButton;

        GameManager GameManager;
        Scene _Scene;

        //Buttons
        Texture2D ShipsRedyTexture;
        Texture2D AttackButtonTexture;

        //Circles
        Texture2D CircleBlueTexture;
        Texture2D CircleRedTexture;

        public CircleEntity CircleEntityPlayer;
        public CircleEntity CircleEntityEnemy;
        public TextEntity middleTextEntity;
        GameControllers GameControllers;

        public GameHud(GameControllers GameControllers)
        {
            this.GameControllers = GameControllers;
            this.GameManager = GameControllers.GameManager;
            this._Scene = GameControllers.Scene;
            Initialize();
        }

        public void Initialize()
        {
            ShipsRedyTexture = _Scene.Content.Load<Texture2D>("Sprites/HUD/GameHud/ShipsInPositionButton");
            AttackButtonTexture = _Scene.Content.Load<Texture2D>("Sprites/HUD/GameHud/AttackButton");

            CircleRedTexture = _Scene.Content.Load<Texture2D>("Sprites/HUD/GameHud/CircleRed");
            CircleBlueTexture = _Scene.Content.Load<Texture2D>("Sprites/HUD/GameHud/CircleBlue");


            var RedyButtonPosition = new Vector2(Constants.PIX_SCREEN_WIDTH - Constants.PIX_SCREEN_WIDTH / 5 - 16,
                                    Constants.PIX_SCREEN_HEIGHT / 2 - 64);
            RedyButton = new RedyButton("SHIPS ON POSITION", RedyButtonPosition, GameControllers);
            
            var AttackButtonPosition = new Vector2(Constants.PIX_SCREEN_WIDTH - Constants.PIX_SCREEN_WIDTH / 5 - 16,
                                    Constants.PIX_SCREEN_HEIGHT / 2 - 64);
            AttackButton = new AttackButton("ATTACK", AttackButtonPosition, GameControllers);
            AttackButton.AddOnScene(GameControllers.Scene);
            AttackButton.setSceneState(false);

            var positionEnemy = new Vector2(Constants.PIX_SCREEN_WIDTH / 2 - 30, Constants.PIX_SCREEN_HEIGHT / 2 - 150);
            CircleEntityEnemy = new CircleEntity(GameControllers, CircleRedTexture, positionEnemy);

            var positionMiddle = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 - 150);
            middleTextEntity = new TextEntity("-", positionMiddle, GameControllers.textFont);

            var positionPlayer = new Vector2(Constants.PIX_SCREEN_WIDTH / 2 + 30, Constants.PIX_SCREEN_HEIGHT / 2 - 150);
            CircleEntityPlayer = new CircleEntity(GameControllers, CircleBlueTexture, positionPlayer);
        }

        public void Update()
        {
            if(CircleEntityEnemy.linkedText != null)
            {
                CircleEntityEnemy.linkedText._textComponent.Text = GameControllers.enemyCountShips.ToString();
            }
            if (CircleEntityPlayer.linkedText != null)
            {
                CircleEntityPlayer.linkedText._textComponent.Text = GameControllers.playerCountShips.ToString();
            }

         
        }
    }
}
