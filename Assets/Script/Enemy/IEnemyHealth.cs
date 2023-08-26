using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyHealth
{
    void TakeDamage(int damage, int attackerArmor);
}
