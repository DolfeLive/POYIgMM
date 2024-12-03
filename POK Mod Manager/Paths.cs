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
        public static string BepInExFolder => Path.Combine(GameFolder, "BepInEx");
        public static string BepInExConfigFolder => Path.Combine(BepInExFolder, "config");
        public static string DataFolder => Path.Combine(BepInExConfigFolder, "POKModManager");

        public static string DLLLocation => Path.Combine(BepInExFolder, "plugins", "NewMM");

        public static void CheckFolders()
        {
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);
        }
    }
}
