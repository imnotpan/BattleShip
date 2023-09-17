using Microsoft.Xna.Framework;

namespace Battleship.src.Networking.NetworkShared
{
    public enum PlayerActions
    {
        PRESS_DOWN, PRESS_UP, PRESS_LEFT, PRESS_RIGHT, PRESS_FIRE
    }
    public class PlayerData
    {
        public PlayerData(Point size)
        {
            Size = size;
            MovementSpeed = 5;
        }
        public int PlayerId { get; set; }
        public string TextureName { get; set; }
        public int Health { get; set; }
        public Point Location;
        public Rectangle BoundingBox { get; set; }
        public Vector2 DirectionVector { get; set; }
        public int MovementSpeed { get; set; }
        public readonly Point Size;
        public bool IsPresent { get; set; }

    }

}
