using Nez;

namespace Battleship.src.MainMenu.Buttons.AbstractClassesButtons
{
     public interface MenuItemsInterface
    {
        Entity _Entity { get;  set; }
        void AddOnScene();
        void DestroyFromScene();
    }
}
