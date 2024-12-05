using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POKModManager
{
    /// <summary>
    /// Mod class to get properties from
    /// </summary>
    public struct Mod
    {
        public ModClass ModClass;
        public string Name;
        public string Version;
        public string Description;
        public string[] Properties;
        public Mod(ModClass mod, string name, string version, string description, string[] properties)
        {
            ModClass = mod;
            Name = name;
            Version = version;
            Description = description;
            Properties = properties;
        }
    }
    /// <summary>
    /// Used for getting range of sliders
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class POKRange : Attribute
    {
        public int min;

        public int max;

        public POKRange(int Max)
        {
            min = 0;
            max = Max;
        }

        public POKRange(int Min, int Max)
        {
            min = Min;
            max = Max;
        }
    }

    /// <summary>
    /// Do not save modified values
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DoNotSave : Attribute
    {
    }

    /// <summary>
    /// If you plan on having public values but dont want to have them all configable
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Editable : Attribute
    {
    }

    public class POKDropdown : List<string>
    {
        public List<string> Properties { get; set; } = new List<string>();
        public int SelectedIndex { get; set; } = 0; // Default to the first index

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class Name : Attribute
    {
        public Name(string name)
        {
            this.name = name;
        }
        public string name;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class Visible : Attribute
    {
        public Visible(bool visible)
        {
            this.visible = visible;
        }
        public bool visible;
    }
}
