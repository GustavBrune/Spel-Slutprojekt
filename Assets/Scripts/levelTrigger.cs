using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 using Text = TMPro.TextMeshProUGUI;

public class levelTrigger : MonoBehaviour

{

    public bool playerIsClose;
    public Text levelCompleteText;
    public GameObject levelPanel;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsClose)
        {
            levelPanel.SetActive(true);
         

        }
        else
        {
            levelPanel.SetActive(false);
        }
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

        }
    }

}
