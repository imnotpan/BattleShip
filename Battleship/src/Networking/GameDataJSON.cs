using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Battleship.src.Networking
{
    public class GameDataJSON
    {

        public class Ship
        {

            public int[] p { get; set; }
            public int[] b { get; set; }
            public int[] s { get; set; }
        }




        public string ClientJSON(string _action, int _bot, List<Vector3>? _shipsPositions = null, Vector2? _ataque = null)
        {

            Ship miBarco = new Ship();
            int[] ataqueJson = new int[] { };

            if (_shipsPositions != null)
            {
                int count = _shipsPositions.Count;

                if (count >= 1)
                {
                    miBarco.p = new int[] { (int)_shipsPositions[0].X, (int)_shipsPositions[0].Y, (int)_shipsPositions[0].Z };
                }

                if (count >= 2)
                {
                    miBarco.b = new int[] { (int)_shipsPositions[1].X, (int)_shipsPositions[1].Y, (int)_shipsPositions[1].Z };
                }

                if (count >= 3)
                {
                    miBarco.s = new int[] { (int)_shipsPositions[2].X, (int)_shipsPositions[2].Y, (int)_shipsPositions[2].Z };
                }
            }

            if (_ataque != null)
            {
                ataqueJson = new int[] { (int)_ataque.Value.X, (int)_ataque.Value.Y };

            }
    
            var json = new
            {
                action = _action,
                bot = _bot,
                ships = miBarco,
                position = ataqueJson
            };


            string jsonString = JsonConvert.SerializeObject(json);
            return jsonString;
        }

        public string ServerJSON(string _action, int _status, Vector2? _position = null)
        {
            int[] varPosition = new int[] { };
            if (_position != null)
            {
                varPosition = new int[] { (int)_position.Value.X, (int)_position.Value.Y };

            }
            var json = new
            {
                action = _action,
                status = _status, // 0 or 1
                position = varPosition,
            };


            string jsonString = JsonConvert.SerializeObject(json);
            return jsonString;

        }
    }


}
