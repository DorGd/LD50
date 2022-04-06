using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorOrigin : Attractor
{
    private int updateInterval = 10;
    private int updatedCount = 0;
    
    void FixedUpdate ()
    {
        if (updatedCount % updateInterval == 0 && Attractors != null)
        {
            // Make sure there are humans in scene!
            foreach (Attractor attractor in Attractors)
            {
                if (attractor != this)
                    Attract(attractor);
            }
        }
        updatedCount++;
    }
}
