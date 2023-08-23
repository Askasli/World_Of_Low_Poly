using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TankManager: MonoBehaviour
{
    [SerializeField] private Transform turretTransform;
    [SerializeField] private Transform gunTransform;

    [SerializeField] private Transform spawnPointShoot;
    [SerializeField] private Transform spawnPointShootMachineGun;

    private ITankMovement _tankMovement;
    private IInputManager _inputManager;
    private ITurretControl _turretControl;
    private IWeaponController _weaponController;
    private IHealth _health;

    Rigidbody _rigi;


    [Inject]
    private void Construct(IHealth health, ITankMovement tankMovement, IInputManager inputManager, 
        ITurretControl turretControl, IWeaponController weaponController, Rigidbody rigi)
    {
        _health = health;
        _tankMovement = tankMovement;
        _inputManager = inputManager;
        _turretControl = turretControl;
        _weaponController = weaponController;
        _rigi = rigi;

    }

    private void Update()
    {
        TurretController();
        ShootController();
    }

    private void FixedUpdate()
    {
        TankController();
    }

    void TankController()
    {
        _tankMovement.Move(_inputManager.GetForwardInput(), _rigi, transform);
        _tankMovement.Rotate(_inputManager.GetTurnInput(), _rigi);
    }

    void TurretController()
    {
        _turretControl.RotateTurretTowardsCamera(turretTransform);
        _turretControl.RotateGunTowardsCamera(gunTransform);
    }

    void ShootController()
    {
        _weaponController.Shoot(spawnPointShoot, spawnPointShootMachineGun);
    }


}
