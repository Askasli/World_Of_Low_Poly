using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : IExplosionForce
{

    public void Explode(Vector3 explosionPosition, float explosionRadius, float explosionForce)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigi = collider.GetComponent<Rigidbody>();


            if (rigi != null)
            {
                rigi.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }
        }

    }

}
