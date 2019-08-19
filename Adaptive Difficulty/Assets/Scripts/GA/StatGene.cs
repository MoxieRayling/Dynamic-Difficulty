using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatGene : Gene
{
    private int upperBound;
    private int lowerBound;
    private int value;

    public StatGene(int lowerBound, int upperBound)
    {
        this.lowerBound = lowerBound;
        this.upperBound = upperBound;
        Mutate();
    }

    public override void Mutate()
    {
        value = (int)(Random.value * (upperBound - lowerBound) + lowerBound);
    }

    public int Value()
    {
        return value;
    }
    public void Max()
    {
        value = upperBound;
    }
    public void Min()
    {
        value = lowerBound;
    }
}
