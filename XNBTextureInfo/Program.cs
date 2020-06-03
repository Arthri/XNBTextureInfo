using System;

namespace XNBTextureInfo
{
    public class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                using (DecoyGame game = new DecoyGame())
                {
                    game.Run();
                }
                Console.WriteLine("Successfully extracted data");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ran into fatal error:\n{e}");
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
