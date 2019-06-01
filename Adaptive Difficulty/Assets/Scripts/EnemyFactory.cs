using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    public GameObject[] GenerateEnemies()
    {
        GameObject[] enemies = new GameObject[3];
        return enemies;
    }

    private GameObject Melee(Vector3 pos)
    {
        GameObject melee = GameObject.Instantiate(Resources.Load("Melee")) as GameObject;
        melee.GetComponent<Transform>().position = pos;
        return melee;
    }

    private GameObject Shooter(Vector3 pos)
    {
        GameObject shooter = GameObject.Instantiate(Resources.Load("Shooter")) as GameObject;
        shooter.GetComponent<Transform>().position = pos;
        return shooter;
    }
}