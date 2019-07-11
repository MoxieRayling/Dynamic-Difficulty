using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveCandidate : Candidate
{

    public WaveCandidate()
    {
        for (int i = 0; i < 6; i++) {
            genes.Add(new EnemyGene());
        }
    }

    public WaveCandidate(List<Gene> genes)
    {
        this.genes = genes;
    }

    public override List<Candidate> Crossover(List<Gene> candidate)
    {
        List<Candidate> result = new List<Candidate>();
        List<Gene> genes1 = new List<Gene>();
        List<Gene> genes2 = new List<Gene>();
        for (int i = 0; i < 6; i++)
        {
            if (Random.value < 0.5)
            {
                genes1.Add(genes[i]);
                genes2.Add(candidate[i]);
            }
            else
            {
                genes2.Add(genes[i]);
                genes1.Add(candidate[i]);
            }
        }
        result.Add(new WaveCandidate(genes1));
        result.Add(new WaveCandidate(genes2));

        return result;
    }

    public override double Fitness()
    {
        List<Candidate> cands = new List<Candidate>();



        return 1.0;
    }

    public override void Mutate(double chance)
    {
        foreach (Gene g in genes)
        {
            if (Random.value > 1 / 6)
            {
                g.Mutate();
            }
        }
    }

    private double CompareWaves(List<WaveData> waves) {
        double result = 0;

        if (waves.Count() > 0) {

            foreach (WaveData wd in waves) {

                var enemies = wd.Enemies.FindAll(e => e.Active);
                double wc = 0;
                foreach (EnemyData ed in enemies) {

                    double ec = 0;
                    foreach (EnemyGene eg in genes) {

                        ec += Compare(ed, eg) / genes.Count();
                    }
                    wc += ec;
                }
                result += wc * (Mathf.Abs(genes.Count() - enemies.Count) + 1) / 6;
            }
        }
        return result / waves.Count();
    }

    private double Compare(EnemyData enemy, EnemyGene gene)
    {
        return (
            Mathf.Abs(enemy.Health - gene.GetHealth()) / 29 +
            Mathf.Abs(enemy.ShotSpeed - gene.GetShotSpeed()) / 50 +
            Mathf.Abs(enemy.FireRate - gene.GetFireRate()) / 115
            ) / 3;
    }

    private double LinModel(int x, double a, double b)
    {
        return a * x + b;
    }
    private double ExpModel(int x, double a, double b, double c)
    {
        return Mathf.Exp((float)a * x) * b + c;
    }
    private double RexpModel(int x, double a, double b, double c)
    {
        return Mathf.Exp((float)a * x *-1) * b + c;
    }
    private double LogModel(int x, double a, double b, double c)
    {
        return Mathf.Log((float)a * x) * b + c;
    }

    private double Model6(int X)
    {
        double result = 0;
        for(int i = 0; i < 6; i++)
        {

        }
        return result;
    }
}
