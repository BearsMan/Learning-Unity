using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerMovement playerCharacter;
    private Inventory playerInventory;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        playerInventory = playerCharacter.GetComponent<Inventory>();
        LoadData(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(int savedSlot)
    {
        if (!SaveLoad.LoadFile(savedSlot)) return;
        SaveData loadedData = SaveLoad.currentSave;
        playerInventory.backPack = DataConversion.StringToItems(loadedData.inventoryData);
        Vector3 position = new Vector3(loadedData.playerPosition[0], loadedData.playerPosition[1], loadedData.playerPosition[2]);
        Quaternion rotation = new Quaternion(loadedData.playerRotation[0], loadedData.playerRotation[1], loadedData.playerRotation[2], loadedData.playerRotation[3]);
        if (position != Vector3.zero)
        {
            playerCharacter.transform.rotation = rotation;
            playerCharacter.transform.position = position;
        }
    }

    public void SaveData(int savedSlot)
    {
        SaveData dataSave = SaveLoad.currentSave;
        dataSave.inventoryData = DataConversion.ItemsToString(playerInventory.backPack);
        Vector3 position = playerCharacter.transform.position;
        Quaternion rotation = playerCharacter.transform.rotation;
        dataSave.playerPosition = new float[3];
        dataSave.playerRotation = new float[4];
        dataSave.playerPosition[0] = position.x;
        dataSave.playerPosition[1] = position.y;
        dataSave.playerPosition[2] = position.z;
        dataSave.playerRotation[0] = rotation.x;
        dataSave.playerRotation[1] = rotation.y;
        dataSave.playerRotation[2] = rotation.z;
        dataSave.playerRotation[3] = rotation.w;
        SaveLoad.currentSave = dataSave;
        SaveLoad.SaveCurrentData(savedSlot);
    }
}
