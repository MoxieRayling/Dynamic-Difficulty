using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    public static void Setup(Shooter shooter)
    {
        Shooter script = shooter.GetComponent<Shooter>();
        script.fireRate = 50;
        script.health = 4;
        script.speed = 0;
        script.shotSpeed = 0.1f;
    }
}