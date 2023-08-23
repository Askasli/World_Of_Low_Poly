using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class WeaponController : IWeaponController
{
    private InputHandler inputHandler;
    private ICameraShake _cameraShake;
    private ITargetPosition _targetPosition;
    private ÑameraShake shakeSettings;


    private bool canShoot = true;
    private float shootTimer = 0.0f;
    private float shootCooldown = 0.6f;
    private readonly Bullet.Factory _bulletFactory;
    private readonly MachineGunBullet.Factory _machineGunBulletFactory;
    private AfterShootEffectMachineGun.Factory _explosionMachGunFactory;
    private bool isGunActive = true;

    public WeaponController(Bullet.Factory bulletFactory, MachineGunBullet.Factory machineGunbBlletFactory, AfterShootEffectMachineGun.Factory explosionMachGunFactory, ITargetPosition targetPosition)
    {
        _bulletFactory = bulletFactory;
        _targetPosition = targetPosition;
        _machineGunBulletFactory = machineGunbBlletFactory;
        _explosionMachGunFactory = explosionMachGunFactory;

        shakeSettings = new ÑameraShake();
        inputHandler = new InputHandler();

        inputHandler.OnQPressed += HandleQPressed;
        inputHandler.OnEPressed += HandleEPressed;
    }



    public void Shoot(Transform shootPosition, Transform subMachineGunPosition)
    {
        inputHandler.HandleInput();
        shootTimer += Time.deltaTime;

        if (canShoot && Input.GetMouseButton(0))
        {
            if (isGunActive)
            {
                ShootBullet(shootPosition, 100f, 0.6f);
            }
            else
            {
                ShootMachineBullet(subMachineGunPosition, 100, 0.2f);
            }

            shootTimer = 0.0f;
            canShoot = false;
        }
   
        if (!canShoot && shootTimer >= shootCooldown)
        {
            canShoot = true;
        }
    }


    private void ShootBullet(Transform shootPosition, float force, float coolDown)
    {
        var bullet = _bulletFactory.Create();
        bullet.transform.position = shootPosition.position;
        bullet.transform.rotation = shootPosition.rotation;
        shootCooldown = coolDown;
        ApplyBulletForce(bullet.GetComponent<Rigidbody>(), shootPosition, force);

    }

    private void ShootMachineBullet(Transform shootPosition, float force, float coolDown)
    {
        var bullet = _machineGunBulletFactory.Create();
        var explosion = _explosionMachGunFactory.Create();
        explosion.transform.position = shootPosition.position;

        bullet.transform.position = shootPosition.position;
        bullet.transform.rotation = shootPosition.rotation;
        shootCooldown = coolDown;
        ApplyBulletForce(bullet.GetComponent<Rigidbody>(), shootPosition, force);
    }

    private void ApplyBulletForce(Rigidbody bulletRigidbody, Transform shootPosition, float force)
    {
        Vector3 shootDirection = (_targetPosition.RayPosition() - shootPosition.position).normalized;

        if (bulletRigidbody.velocity.magnitude > 0f)
        {
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = Vector3.zero;
        }

        bulletRigidbody.AddForce(shootDirection * force, ForceMode.Impulse);
    }

    IEnumerator Shake()
    {
        shakeSettings.ShakeMagnitude = Mathf.Lerp(0.1f, 0.2f, Mathf.PingPong(Time.time, 1f));
        yield return null;
    }

    private void HandleQPressed()
    {
        Debug.Log("Q pressed!");
        isGunActive = true;
    }

    private void HandleEPressed()
    {
        Debug.Log("E pressed!");
        isGunActive = false;
    }

    private void OnDestroy()
    {
        inputHandler.OnQPressed -= HandleQPressed;
        inputHandler.OnEPressed -= HandleEPressed;
    }


}
