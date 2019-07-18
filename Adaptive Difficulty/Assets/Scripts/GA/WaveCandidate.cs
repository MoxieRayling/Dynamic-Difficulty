using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveCandidate : Candidate
{
    private double[] subFit1 = { 0.088, 0.129, 1.325, 0.441, 0.067, 0.011, 3.579, -0.443 };
    private double[] fit1 = { 1.000, 1.344, 0.516, 0.656,  - 0.037 };

    private double[] subFit2 = { 0.080, 0.839, 19.006, 0.523, -0.994, 0.008, 5.294, -1.161 };
    private double[] fit2 = { -0.912, -0.773, -0.397, 1.237, -0.251, 1.343, -0.638, 0.081 };

    private double[] subFit3 = { 0.081, 1.222, 1.567, 0.512, 0.743, 0.006, 7.454, -2.816 };
    private double[] fit3 = { 1.841, 1.903, 2.547, 0.437, -13.057 };

    private double[] subFit4 = { 1.317, 0.872, 0.038, 1.803, 0.008, 6.685, -1.556 };
    private double[] fit4 = { 1.248, 1.324, 0.987, 0.914, 0.109, -2.005 };

    private double[] subFit5 = { 0.559, 0.890, 0.041, 1.869, 0.010, 6.699, -0.948 };
    private double[] fit5 = { 1.732, 1.000, 1.650, 1.597, 1.373, 0.190, -8.719 };

    private double[] subFit6 = { 0.896, 0.958, 0.720, 0.040, 2.001, 0.010, 6.870, -0.755 };
    private double[] fit6 = { 0.867, 0.843, 0.753, 0.732, 0.634, 0.532, 0.276, -6.928 };

    public double FitScore { get => fitness; set => fitness = value; }

    public WaveCandidate()
    {
        genes = new List<Gene>();
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

    public override void Fitness()
    {
        double target = 1;
        double result = 0;
        switch (genes.FindAll(g => ((EnemyGene)g).IsActive()).Count)
        {
            case 1:
                result = Model1();
                break;
            case 2:
                result = Model2();
                break;
            case 3:
                result = Model3();
                break;
            case 4:
                result = Model4();
                break;
            case 5:
                result = Model5();
                break;
            case 6:
                result = Model6();
                break;
            default:
                break;
        }
        if(result == target) FitScore = 1;
        
        else result = 1/Mathf.Abs(1 - (float) result);
        //result *= CompareWaves(new List<WaveData>());
        
        FitScore = result;
    }

    public override void Mutate(double chance)
    {
        foreach (Gene g in genes)
        {
            if (Random.value > chance)
            {
                g.Mutate();
            }
        }
    }

    private double CompareWaves(List<WaveData> waves) {
        double result = 0;

        if (waves.Count > 0) {

            foreach (WaveData wd in waves) {

                var enemies = wd.Enemies.FindAll(e => e.Active);
                double wc = 0;
                foreach (EnemyData ed in enemies) {

                    double ec = 0;
                    foreach (EnemyGene eg in genes) {

                        ec += Compare(ed, eg) / genes.Count;
                    }
                    wc += ec;
                }
                result += wc * (Mathf.Abs(genes.Count - enemies.Count) + 1) / 6;
            }
        }
        return result / waves.Count;
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

    private double Model1()
    {
        double result = 0;
        int[,] stats = GenesToStats();

        result = (
        fit1[0] * LinModel(stats[0,0], subFit1[0], subFit1[1]) *
        fit1[1] * LogModel(stats[0,1], subFit1[2], subFit1[3], subFit1[4]) *
        fit1[2] * RexpModel(stats[0,2], subFit1[5], subFit1[6], subFit1[7])
        ) * fit1[3] + fit1[4];
        
        return fit1[fit1.Length - 2] * result + fit1[fit1.Length - 1];
    }

    private double Model2()
    {
        double result = 0;
        int[,] stats = GenesToStats();

        result = (
        fit2[0] * LinModel(stats[0,0], subFit2[0], subFit2[1]) *
        fit2[1] * LogModel(stats[0,1], subFit2[2], subFit2[3], subFit2[4]) *
        fit2[2] * RexpModel(stats[0,2], subFit2[5], subFit2[6], subFit2[7]) + 

        fit2[3] * LinModel(stats[1,0], subFit2[1], subFit2[1]) *
        fit2[4] * LogModel(stats[1,1], subFit2[2], subFit2[3], subFit2[4]) *
        fit2[5] * RexpModel(stats[1,2], subFit2[5], subFit2[6], subFit2[7])

        ) * fit2[6] + fit2[7] ;
        
        return result;
    }

    private double Model3()
    {
        double result = 0;
        int[,] stats = GenesToStats();
        for (int i = 0; i < 3; i++)
        {
            result += fit3[i] * (
                LinModel(stats[i,0], subFit3[0], subFit3[1]) +
                LogModel(stats[i,1], subFit3[2], subFit3[3], subFit3[4]) +
                RexpModel(stats[i,2], subFit3[5], subFit3[6], subFit3[7]));
        }
        return fit3[fit3.Length - 2] * result + fit3[fit3.Length - 1];
    }

    private double Model4()
    {
        double result = 0;
        int[,] stats = GenesToStats();
        for (int i = 0; i < 4; i++)
        {
            result += fit4[i] * (
                LinModel(stats[i,0], subFit4[0], subFit4[1]) +
                LinModel(stats[i,1], subFit4[2], subFit4[3]) +
                RexpModel(stats[i,2], subFit4[4], subFit4[5], subFit4[6]));
        }
        return fit4[fit4.Length - 2] * result + fit4[fit4.Length - 1];
    }

    private double Model5()
    {
        double result = 0;
        int[,] stats = GenesToStats();
        for (int i = 0; i < 5; i++)
        {
            result += fit5[i] * (
                LinModel(stats[i,0], subFit5[0], subFit5[1]) +
                LinModel(stats[i,1], subFit5[2], subFit5[3]) +
                RexpModel(stats[i,2], subFit5[4], subFit5[5], subFit5[6]));
        }
        return fit5[fit5.Length - 2] * result + fit5[fit5.Length - 1];
    }

    private double Model6()
    {
        double result = 0;
        int[,] stats = GenesToStats();
        for (int i = 0; i < 6; i++)
        {
            result += 
                fit6[i] * (
                LinModel(stats[i,0], subFit6[0], subFit6[1]) +
                LinModel(stats[i,1], subFit6[2], subFit6[3]) +
                RexpModel(stats[i,2], subFit6[4], subFit6[5], subFit6[6]));
        }
        return fit6[fit6.Length - 2] * result + fit6[fit6.Length - 1];
    }

    public int[,] GenesToStats()
    {
        var activeGenes = genes.FindAll(g => ((EnemyGene)g).IsActive());
        int[,] stats = new int[activeGenes.Count,3];
        for(int i = 0; i < activeGenes.Count; i++)
        {
            stats[i,0] = ((EnemyGene)genes[i]).GetHealth();
            stats[i,1] = ((EnemyGene)genes[i]).GetShotSpeed();
            stats[i,2] = ((EnemyGene)genes[i]).GetFireRate();
        }
        return stats;
    }

    public override string ToString()
    {
        string result = "";
        int[,] stats = GenesToStats();
        for(int i = 0; i < stats.GetLength(0); i++)
        {
            for (int j = 0; j < stats.GetLength(1); j++) {
                result += stats[i, j] + ", ";
            }
        }
        return result;
    }
}
