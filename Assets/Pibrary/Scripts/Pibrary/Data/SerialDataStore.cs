using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UniRx;
using UnityEngine;

namespace Pibrary.Data
{
    public class SerialDataStore<T> : IDataStore<T> where T : class, new()
    {
        private ReactiveProperty<T> saveData = new ReactiveProperty<T>();

        public IReadOnlyReactiveProperty<T> SaveData
        {
            get
            {
                if (saveData.Value == null)
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

        private T Load()
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

            saveData.Value = data;
            return data;
        }
    }
}