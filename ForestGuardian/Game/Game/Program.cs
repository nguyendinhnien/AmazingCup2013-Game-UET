using System;

namespace Forest
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ForestGuardian game = new ForestGuardian())
            {
                game.Run();
            }
        }
    }
#endif
}

