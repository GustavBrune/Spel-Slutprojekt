using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Healthbar : MonoBehaviour
{

    public Slider Slider; 

    public void SetHealth(int Health)

    {
        Slider.value = Health; 

    }

    public void SetMaxHealth(int Health)

    {
        Slider.maxValue = Health;
        Slider.value = Health; 

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
