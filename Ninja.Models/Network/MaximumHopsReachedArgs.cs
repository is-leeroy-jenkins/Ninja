using System;

namespace Ninja.Models.Network;

public class MaximumHopsReachedArgs : EventArgs
{
    public MaximumHopsReachedArgs()
    {
    }

    public MaximumHopsReachedArgs(int hops)
    {
        Hops = hops;
    }

    public int Hops { get; set; }
}