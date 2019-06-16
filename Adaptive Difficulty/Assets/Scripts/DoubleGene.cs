using System;
using System.Linq;
using System.Collections.Generic;

public class DoubleGene : Gene
{
    public double val;
    public double max;
    public double min;

    public DoubleGene(double max, double min, double rand)
    {
        val = rand * (max - min) + min;
    }

    public override void Mutate()
    {
        Random r = new Random();
        val = r.NextDouble() * (max - min) + min;
    }

    public override double Value()
    {
        return val;
    }
}