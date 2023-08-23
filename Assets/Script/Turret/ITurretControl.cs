
using System;
using UnityEngine;

public interface ITurretControl
{
    void RotateTurretTowardsCamera(Transform transform);
    void RotateGunTowardsCamera(Transform transform);
}