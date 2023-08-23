using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MoveToward : IMoveToward
{
    Player _player;
 
    private float rotationSpeed = 10f;

    public MoveToward(Player player)
    {
        _player = player;
    }

    public void MoveToThePlayer(Transform currentTransform, float speed)
    {

        Debug.Log("Работает MoveToThePlayer");

        
        Vector3 playerDirection = (_player.transform.position - currentTransform.position).normalized;
        Vector3 moveDirection = playerDirection;

        currentTransform.position += moveDirection * speed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
        currentTransform.rotation = Quaternion.RotateTowards(currentTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        

    }
}
