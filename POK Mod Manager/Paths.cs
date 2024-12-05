using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace POKModManager
{
    internal static class Paths
    {
        public static string ExecutionPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string GameFolder => Path.GetDirectoryName(Application.dataPath); 


#if BEPINEX
        public static string BepInExFolder => Path.Combine(GameFolder, "BepInEx");
        public static string ConfigFolder => Path.Combine(BepInExFolder, "config");
        public static string DataFolder => Path.Combine(ConfigFolder, "POKModManager");
#elif MELONLOADER
        public static string ConfigFolder => Path.Combine(GameFolder, "UserData");
        public static string DataFolder => Path.Combine(ConfigFolder, "POKModManager");
#endif

#if BEPINEX
        public static string DLLLocation => Path.Combine(BepInExFolder, "plugins", "NewMM");
#elif MELONLOADER
        public static string DLLLocation => Path.Combine(GameFolder, "Mods");
#endif
    }
}
