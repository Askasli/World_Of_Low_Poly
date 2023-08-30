using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExplosionForce
{
    void Explode(Vector3 explosionPosition, float explosionRadius, float explosionForce);
}
