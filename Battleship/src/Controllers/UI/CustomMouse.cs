

namespace Battleship.src.Controllers.UI
{
    public class CustomCursor
    {
        private static CustomCursor _instance;


        private CustomCursor()
        { 

        }

        public void update()
        {

        }
        public static CustomCursor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomCursor();
                }
                return _instance;
            }
        }

    }
}
