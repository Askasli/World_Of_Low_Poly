using System.Collections;
using UnityEngine;


public class InputManager : IInputManager
{
    public float GetForwardInput()
    {
        return Input.GetAxis("Vertical");
    }

    public float GetTurnInput()
    {
        return Input.GetAxis("Horizontal");
    }
}
