﻿using System;
using System.Linq;
using System.Collections.Generic;

public abstract class Candidate
{
    protected List<Gene> genes;
    protected double fitness = 0;

    public Candidate()
	{

	}
    public abstract void Fitness();

    public abstract List<Candidate> Crossover(List<Gene> candidate);

    public abstract void Mutate(double chance);

    public Gene GetGene(int i)
    {
        return genes[i];
    }

    public List<Gene> GetGenes()
    {
        return genes;
    }
}
