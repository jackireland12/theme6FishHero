using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui : MonoBehaviour
{
    public void OnStartWaveButton()
    {
        fishManager.Instance.StartAttackMode();
    }
    public void OnEndWaveButton()
    {
        fishManager.Instance.StartMergingMode();
    }
}
