using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent OnFoodEaten = new UnityEvent();
    public static UnityEvent OnSnakeDeath = new UnityEvent();

    public static void SendFoodEaten()
    {
        OnFoodEaten.Invoke();
    }

    public static void SendSnakeDeath()
    {
        OnSnakeDeath.Invoke();
    }
}
