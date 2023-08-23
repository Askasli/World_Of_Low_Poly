
using UnityEngine;


public class MouseInput : IMouseInput
{
    public float GetMouseX()
    {
    
        float mouseX = Input.GetAxis("Mouse X");    // Получаем значение координаты X
        return mouseX;
    }

    public float GetMouseY()
    {
      
        float mouseY = Input.GetAxis("Mouse Y");   // Получаем значение координаты Y
        return mouseY;
    }
}
