using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponController
{
    void Shoot(Transform shootPosition, Transform machineGunPosition);
}