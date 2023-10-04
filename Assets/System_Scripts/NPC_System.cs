using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class DialogueData
{
    public string name;
    public List<string> dialogue;
}

public class NPC_System : MonoBehaviour
{
    [SerializeField] public GameObject d_template;
    [SerializeField] public GameObject canvas;
    [SerializeField] public GameObject notification;
    [SerializeField] public ThirdPersonCharacter player;
    [SerializeField] public TextAsset dialogueJSON;

    private DialogueData dialogues; // Store dialogues from JSON

    private void Start()
    {
        LoadDialoguesFromJSON();
    }

    public bool playerDetected = false;
    int index = 1;
    // Update is called once per frame
    void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.F) && !player.inDialogue)
        {
            canvas.SetActive(true);
            print("Dialog Initiated");
            player.inDialogue = true;
            // NewDialog("Test");
            // NewDialog("Test 2");
            // NewDialog("Test 3");
            print(dialogues.dialogue.Count);
            foreach (string text in dialogues.dialogue)
            {
                NewDialog(text);
            }
            canvas.transform.GetChild(index++).gameObject.SetActive(true);

            // create a string with names of all children
            string childNames = "";
            for (int i = 0; i < canvas.transform.childCount; i++)
            {
                childNames += canvas.transform.GetChild(i).name;
                if (i != canvas.transform.childCount - 1)
                {
                    childNames += ", ";
                }
            }
            print("Children: " + childNames);
        }
        // show your indexth child and deactivate the rest like a ring buffer on mouse left button click
        if (player.inDialogue && Input.GetMouseButtonDown(0) && playerDetected)
        {
            print(index);
            for (int i = 0; i < canvas.transform.childCount; i++)
            {
                canvas.transform.GetChild(i).gameObject.SetActive(i == index);
            }
            index++;
            if (index > canvas.transform.childCount)
            {
                index = 1;
                player.inDialogue = false;
                // delete all children except the first
                for (int i = 1; i < canvas.transform.childCount; i++)
                {
                    Destroy(canvas.transform.GetChild(i).gameObject);
                }
            }
        }
    }

    void NewDialog(string text)
    {
        GameObject duplicate = Instantiate(d_template, d_template.transform);
        duplicate.transform.parent = canvas.transform;
        duplicate.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    }

    void LoadDialoguesFromJSON()
    {
        dialogues = JsonUtility.FromJson<DialogueData>(dialogueJSON.text);

        if (dialogues == null)
        {
            Debug.LogError("Failed to parse JSON data from the provided TextAsset.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerDetected = true;
            notification.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerDetected = false;
        notification.SetActive(false);
    }
}
