using Microsoft.Xna.Framework;
using Nez;
using System.Collections.Generic;

namespace Battleship.src.Controllers.Ships
{
    public class ShipsDeploy
    {

        TextureLoader TextureLoader;
        Scene _Scene;
        public List<ShipBase> ShipsList = new List<ShipBase>();

        public ShipsDeploy(GameControllers GameControllers) {

            this.TextureLoader = GameControllers.TextureLoader;
            this._Scene = GameControllers._Scene;



            // Definir las posiciones de las naves en la parte derecha de la pantalla
            Vector2 positionBattleShip = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT);
            Vector2 positionCarrier = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT);
            Vector2 positionCruiser = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT);
            Vector2 positionPatrolBoat = new Vector2(Constants.PIX_SCREEN_WIDTH / 2, Constants.PIX_SCREEN_HEIGHT);

            // Crear y agregar las naves al escenario
            ShipBase shipBattleShip = new ShipBase(TextureLoader._gameTextures["ship_BattleShip"], positionBattleShip);
            ShipsList.Add(shipBattleShip);

            ShipBase shipCarrier = new ShipBase(TextureLoader._gameTextures["ship_Carrier"], positionCarrier);
            ShipsList.Add(shipCarrier);

            ShipBase shipPatrolBoat = new ShipBase(TextureLoader._gameTextures["ship_PatrolBoat"], positionPatrolBoat);
            ShipsList.Add(shipPatrolBoat);

            ShipBase shipCruiser = new ShipBase(TextureLoader._gameTextures["ship_Cruiser"], positionCruiser);
            ShipsList.Add(shipCruiser);

        }
    }
}
