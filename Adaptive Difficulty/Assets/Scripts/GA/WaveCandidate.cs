using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCandidate : Candidate
{
    public WaveCandidate()
    {
        //genes.Add(new WaveGene());
        for (int i = 0; i < 6; i++){
            genes.Add(new StatGene(1,30));
            genes.Add(new StatGene(1,51));
            genes.Add(new StatGene(5,120));
        }
    }

    public override List<Candidate> Crossover(Candidate candidate)
    {
        List<Candidate> result = new List<Candidate>();
        int r = (int)Mathf.Floor((Random.value - 0.0001f) * 5);
        List<Gene> cGenes = candidate.GetGenes();

        for(int i = 0; i < 6; i++)
        {

        }

        return result;
    }

    public override double Fitness()
    {
        throw new System.NotImplementedException();
    }

    public override void Mutate(double chance)
    {
        throw new System.NotImplementedException();
    }
}
