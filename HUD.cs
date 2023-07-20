using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }
    public Image cursorImage;
    public Sprite standard, active, notActive;
    public GameObject conversationScreen;
    public TMP_Text conversationText;
    public Item heldItem;
    public bool paused;
    public GameObject saveScreen, pauseScreen, gameScreen;
    public enum CursorTypes
    {
        standard,
        active,
        notActive,
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        conversationScreen.SetActive(false);
        SetCursorType(CursorTypes.standard);
    }

    // Update is called once per frame
    void Update()
    {
        if (conversationScreen.activeSelf)
        {
            return;
        }
        cursorImage.transform.parent.position = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pauseScreen.activeSelf)
            {
                OpenPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }

    public void OpenPauseMenu()
    {
        paused = true;
        Time.timeScale = 0;
        if (saveScreen.activeSelf)
        {
            saveScreen.SetActive(false);
        }
        pauseScreen.SetActive(true);
        gameScreen.SetActive(false);
    }

    public void ClosePauseMenu()
    {
        paused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        gameScreen.SetActive(true);
        saveScreen.SetActive(false);
    }

    public void OpenSaveScreen()
    {
        paused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(false);
        gameScreen.SetActive(false);
        saveScreen.SetActive(true);
    }

    public void CloseSaveScreen()
    {
        paused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        gameScreen.SetActive(true);
        saveScreen.SetActive(false);
    }

    public void SetCursorType(CursorTypes type)
    {
        switch (type)
        {
            case CursorTypes.standard:
                cursorImage.sprite = standard;
                return;
            case CursorTypes.active:
                cursorImage.sprite = active;
                return;
            case CursorTypes.notActive:
                cursorImage.sprite = notActive;
                return;
        }
    }

    public void StartConverstation(ConversationPiece[] newConversations)
    {
        StartCoroutine(ConversationProcess(newConversations));
    }
    

    private IEnumerator ConversationProcess(ConversationPiece[] newConversations)
    {
        conversationScreen.SetActive(true);
        foreach(ConversationPiece c in newConversations)
        {
            conversationText.text = c.convo;
            while (!Input.GetKey(KeyCode.Return)) yield return null;
            while (Input.GetKey(KeyCode.Return)) yield return null;
            c.sentenceEvent?.Invoke();
        }
        conversationScreen.SetActive(false);
    }
    
    public void LoadGame(int saveSlot)
    {
        GameManager.Instance.LoadData(saveSlot);
    }

    public void SaveGame(int saveSlot)
    {
        GameManager.Instance.SaveData(saveSlot);
    }
}

[System.Serializable]
public struct ConversationPiece
{
    public string convo;
    public Transform target, lookTarget;
    public UnityEvent sentenceEvent;
}
