using System;
using UnityEngine;

public class GameKeyboard : MonoBehaviour
{
    public static event Action<char> Input;

    private void Update()
    {
        if (UnityEngine.Input.anyKeyDown == true && CheckMouseInput() == false)
        {
            if (UnityEngine.Input.inputString is null == false)
                Input?.Invoke(UnityEngine.Input.inputString[0]);
        }
    }

    private bool CheckMouseInput()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0) == true)
            return true;
        
        if(UnityEngine.Input.GetMouseButtonDown(1) == true)
            return true;
        if (UnityEngine.Input.GetMouseButtonDown(2) == true)
            return true;
        
        return false;
    }

    public void ButtonPressed(string symbol)
    {
        Input?.Invoke(char.Parse(symbol));
    }
}
