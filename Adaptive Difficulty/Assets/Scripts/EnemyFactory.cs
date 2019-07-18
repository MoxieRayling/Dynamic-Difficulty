using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class EnemyFactory
{
    private int count = 1;
    public EnemyFactory()
    {

    }

    public void Setup(Shooter shooter)
    {
        Shooter script = shooter.GetComponent<Shooter>();
        script.FireRate = 50;
        script.Health = 4;
        script.speed = 0;
        script.ShotSpeed = 0.1f;
    }

    public void GetRandWave(List<Shooter> enemies)
    {
        Random r = new Random();
        enemies.Sort((x, y) => r.Next(-1, 1));
        int limit = 1;
        if (count <= 60)
            limit = 1;
        else if (count <= 90)
            limit = 2;
        else if (count <= 110)
            limit = 3;
        else if (count <= 125)
            limit = 4;
        else if (count <= 137)
            limit = 5;
        else if (count <= 147)
            limit = 6;

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
        count++;
        if (count > 147) count = 1;
    }

    public void GetBestWave(List<Shooter> enemies, List<Gene> best)
    {
        for(int i = 0; i < 6; i++)
        {
            enemies[i].Id = i + 1;
            if (((EnemyGene)best[i]).IsActive()) enemies[i].Revive();
            else
            {
                enemies[i].SetInactive();
                enemies[i].Id = 0;
            }
            enemies[i].Health = ((EnemyGene)best[i]).GetHealth();
            enemies[i].ShotSpeed = ((EnemyGene)best[i]).GetShotSpeed();
            enemies[i].FireRate = ((EnemyGene)best[i]).GetFireRate();
        }

    }
}