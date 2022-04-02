using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorOrigin : Attractor
{
    void FixedUpdate ()
    {
        foreach (Attractor attractor in Attractors)
        {
            if (attractor != this)
                Attract(attractor);
        }
    }
}
