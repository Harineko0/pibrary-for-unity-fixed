using Pibrary.Config;

namespace Pibrary
{
    public static class Constant
    {
        private readonly static string ROOT_PATH = "Pibrary/";
        
        public static string getAssetPath(string name)
        {
            return ROOT_PATH + name;
        }
    }
}