using System;
using System.Linq;
using System.Collections.Generic;

public abstract class Gene
{
    public Gene()
    {

    }

    public abstract void Mutate();
    public abstract double Value();
}