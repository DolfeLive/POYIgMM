using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POKModManager;
using UnityEngine;
using UnityEngine.UI;



namespace POKModManager
{
    public class AssetBundleLoader
    {
        AssetBundle DropdownBundle;
        public static GameObject dropdown;
        public AssetBundleLoader()
        {
            string bundlePath = Path.Combine(Paths.DLLLocation, "Dropdown");
            DropdownBundle = AssetBundle.LoadFromFile(bundlePath);
            if (DropdownBundle == null)
            {
                Debug.LogError("Failed to load AssetBundle!");
                return;
            }


            dropdown = DropdownBundle.LoadAsset<GameObject>("Dropdown");
        }


    }
}
