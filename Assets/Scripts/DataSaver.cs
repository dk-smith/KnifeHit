using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization;

public class DataSaver
{
    private BinaryFormatter formatter = new BinaryFormatter();
    private FileStream fileStream;

    public DataSaver Open(FileMode mode = FileMode.OpenOrCreate)
    {
        fileStream = new FileStream(Path.Combine(Application.persistentDataPath, "data.kh"), mode);
        return this;
    }

    public DataSaver Save(PlayerData data)
    {
        if (!fileStream.CanWrite) Open();
        formatter.Serialize(fileStream, data);
        return this;
    }

    public DataSaver Close()
    {
        fileStream.Close();
        return this;
    }

    public PlayerData Load()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "data.kh")))
        {
            try
            {
                Open();
                PlayerData data = (PlayerData)formatter.Deserialize(fileStream);
                Close();
                return data;
            } catch (SerializationException e) { Debug.LogError("Data is corrupted!\n"+e.Message); Close(); }
        }
        return new PlayerData();
    }

}

