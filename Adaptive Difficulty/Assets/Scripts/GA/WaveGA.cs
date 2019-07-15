using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveGA : MonoBehaviour
{
    private List<WaveCandidate> cands;
    private int gen = 0;
    public int population;
    private WaveCandidate best;
    public WaveCandidate Best { get => best; set => best = value; }

    private void Start()
    {
        cands = new List<WaveCandidate>();
        for (int i = 0; i<population-1; i++)
        {
            cands.Add(new WaveCandidate());
        }
    }

    private void Update()
    {
        NextGen();
    }

    public void NextGen()
    {
        gen++;
        cands.OrderBy(c => c.FitScore);
        
        var nextGen = new List<WaveCandidate>();
        for(int i = 0; i < population/2; i++)
        {
            var temp = cands[i].Crossover(cands[Mathf.Min((int) Random.value*population,population-1)].GetGenes());
            nextGen.Add((WaveCandidate) temp[0]);
            nextGen.Add((WaveCandidate) temp[1]);
        }
        cands = nextGen;
        //Debug.Log("Gen " + gen);
        foreach(WaveCandidate wc in cands)
        {
            wc.Mutate(0.166);
            wc.Fitness();
            //Debug.Log(wc.ToString());
        }
        cands.OrderBy(c => c.FitScore);
        if (Best == null || Best.FitScore < cands[0].FitScore) Best = cands[0];
        
    }
}