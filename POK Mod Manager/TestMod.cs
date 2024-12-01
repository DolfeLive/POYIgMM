using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POKModManager;
using UnityEngine.Events;
using UnityEngine;

namespace TestMod
{
    [BepInPlugin("Test", "test", "1.0.0")]
    public class TestMod : BaseUnityPlugin
    {
        void Start()
        {
            POKManager.RegisterMod(new actualtestMod(), "test", "test", "test", "TestInt", "TestFloat", "TestBool", "TestButton");
        }
    }

    public class actualtestMod : ModClass
    {
        [POKRange(0, 10)] public int TestInt { get; set; }
        [POKRange(0, 10)] public float TestFloat { get; set; }
        public bool TestBool { get; set; }

        public UnityEvent TestButton { get; set; } = new UnityEvent();

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
