using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mergeDetect : MonoBehaviour
{
    GameObject merge1;
    GameObject merge2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (merge1 == merge2.unitlLevel)
             
        mergeDetect otherFish = collision.GetComponent<mergeDetect>();
    }

}
