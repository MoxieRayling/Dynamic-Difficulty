using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

public class MovementGA : GeneticAlgorithm
{
    private int generation = 0;
    private int pop = 20;

    public MovementGA()
    {
        GenerationZero();
    }

    public void GenerationZero()
    {
        for (int i = 0; i < pop; i++)
        {
            cands.Add(new MovementCand());
        }
    }

    public void NextGen()
    {
        var nextGen = new List<Candidate>();
        cands.OrderBy(x => x.Fitness());
        Write(cands[0]);
        nextGen.Add(cands[0]);
        Random r = new Random();
        for (int i = 0; i < pop / 2; i++)
        {
            Candidate c1 = cands[i];
            Candidate c2 = cands[r.Next(0 / pop - 1)];
            List<Candidate> result = c1.Crossover(c2);
            cands.Remove(c1);
            cands.Remove(c2);
            nextGen.Concat(result);
        }
        nextGen.ForEach(x => x.Mutate(0.1));
        cands = nextGen;
        generation++;
    }

    public List<Candidate> GetCandidates()
    {
        return cands;
    }

    public void Write(Candidate cand)
    {
        string docPath =
          Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "GA.txt")))
        {
            outputFile.WriteLine("The best candidate for generation " + generation + " is " + cands.ToString());
        }
    }
}
