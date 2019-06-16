using System;

public abstract class Candidate
{
    protected List<Gene> genes;

	public Candidate()
	{

	}

    abstract double fitness();
}
