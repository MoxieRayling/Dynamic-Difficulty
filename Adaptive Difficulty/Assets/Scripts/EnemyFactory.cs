using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    public static void Setup(GameObject shooter, float fireRate, int health, float speed, float shotSpeed)
    {
        Shooter script = shooter.GetComponent<Shooter>();
        script.fireRate = fireRate;
        script.health = health;
        script.speed = speed;
        script.shotSpeed = shotSpeed;
    }
}