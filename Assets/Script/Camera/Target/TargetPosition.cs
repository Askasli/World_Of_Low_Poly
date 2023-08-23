using System.Collections;
using TMPro;
using UnityEngine;


public class TargetPosition : ITargetPosition
{
    private Transform _crosshair;
    private Vector3 targetPoint;



    public TargetPosition(Transform crosshair)
    {
        _crosshair = crosshair;
    }

    public Vector3 GetPosition()
    {
        /*
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        */

        Vector3 targetPosition = Camera.main.transform.TransformPoint(_crosshair.localPosition);
        return targetPosition;
    }

    public Vector3 RayPosition()
    {
        RaycastHit hit;
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = Camera.main.transform.TransformPoint(_crosshair.localPosition);
        }

        return targetPoint;

    }
}