using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem
{
    public static class Saver<T> where T : class, ISaveable
    {
        public static void Save(T data, DataTypes type)
        {
            string path = Application.persistentDataPath + $"/{type.ToString()}.set";
            FileStream stream = new FileStream(path, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static T Load(DataTypes type)
        {
            string path = Application.persistentDataPath + $"/{type.ToString()}.set";

            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                T saveableData = formatter.Deserialize(stream) as T;
                stream.Close();
                return saveableData;
            }

            return null;
        }
    }
}