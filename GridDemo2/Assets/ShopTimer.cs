using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopTimer : MonoBehaviour
{
    public int duration = 60;
    public TextMeshProUGUI text;
    private int timeRemaining;

   
    void Start()
    {
        text.text = duration + " Secounds remaining";
        timeRemaining = duration;
        _tick();
    }

    private void _tick()
    {
        timeRemaining--;
        if (timeRemaining > 0)
        {
            text.text = timeRemaining + " Secounds remaining";
            Debug.Log("Time remaining: " + timeRemaining);
            Invoke("_tick", 1f);
        }
        else
        {
            Debug.Log("Timer finished.");
        }
    }
}
