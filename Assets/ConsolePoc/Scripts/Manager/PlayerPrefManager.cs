using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Nagih
{
    public class PlayerPrefManager : MonoBehaviour
    {
        private DataSelf _dataSelf;
        private Dictionary<string, (Type, object)> _populateTestUserData;

        private void Awake()
        {
            _populateTestUserData = new Dictionary<string, (Type, object)>();
            _populateTestUserData["CountDictionary"] = (typeof(List<int>), TestUserData.GetDefaultCountDictionary());
        }

        public T LoadTable<T>() where T : new()
        {
            string key = GetKey<T>();
            if (!PlayerPrefs.HasKey(key))
            {
                return default;
            }

            string content = Encryption.Decrypt(PlayerPrefs.GetString(key));
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
                return JsonConvert.DeserializeObject<T>(content, settings);
            }
            catch
            {
                // If it's in this section, it's trying to deconvert outdated JSON
                Dictionary<string, dynamic> objectMap = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(content);
                Dictionary<string, FieldInfo> fieldMap = new Dictionary<string, FieldInfo>();
                T data = new T();
                foreach (FieldInfo field in typeof(T).GetFields())
                    fieldMap.Add(field.Name, field);

                Dictionary<string, (Type type, object value)> populateData = GetPopulateData<T>();
                foreach (KeyValuePair<string, dynamic> kvp in objectMap)
                {
                    Type fielfType = fieldMap[kvp.Key].FieldType;
                    if (populateData.ContainsKey(kvp.Key))
                    {
                        if (populateData[kvp.Key].type.IsInstanceOfType(kvp.Value))
                        {
                            fieldMap[kvp.Key].SetValue(data, kvp.Value);
                        }
                        else
                        {
                            fieldMap[kvp.Key].SetValue(data, populateData[kvp.Key].value);
                        }
                    }
                    else
                    {
                        if (kvp.Value is Array || kvp.Value is JArray)
                        {
                            Type recipientType = fielfType.GetElementType();
                            Type listType = typeof(List<>).MakeGenericType(recipientType);
                            System.Collections.IList itemList = (System.Collections.IList)Activator.CreateInstance(listType);
                            foreach (var item in (kvp.Value as JArray))
                                itemList.Add(Convert.ChangeType(item, recipientType));
                            Array itemArray = Array.CreateInstance(recipientType, itemList.Count);
                            itemList.CopyTo(itemArray, 0);
                            fieldMap[kvp.Key].SetValue(data, itemArray);
                        }
                        else if (Nullable.GetUnderlyingType(fielfType) != null)
                        {
                            fieldMap[kvp.Key].SetValue(data, Convert.ChangeType(kvp.Value, Nullable.GetUnderlyingType(fielfType)));
                        }
                        else
                            fieldMap[kvp.Key].SetValue(data, Convert.ChangeType(kvp.Value, fielfType));
                    }
                }

                return data;
            }
        }

        public void SaveTable<T>(bool isSave = true)
        {
            if(_dataSelf == null) _dataSelf = DataSelf.GetInstance();

            string varName = GetKey<T>();
            PropertyInfo field = _dataSelf.GetType().GetProperty(varName);
            T obj = (T)field.GetValue(_dataSelf);
            string data = JsonConvert.SerializeObject(obj);
            string encryptedData = Encryption.Encrypt(data);

            PlayerPrefs.SetString(varName, encryptedData);
            if (isSave)
                PlayerPrefs.Save();
        }

        private string GetKey<T>()
        {
            return typeof(T).Name.Replace("Data", string.Empty);
        }

        private Dictionary<string, (Type, object)> GetPopulateData<T>()
        {
            if(typeof(T) == typeof(TestUserData))
            {
                return _populateTestUserData;
            }

            return new Dictionary<string, (Type, object)>();
        }
    }
}