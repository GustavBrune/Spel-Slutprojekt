using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectable : MonoBehaviour
{
    private Rigidbody2D itemRb;
    public float dropforce = 5;
    
    
    
    void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
        itemRb.AddForce(Vector2.up * dropforce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  






}
