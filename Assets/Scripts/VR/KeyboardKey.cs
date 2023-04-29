using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardKey : MonoBehaviour
{
    public TMP_InputField inputField;
    public string key;

    public void KeyboardBackSpace()
    {
        inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
    }

    public void InputName()
    {
        inputField.text += key;
    }
}
