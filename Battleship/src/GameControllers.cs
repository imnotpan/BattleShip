using Battleship.src.Controllers;
using Battleship.src.Controllers.Enemy;
using Battleship.src.Controllers.Ships;
using Battleship.src.Controllers.UI.GameHud;
using Battleship.src.MainMenu;
using Battleship.src.Networking;
using Nez;

namespace Battleship.src
{
    public class GameControllers
    {


        public GameManager GameManager;
        public TextureLoader TextureLoader;
        public MainMenuController MainMenuController;
        public GameHud GameHud;
        public GameNetworking GameNetworking;
        public ShipsDeploy ShipsDeploy;

        //GameController
        public playerBoard PlayerBoard;
        public EnemyIA EnemyIA;

        public Scene _Scene;

        public GameControllers(Scene _Scene)
        {
            this._Scene = _Scene;
            TextureLoader = new TextureLoader(_Scene.Content);
            LoadTextures();

            // Logica del juego
            PlayerBoard = new playerBoard(this);
            ShipsDeploy = new ShipsDeploy(this);


            //EnemyIA = new EnemyIA(this);
            //MainMenuController = new MainMenuController(this);
            //GameHud = new GameHud(this);            

            // Juego
            GameManager = new GameManager(this);
            MainMenuController = new MainMenuController(this);  


            //GameNetworking = new GameNetworking(this);

        }

        public void LoadTextures()
        {
            /* Load Texture */
            TextureLoader.loadTexture("Sprites/Celda");
            TextureLoader.loadTexture("Sprites/GridEnemy");
            TextureLoader.loadTexture("Sprites/ship_BattleShip");
            TextureLoader.loadTexture("Sprites/ship_Carrier");
            TextureLoader.loadTexture("Sprites/ship_Cruiser");
            TextureLoader.loadTexture("Sprites/ship_PatrolBoat");
            TextureLoader.loadTexture("Sprites/Flag");


        }



        public void Update()
        {
            //MainMenuController.Update();
        }
    }
}
