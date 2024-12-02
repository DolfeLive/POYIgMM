using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using POKModManager;
using Newtonsoft.Json;
using Newtonsoft;

namespace POKModManager
{
    public static class Persistance
    {
        public static bool HasKey(string modName, string property)
        {
            
            if (string.IsNullOrWhiteSpace(modName) || string.IsNullOrWhiteSpace(property))
                throw new ArgumentException("modName and property cannot be null or empty.");

            string folderPath = Path.Combine(Paths.DataFolder, modName);
            string filePath = Path.Combine(folderPath, $"{modName}.json");

            // Return false if the directory or file doesn't exist
            if (!Directory.Exists(folderPath) || !File.Exists(filePath))
                return false;

            List<SerializedConfiggable> loadedData;

            try
            {
                // Load and deserialize data
                string json = File.ReadAllText(filePath);
                loadedData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SerializedConfiggable>>(json)
                             ?? new List<SerializedConfiggable>();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading settings for {modName}: {ex.Message}");
                BackupAndResetFile(filePath);
                return false; // Default to false if the file is corrupted
            }

            // Filter valid entries and check for the property
            return loadedData.Where(x => x.IsValid()).Any(x => x.key == property);
        }


        public static void SaveData(string modName, string property, object value)
        {
            if (string.IsNullOrWhiteSpace(modName) || string.IsNullOrWhiteSpace(property) || value == null)
                throw new ArgumentException("modName, property, and value cannot be null or empty.");

            Debug.Log($"Saving: ModName={modName}, Property={property}, Value={value}, Type={value.GetType()}");

            string folderPath = Path.Combine(Paths.DataFolder, modName);
            string filePath = Path.Combine(folderPath, $"{modName}.json");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            List<SerializedConfiggable> loadedData = new List<SerializedConfiggable>();

            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    loadedData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SerializedConfiggable>>(json)
                                 ?? new List<SerializedConfiggable>();
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error loading settings for {modName}: {ex.Message}");
                    BackupAndResetFile(filePath);
                }
            }

            // Check if the property exists and update it, or add a new entry
            var existingEntry = loadedData.FirstOrDefault(x => x.key == property);
            if (existingEntry != null)
            {
                existingEntry.SetValue(value);
            }
            else
            {
                loadedData.Add(new SerializedConfiggable(property, value));
            }

            string updatedJson = Newtonsoft.Json.JsonConvert.SerializeObject(loadedData, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);
            
        }
        
        public static T GetValue<T>(string modName, string property, T defaultValue = default)
        {
            if (string.IsNullOrWhiteSpace(modName) || string.IsNullOrWhiteSpace(property))
                throw new ArgumentException("modName or property cannot be null or empty.");

            string folderPath = Path.Combine(Paths.DataFolder, modName);
            string filePath = Path.Combine(folderPath, $"{modName}.json");

            if (!File.Exists(filePath)) return defaultValue;

            try
            {
                string json = File.ReadAllText(filePath);
                var loadedData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SerializedConfiggable>>(json)
                                 ?? new List<SerializedConfiggable>();

                var entry = loadedData.FirstOrDefault(x => x.key == property);
                if (entry != null)
                {
                    Debug.Log($"Retrieved: ModName={modName}, Property={property}, Value={entry.GetValue<T>()}, Type={typeof(T)}");

                    return entry.GetValue<T>();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error reading value for {property} in {modName}: {ex.Message}");
            }

            Debug.Log($"Retrieved: ModName={modName}, Property={property}, Value={defaultValue}, Type={typeof(T)}");
            return defaultValue;
        }

        private static void BackupAndResetFile(string filePath)
        {
            try
            {
                int count = 0;
                string backupPath;

                do
                {
                    backupPath = filePath + $".backup{(count > 0 ? $" ({count})" : "")}";
                    count++;
                }
                while (File.Exists(backupPath));

                File.Copy(filePath, backupPath);
                File.Delete(filePath);

                Debug.Log($"Backup created: {backupPath}");
            }
            catch (Exception backupEx)
            {
                Debug.LogError($"Failed to back up the corrupted file: {backupEx.Message}");
            }
        }
    }
}
