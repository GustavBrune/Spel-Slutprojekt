using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{
    public GameObject doorInteract; //dialoguePanel
    public Text doorInteractText;   // dialogueText
    public string[] interact;       // dialogue
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;
    void Start()
    {
        
    }

 
    void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(2); 
            
        }

        if(playerIsClose)
        {

            Debug.Log("dunkar");
        }
    }



    public void zeroText()
    {
        doorInteractText.text = "";
        index = 0;
        doorInteract.SetActive(false);

    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}

