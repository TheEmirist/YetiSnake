using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputName;
    
    void Start()
    {
        inputName.text = PlayerPrefs.GetString("userName", "");
    }
    public void SubmitName()
    {
        PlayerPrefs.SetString("userName", inputName.text);
    }
}
