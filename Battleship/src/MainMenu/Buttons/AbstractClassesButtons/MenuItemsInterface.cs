using Nez;

namespace Battleship.src.MainMenu.Buttons.AbstractClassesButtons
{
     public interface MenuItemsInterface
    {
        Entity _Entity { get;  set; }
        void AddOnScene(Scene _Scene);
        void DestroyFromScene();
        void setSceneState(bool state);

    }
}
