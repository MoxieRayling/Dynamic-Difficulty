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
        script.FireRate = 50;
        script.Health = 4;
        script.speed = 0;
        script.ShotSpeed = 0.1f;
    }

    public static void GetRandWave(List<Shooter> enemies)
    {
        Random r = new Random();
        enemies.Sort((x, y) => r.Next(-1, 1));
        int temp = r.Next(1, 148);
        int limit = r.Next(1, 7);/*1;
        if (temp <= 60)
            limit = 1;
        else if (temp <= 90)
            limit = 2;
        else if (temp <= 110)
            limit = 3;
        else if (temp <= 125)
            limit = 4;
        else if (temp <= 137)
            limit = 5;
        else if (temp <= 147)
            limit = 6;
        Debug.Log(limit);*/

        for (int i = 0; i < 6; i++)
        {
            if (i < limit)
            {
                enemies[i].Randomize();
                enemies[i].Id = i + 1;
            }
            else
            {
                enemies[i].SetInactive();
                enemies[i].Id = 0;
            }
        }
    }
}