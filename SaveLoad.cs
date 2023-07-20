using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


public static class SaveLoad 
{

    private static string SaveLocation(int saveID)
    {
        return $"{Application.persistentDataPath}/save{saveID}.sav"; 
    }
    public static SaveData currentSave;

    public static void SaveCurrentData(int saveId)
    {
        Inventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        currentSave.inventoryData = DataConversion.ItemsToString(playerInventory.backPack);
        string destination = SaveLocation(saveId);
        FileStream file;
        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
        }
        else
        {
            file = File.Create(destination);
        }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, currentSave);
        file.Close();
    }

    public static bool LoadFile(int saveId)
    {
        string destination = SaveLocation(saveId); ;
        FileStream file;
        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);
        }
        else
        {
            Debug.LogWarning("File does not exist");
            return false;
        }

        BinaryFormatter bf = new BinaryFormatter();
        currentSave = (SaveData)bf.Deserialize(file);
        file.Close();
        return true;
    }

}
[System.Serializable]
public struct SaveData
{
    public string equipment;
    public string inventoryData;
    public string saveName;
    public string questData;
    public int money;
    public int saveDate;
    public float[] playerPosition;
    public float[] playerRotation;
}
