using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winScript : MonoBehaviour
{
    
    public GameObject winSreen; // The GameObject to enable

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (winSreen != null)
        {
            winSreen.SetActive(true);
        }
    }
}

