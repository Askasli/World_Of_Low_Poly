
using UnityEngine;


public class MouseInput : IMouseInput
{
    public float GetMouseX()
    {
    
        float mouseX = Input.GetAxis("Mouse X");    
        return mouseX;
    }

    public float GetMouseY()
    {
      
        float mouseY = Input.GetAxis("Mouse Y");   
        return mouseY;
    }
}
