using System;
using UnityEngine;

public class InputHandler
{
    public event Action OnQPressed;
    public event Action OnEPressed;

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnQPressed?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            OnEPressed?.Invoke();
        }
    }
}
