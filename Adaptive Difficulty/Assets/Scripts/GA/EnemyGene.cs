using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGene : Gene
{
    private bool active;
    private StatGene health;
    private StatGene shotSpeed;
    private StatGene fireRate;

    public bool Active { get => active; set => active = value; }
    public StatGene Health { get => health; set => health = value; }
    public StatGene ShotSpeed { get => shotSpeed; set => shotSpeed = value; }
    public StatGene FireRate { get => fireRate; set => fireRate = value; }

    public EnemyGene()
    {
        Active = Random.value < 0.5 ? true : false;
        Health = new StatGene(1, 30);
        ShotSpeed = new StatGene(10, 50);
        FireRate = new StatGene(5, 120);
    }

    public override void Mutate()
    {
        if(Random.value < 0.1) Active = !Active;
        if (Random.value < 0.1) Health.Mutate();
        if (Random.value < 0.1) ShotSpeed.Mutate();
        if (Random.value < 0.1) FireRate.Mutate();
    }

    public int GetHealth()
    {
        return Health.Value();
    }

    public int GetShotSpeed()
    {
        return ShotSpeed.Value();
    }

    public int GetFireRate()
    {
        return FireRate.Value();
    }

    public bool IsActive()
    {
        return Active;
    }
    public List<EnemyGene> Crossover(EnemyGene enemyGene)
    {
        var result = new List<EnemyGene>();
        EnemyGene eg1 = new EnemyGene();
        EnemyGene eg2 = new EnemyGene();
        if (Random.value > 0.5)
        {
            eg1.Active = Active;
            eg2.Active = enemyGene.Active;
        }
        else
        {
            eg2.Active = Active;
            eg1.Active = enemyGene.Active;
        }
        if (Random.value > 0.5)
        {
            eg1.Health = Health;
            eg2.Health = enemyGene.Health;
        }
        else
        {
            eg2.Health = Health;
            eg1.Health = enemyGene.Health;
        }
        if (Random.value > 0.5)
        {
            eg1.ShotSpeed = ShotSpeed;
            eg2.ShotSpeed = enemyGene.ShotSpeed;
        }
        else
        {
            eg2.ShotSpeed = ShotSpeed;
            eg1.ShotSpeed = enemyGene.ShotSpeed;
        }
        if (Random.value > 0.5)
        {
            eg1.FireRate = FireRate;
            eg2.FireRate = enemyGene.FireRate;
        }
        else
        {
            eg2.FireRate = FireRate;
            eg1.FireRate = enemyGene.FireRate;
        }
        result.Add(eg1);
        result.Add(eg2);

        return result;
    }
}
