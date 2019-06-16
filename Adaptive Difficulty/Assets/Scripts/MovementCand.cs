using System;
using System.Linq;
using System.Collections.Generic;

public class MovementCand : Candidate
{
    private int hits;

    public MovementCand()
    {

    }

    public void NewCand()
    {
        genes.Add(new DoubleGene(100, -100));
        genes.Add(new DoubleGene(10000, -10000));
        genes.Add(new DoubleGene(10000, -10000));
        genes.Add(new DoubleGene(10000, -10000));
        genes.Add(new DoubleGene(10000, -10000));
        genes.Add(new DoubleGene(10000, -10000));
        genes.Add(new DoubleGene(10000, -10000));
    }

    public override List<Candidate> Crossover(Candidate candidate)
    {
        Random r = new Random();
        List<Candidate> result = new List<Candidate>();
        MovementCand c1 = new MovementCand();
        MovementCand c2 = new MovementCand();
        MovementCand temp = new MovementCand();
        for (int i = 0; i < 7; i++)
        {
            if(r.Next() > 0.5)
            {
                temp = c1;
                c1 = c2;
                c2 = temp;
            }
            c1.AddGene(genes[i]);
            c2.AddGene(candidate.GetGene(i));
        }
        result.Add(c1);
        result.Add(c2);
        return result;
    }

    public override void Mutate(double chance)
    {
        Random r = new Random();
        foreach (Gene g in genes)
            if (r.Next() < chance)
                g.Mutate();
    }

    private void AddGene(Gene gene)
    {
        genes.Add(gene);
    }

    public override double Fitness() { return 1/hits; }

    public void SetScore(int hits)
    {
        this.hits = hits;
    }

    public override string ToString()
    {
        string result = "";
        foreach(Gene g in genes)
        {
            result += g.Value() + " ";
        }
        return result;
    }
}