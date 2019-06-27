using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

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

    public static void GetRandWave(List<Shooter> enemies)
    {
        Random r = new Random();
        enemies.Sort((x, y) => r.Next(-1, 1));
        int limit = r.Next(1, 6);
        for (int i = 0; i < 6; i++)
        {
            if (i < limit) { 
                enemies[i].Randomize();
                enemies[i].Id = i+1;
            }
            else
                enemies[i].SetInactive();
        }
    }
}