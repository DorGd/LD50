using System.Collections.Generic;
using UnityEngine;

public class AttractorTarget : Attractor
{
    void OnEnable ()
    {
        if (Attractors == null)
            Attractors = new List<Attractor>();

        Attractors.Add(this);
    }

    void OnDisable ()
    {
        Attractors.Remove(this);
    }
}
