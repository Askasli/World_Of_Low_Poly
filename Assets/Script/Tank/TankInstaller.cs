using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TankInstaller : MonoInstaller
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Transform crosshair;
    [SerializeField] private Rigidbody tankRigidbody;

    [SerializeField] private int healthAmount = 100;
    [SerializeField] private float armourAmount = 0.2f; // (0, 1) 

    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float turretRotationSpeed = 180f;

    public override void InstallBindings()
    {
        Container.Bind<HealthUIController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IHealth>().To<Health>().AsSingle().WithArguments(healthSlider, healthAmount, armourAmount);
        Container.Bind<ITankMovement>().To<TankMovement>().AsTransient().WithArguments(maxSpeed, moveSpeed, rotationSpeed);
        Container.Bind<ITurretControl>().To<TurretController>().AsSingle().WithArguments(turretRotationSpeed);
        Container.Bind<IWeaponController>().To<WeaponController>().AsSingle();
        Container.Bind<ITargetPosition>().To<TargetPosition>().AsSingle().WithArguments(crosshair);
        Container.Bind<Rigidbody>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IExplosionForce>().To<ExplosionForce>().AsSingle();

        Container.Bind<ItestTwo>().To<TestTwo>().AsSingle();


    }



}
