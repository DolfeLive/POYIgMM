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
        private string value;

        [JsonIgnore]
        private object obj;

        [JsonIgnore]
        private Type type;

        public SerializedConfiggable(string key, object value)
        {
            this.key = key;
            SetValue(value);
        }

        [JsonConstructor]
        protected SerializedConfiggable(string key, string jsonValue)
        {
            this.key = key;
            this.value = jsonValue;
        }

        public string GetSerialized()
        {
            return value;
        }

        public T GetValue<T>()
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public void SetValue(object value)
        {
            this.obj = value;
            this.type = value.GetType();
            this.value = JsonConvert.SerializeObject(value, type: type, formatting: Formatting.Indented, null);
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(value))
                return false;

            if (string.IsNullOrEmpty(key))
                return false;

            return true;
        }
    }
}
