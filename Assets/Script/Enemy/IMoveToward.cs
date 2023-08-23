using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveToward
{
    void MoveToThePlayer(Transform currentTransform, float speed);
}
