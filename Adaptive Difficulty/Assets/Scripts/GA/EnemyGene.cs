using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGene : Gene
{
    private bool active;
    private StatGene health;
    private StatGene shotSpeed;
    private StatGene fireRate;
    
    public EnemyGene()
    {
        active = true;
        health = new StatGene(1, 30);
        shotSpeed = new StatGene(1, 50);
        fireRate = new StatGene(5, 120);
    }

    public override void Mutate()
    {
        if(Random.value < 0.5) active = !active;
        if (Random.value < 0.1) health.Mutate();
        if (Random.value < 0.1) shotSpeed.Mutate();
        if (Random.value < 0.1) fireRate.Mutate();
    }

    public int GetHealth()
    {
        return health.Value();
    }

    public int GetShotSpeed()
    {
        return shotSpeed.Value();
    }

    public int GetFireRate()
    {
        return fireRate.Value();
    }
}
