using System;

namespace ButtonMenu
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MenuTemplate game = new MenuTemplate())
            {
                game.Run();
            }
        }
    }
#endif
}

