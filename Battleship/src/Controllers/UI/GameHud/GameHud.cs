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
        RedyButton RedyButton;
        AttackButton AttackButton;

        GameManager GameManager;
        Scene _Scene;

        //Buttons
        Texture2D ShipsRedyTexture;
        Texture2D AttackButtonTexture;

        //Circles
        Texture2D CircleBlueTexture;
        Texture2D CircleRedTexture;

        private CircleEntity CircleEntityPlayer;
        private CircleEntity CircleEntityEnemy;
        private TextEntity middleTextEntity;
        GameControllers GameControllers;

        public GameHud(GameControllers GameControllers) 
        {
            this.GameControllers = GameControllers;
            this.GameManager = GameControllers.GameManager;
            this._Scene = GameControllers._Scene;
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
            RedyButton.AddOnScene();
        }

        public void Update()
        {
            if(GameManager.GameState == "PREPARATION")
            {
                if (!_Scene.Entities.Contains(RedyButton))
                {
  
                }
            }


            if (GameManager.GameState == "PLAYERTURN" || GameManager.GameState == "ENEMYTURN")
            {
                if (CircleEntityEnemy == null)
                {
                    var positionEnemy = new Vector2(Constants.PIX_SCREEN_WIDTH / 2 - 30, Constants.PIX_SCREEN_HEIGHT / 2 - 150);
                    CircleEntityEnemy = new CircleEntity(GameControllers, CircleRedTexture, positionEnemy);
                    CircleEntityEnemy.AddEntityOnScene();
                }
                else
                {
                    CircleEntityEnemy.linkedText._textComponent.Text = GameManager.enemyShipsPositions.Count.ToString();
                }
                if (middleTextEntity == null)
                {
                    var positionEnemy = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT / 2 - 150);
                    middleTextEntity = new TextEntity("-", positionEnemy, GameControllers);
                    middleTextEntity.GenerateShadowClone();
                    middleTextEntity.AddOnScene();
                }

                if (CircleEntityPlayer == null)
                {
                    var positionEnemy = new Vector2(Constants.PIX_SCREEN_WIDTH / 2 + 30, Constants.PIX_SCREEN_HEIGHT / 2 - 150);
                    CircleEntityPlayer = new CircleEntity(GameControllers, CircleBlueTexture, positionEnemy);
                    CircleEntityPlayer.AddEntityOnScene();
                }
                else
                {
                    CircleEntityPlayer.linkedText._textComponent.Text = GameManager.playerShipsPositions.Count.ToString();

                }

            }
            

            if (GameManager.GameState == "PLAYERTURN")
            {
                if (AttackButton == null)
                {
                    var AttackButtonPosition = new Vector2(Constants.PIX_SCREEN_WIDTH - Constants.PIX_SCREEN_WIDTH / 5 - 16,
                                                        Constants.PIX_SCREEN_HEIGHT / 2 - 64);
                    AttackButton = new AttackButton(AttackButtonTexture, AttackButtonPosition, GameManager);
                    _Scene.AddEntity(AttackButton);
                }
            }
        }
    }
}
