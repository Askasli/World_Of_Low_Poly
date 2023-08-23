using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField]private Transform bulletPrefab;
    [SerializeField] private Transform explosionPrefab;

    [SerializeField] private Transform towerShootEffect;
    [SerializeField] private Transform machineGunShootEffect;

    [SerializeField] private Transform machineGunBullet;
    [SerializeField] private Transform machineGunExplosion;
    


    public override void InstallBindings()
    {
        Container.BindFactory<Bullet, Bullet.Factory>().FromPoolableMemoryPool<Bullet, BulletPool>(poolBinder => poolBinder.WithInitialSize(4).FromComponentInNewPrefab(bulletPrefab).UnderTransformGroup("Bullets"));
        Container.BindFactory<Explosion, Explosion.Factory>().FromPoolableMemoryPool<Explosion, ExplosionPool>(poolBinder => poolBinder.WithInitialSize(4).FromComponentInNewPrefab(explosionPrefab).UnderTransformGroup("Explosion"));

        Container.BindFactory<AfterShootEffectMachineGun, AfterShootEffectMachineGun.Factory>().FromPoolableMemoryPool<AfterShootEffectMachineGun, ShootFXPool>(poolBinder => poolBinder.WithInitialSize(10).FromComponentInNewPrefab(machineGunShootEffect).UnderTransformGroup("AfterShootEffects"));

        Container.BindFactory<MachineGunBullet, MachineGunBullet.Factory>().FromPoolableMemoryPool<MachineGunBullet, MachineGunBulletPool>(poolBinder => poolBinder.WithInitialSize(20).FromComponentInNewPrefab(machineGunBullet).UnderTransformGroup("MachineGunBullets"));
        Container.BindFactory<MachineGunExplosion, MachineGunExplosion.Factory>().FromPoolableMemoryPool<MachineGunExplosion, MachineGunExplosionPool>(poolBinder => poolBinder.WithInitialSize(20).FromComponentInNewPrefab(machineGunExplosion).UnderTransformGroup("MachineGunExplosion"));

    }

    class BulletPool : MonoPoolableMemoryPool<IMemoryPool, Bullet>
    {
    }

    class MachineGunBulletPool : MonoPoolableMemoryPool<IMemoryPool, MachineGunBullet>
    {
    }

    class ExplosionPool : MonoPoolableMemoryPool<IMemoryPool, Explosion>
    {
    }

    class MachineGunExplosionPool : MonoPoolableMemoryPool<IMemoryPool, MachineGunExplosion>
    {
    }

    class ShootFXPool : MonoPoolableMemoryPool<IMemoryPool, AfterShootEffectMachineGun>
    {
    }
}