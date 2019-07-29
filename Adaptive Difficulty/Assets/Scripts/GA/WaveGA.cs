using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveGA : MonoBehaviour
{
    public Room room;
    private List<WaveCandidate> cands;
    private List<WaveCandidate> history;
    private int gen = 0;
    public int population;
    private WaveCandidate best;
    private bool restart = false;
    private int hits;
    public WaveCandidate Best { get => best; set => best = value; }
    public bool Restart { get => restart; set => restart = value; }
    public int Hits { get => hits; set => hits = value; }

    private void Start()
    {
        cands = new List<WaveCandidate>();
        history = new List<WaveCandidate>();
        for (int i = 0; i<population-1; i++)
        {
            cands.Add(new WaveCandidate());
        }
    }

    private void Update()
    {
        if (restart) NewGA();
        
        NextGen();
    }

    public void NextGen()
    {
        gen++;
        cands.OrderBy(c => c.FitScore);
        cands.Reverse();
        
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
            wc.Fitness(history);
            //Debug.Log(wc.ToString());
        }
        cands.OrderBy(c => c.FitScore);
        if (Best == null || Best.FitScore < cands[0].FitScore) Best = cands[0];
        
    }
    public void NewGA()
    {
        history.Add(Best);
        if (history.Count >= 2) { 
            history[history.Count-2].Hits = Hits;
            room.InsertCandidate(history[history.Count - 2]);
        }
        cands = new List<WaveCandidate>();
        for (int i = 0; i < population - 1; i++)
        {
            cands.Add(new WaveCandidate());
        }
        Best = null;
        restart = false;
    }
}