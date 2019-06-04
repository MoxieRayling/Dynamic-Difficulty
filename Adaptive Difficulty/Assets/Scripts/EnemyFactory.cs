using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    public static void Setup(GameObject shooter, Vector3 pos, float fireRate, int health, float speed, float shotSpeed)
    {
        shooter.GetComponent<Transform>().position = pos;
        Shooter script = shooter.GetComponent<Shooter>();
        script.fireRate = fireRate;
        script.health = health;
        script.speed = speed;
        script.shotSpeed = shotSpeed;
    }
}