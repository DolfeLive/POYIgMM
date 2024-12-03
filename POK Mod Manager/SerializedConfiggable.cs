using Newtonsoft.Json;
using System;

namespace POKModManager
{
    [Serializable]
    public class SerializedConfiggable
    {
        [JsonProperty]
        public string key { get; }

        [JsonProperty]
        private object value;

        [JsonIgnore]
        private Type type;

        public SerializedConfiggable(string key, object value)
        {
            this.key = key;
            SetValue(value);
        }


        public string GetSerialized()
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }

        public T GetValue<T>()
        {
            if (value == null)
                return default;

            if (typeof(T) == typeof(int))
                return (T)(object)Convert.ToInt32(value);
            if (typeof(T) == typeof(float))
                return (T)(object)Convert.ToSingle(value);
            if (typeof(T) == typeof(bool))
                return (T)(object)Convert.ToBoolean(value);
            if (typeof(T) == typeof(string))
                return (T)(object)value.ToString();

            return (T)value;
        }

        public void SetValue(object value)
        {
            this.value = value;
            this.type = value.GetType();
        }

        public bool IsValid()
        {
            return value != null && !string.IsNullOrEmpty(key);
        }
    }
}
