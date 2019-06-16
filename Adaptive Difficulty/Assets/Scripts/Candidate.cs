using System;
using System.Linq;
using System.Collections.Generic;

public abstract class Candidate
{
    protected List<Gene> genes;

	public Candidate()
	{

	}
    public abstract double Fitness();

    public abstract List<Candidate> Crossover(Candidate candidate);

    public abstract void Mutate(double chance);

    public abstract string ToString();

    public Gene GetGene(int i)
    {
        return genes[i];
    }
}
