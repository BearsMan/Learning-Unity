using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterPlacement : MonoBehaviour
{
    public NPCData NPC;
    private NPCData previousNPC;
    public GameObject characterObject, placeHolder;
    private void Awake()
    {
        Setup();
    }
    private void Update()
    {
        if (NPC != previousNPC)
        {
            //PrefabUtility.UnpackPrefabInstance(gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            previousNPC = NPC;
            gameObject.name = NPC.name;
            Setup();
        }
    }



    public void Setup()
    {
        if (NPC != null)
        {
            if (characterObject != NPC.characterObject)
            {
                if (characterObject != null)
                {
                    DestroyImmediate(characterObject);
                }
                characterObject = Instantiate(NPC.characterObject, transform);
                characterObject.transform.localPosition = Vector3.zero;
                characterObject.transform.localRotation = Quaternion.identity;
            }
            
        }
        else
        {
            if (characterObject != placeHolder)
            {
                DestroyImmediate(characterObject);
                characterObject = Instantiate(placeHolder, transform);
                characterObject.transform.localPosition = Vector3.zero;
                characterObject.transform.localRotation = Quaternion.identity;
            }
        }
        
    }
}
