using Battleship.src.MainMenu.Buttons.AbstractClasses;
using Microsoft.Xna.Framework;


namespace Battleship.src.Controllers.UI.GameLoopButtons
{
    public class AttackButton : TextButtonBase
    {
        GameControllers GameControllers;

        public AttackButton(string Text, Vector2 _position, GameControllers GameControllers) : base(Text, _position, GameControllers)
        {
            this.GameControllers = GameControllers;
        }

        public override void onClick()
        {
            base.onClick();
            if (GameControllers.playerSelectedGrids.Count > 0)
            {
                GameControllers.GameStatesSystem.PlayerEndTurn();
            }
        }
    }
}
