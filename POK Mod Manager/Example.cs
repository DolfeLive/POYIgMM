using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POKModManager;

using UnityEngine.Events;
using UnityEngine;
using System.Runtime.InteropServices;

namespace TestMod
{
    [BepInPlugin("Test", "test", "1.0.0")]
    public class TestMod : BaseUnityPlugin
    {
        void Start()
        {
            POKManager.RegisterMod(new actualtestMod(), "test", "test", "test");//, "TestInt", "TestFloat", "TestBool", "TestButton", "TestString");
        }
    }

    public class actualtestMod : ModClass
    {
        [POKRange(0, 10)] public int TestInt { get; set; }
        [POKRange(0, 10)] public float TestFloat { get; set; }
        [Editable] public bool TestBool { get; set; }
        public string TestString { get; set; } = "hello :D";
        public UnityEvent TestButton { get; set; } = new UnityEvent();

        public POKDropdown dropdown { get; set; } = new POKDropdown { Properties = new List<string> { "val1", "val2", "val3", "val4", "val5" } };

        public override void Start()
        {
            TestButton.AddListener(OnButton); 
        }

        void OnButton()
        {
            Debug.Log("Button pressed! ");
        }

    }
}
