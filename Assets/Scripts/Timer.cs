using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] Image fill;
    [SerializeField] float max;

    void Start()
    {
        GlobalEventManager.OnFoodEaten.AddListener(UpdateTimer);
    }

    void Update()
    {
        time -= Time.deltaTime * 10;
        fill.fillAmount = time / max;

        if (time < 0)
        {
            time = 0;
            GlobalEventManager.SendSnakeDeath();
        }
    }

    void UpdateTimer()
    {
        time = 15;
    }


}
