using UnityEngine;


public interface ICameraLock
{
    Transform TargetTransform { get; }
    bool LockEnabled { get; set; }

   

}
