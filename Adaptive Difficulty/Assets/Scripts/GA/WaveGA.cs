using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveGA
{
    private List<WaveCandidate> cands;
    private int population;
    private WaveCandidate best;
    public WaveCandidate Best { get => best; set => best = value; }

    public WaveGA(int population)
    {
        this.population = population;
        cands = new List<WaveCandidate>();

        for (int i = 0; i<population-1; i++)
        {
            cands.Add(new WaveCandidate());
        }
    }


    public void NextGen()
    {
        cands.ForEach(c => c.Fitness());
        cands.OrderBy(c => c.FitScore);
        if (Best == null || Best.FitScore < cands[0].FitScore) Best = cands[0];
        
        var nextGen = new List<WaveCandidate>();
        nextGen.Add(Best);
        for(int i = 0; i < population/2; i++)
        {
            var temp = cands[i].Crossover(cands[Mathf.Min((int) Random.value*population,population-1)].GetGenes());
            nextGen.Add((WaveCandidate) temp[0]);
            nextGen.Add((WaveCandidate) temp[1]);
        }
        cands = nextGen;
        nextGen.ForEach(c => c.Mutate(1/6));
    }
}