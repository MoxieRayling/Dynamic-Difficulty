using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class EnemyFactory
{
    private WaveGA ga;
    private bool runningGA = false;

    public bool RunningGA { get => runningGA; set => runningGA = value; }

    public EnemyFactory()
    {
        ga = new WaveGA(50);
    }

    public IEnumerator RunGA()
    {
        RunningGA = true;
        while (RunningGA)
        {
            ga.NextGen();
        }
        yield return null;
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

    public void GetBestWave(List<Shooter> enemies)
    {
        var best = ga.Best.GetGenes();
        for(int i = 0; i < 6; i++)
        {
            enemies[0].enabled = ((EnemyGene)best[0]).IsActive();
            enemies[0].Health = ((EnemyGene)best[0]).GetHealth();
            enemies[0].ShotSpeed = ((EnemyGene)best[0]).GetShotSpeed();
            enemies[0].FireRate = ((EnemyGene)best[0]).GetFireRate();
        }
    }
}