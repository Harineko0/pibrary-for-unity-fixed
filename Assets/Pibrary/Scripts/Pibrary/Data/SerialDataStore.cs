using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Pibrary.Data
{
    public class SerialDataStore<T> : IDataStore<T> where T : class, new()
    {
        private T saveData;

        public T SaveData
        {
            get
            {
                if (saveData == null)
                {
                    Load();
                }

                return saveData;
            }
        }
        private string SavePath = Application.dataPath + "/pibrary_data.bytes";

        public void Save(T data)
        {
            using (FileStream fs = new FileStream(SavePath, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, data);
            }
        }

        public T Load()
        {
            T data;
            
            if (File.Exists(SavePath))
            {
                using (FileStream fs = new FileStream(SavePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    data = bf.Deserialize(fs) as T;
                }
            }
            else
            {
                data = new T();
            }

            saveData = data;
            return data;
        }
    }
}