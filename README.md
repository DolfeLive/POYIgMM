# FORKED FROM https://github.com/KinexDev/Poky-Mod-Manager

I will try to keep this as compatible with old mods as possible

# Poky Mod Manager
Poky is a simple mod manager for peaks of yore made in bepinex, it allows enabling, disabling and configurating mods easily.

Poky is still in development so expect bugs.

![](https://github.com/DolfeLive/Poky-Mod-Manager/blob/main/POKManager.gif)

# Documentation

Template for poky mods is available here. [![resource](resource)](https://github.com/DolfeLive/Poky-Mod-Manager/blob/main/POK%20Mod%20Manager/Example.cs)

If you need help setting up bepinex look at this [![resource](resource)](https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/1_setup.html).

You will also need to add a reference to the Poky Mod Manager dll.

To create a mod, you will need to create a new mod class.

```cs
using POKModManager;

namespace Mod
{
    public class TemplateMod : ModClass
    {
    }
}
```

the mod class has a field called "RunUpdateOnMenu", this will tell the mod manager if to run update in the menu, and another field called "Enabled", this is just says if the mod is enabled or not.

the mod manager can override many built in functions into mod class,

```
void OnEnabled() <- called when the game is started/when the mod is enabled
        
void OnDisabled() <- called when the game is started/when the mod is disabled
        
void SceneChange(int sceneIndex) <- called on scene change, is ran even if the mod is disabled
        
void GUIUpdate() <- called on OnGUI
        
void Start() <- called on start
        
void Update(float deltaTime) <- called on update
        
void FixedUpdate(float deltaTime) <- called on fixed update
```

you can also print with 3 different log types,

```
print()

printWarning()

printError()
```

and then you will need to register the new mod with using bepinex.

IT MUST BE REGISTERED IN START NOT AWAKE!!!

```cs
using BepInEx;
using POKModManager;

namespace Mod
{
    [BepInPlugin(MOD_GUID, MOD_NAME, MOD_VERSION)]
    public class Mod : BaseUnityPlugin
    {
        private void Start()
        {
            POKManager.RegisterMod(new TemplateMod(), MOD_NAME, MOD_VERSION, MOD_DESCRIPTION);
        }
    }
}
```

POKManager.RegisterMod is a function that adds the mod into the mod manager.

It has 4 parameters,

The mod (ModClass),

mod name (string),

mod version (string),

mod description (string), 

the description can only go up to 103 characters,

The currently supported data types: - Ints, floats, booleans, strings, UnityEvents and Dropdowns.

```cs
using POKModManager;

namespace Mod
{
    public class TemplateMod : ModClass
    {
            [POKRange(0, 10)] public int TestInt { get; set; }
            [POKRange(0, 10)] public float TestFloat { get; set; }
            public bool TestBool { get; set; }
            public string TestString { get; set; } = "hello :D";
            public UnityEvent TestButton { get; set; } = new UnityEvent();
            public POKDropdown dropdown { get; set; } = new POKDropdown { Properties = new List<string> { "val1", "val2", "val3", "val4", "val5" } };

            // Example button use case
            public override void Start()
            {
                TestButton.AddListener(OnButton);
            }
            
            void OnButton()
            {
                Debug.Log("Button pressed!");
            }
    }
}
```

Ints and floats are required to have the attribute "POKRange", it has 2 parameters, min and max.

There is also another attribute called "DoNotSave", this will not save the modified values.

As well as "Editable" (Explained later on)

There are 3 ways to register the mod, the legacy way, automatic and Explicit
```cs
using BepInEx;
using POKModManager;

namespace Mod
{
    [BepInPlugin(MOD_GUID, MOD_NAME, MOD_VERSION)]
    public class Mod : BaseUnityPlugin
    {
        private void Start()
        {
            // Legacy
            POKManager.RegisterMod(new TemplateMod(), MOD_NAME, MOD_VERSION, MOD_DESCRIPTION, "TestInt", "TestFloat", "TestBool", "TestButton", "TestString");

            // Automatic
            POKManager.RegisterMod(new TemplateMod(), MOD_NAME, MOD_VERSION, MOD_DESCRIPTION);

            // Explicit
            POKManager.RegisterMod(new TemplateMod(), MOD_NAME, MOD_VERSION, MOD_DESCRIPTION, UseEditableAttributeOnly=true);
            // Note that with explicit all variables that you want the player to be able to change must have [Editable] before it
            // Example: [Editable] public bool TestBool { get; set; }
        }
    }
}
```

and thats then the rest is up to you. 👋
