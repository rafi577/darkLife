using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] GameObject howToPlay;
    public void How()
    {
        howToPlay.SetActive(true);
    }

    public void HowClose()
    {
        howToPlay.SetActive(false);
    }
}
