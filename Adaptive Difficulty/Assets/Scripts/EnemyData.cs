using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData 
{
    private bool active;
    private int health;
    private int shotSpeed;
    private int fireRate;

    public bool Active { get => active; set => active = value; }
    public int Health { get => health; set => health = value; }
    public int ShotSpeed { get => shotSpeed; set => shotSpeed = value; }
    public int FireRate { get => fireRate; set => fireRate = value; }

    public EnemyData(bool active, int health, int shotSpeed, int fireRate)
    {
        Active = active;
        Health = health;
        ShotSpeed = ShotSpeed;
        FireRate = fireRate;
    }
}
