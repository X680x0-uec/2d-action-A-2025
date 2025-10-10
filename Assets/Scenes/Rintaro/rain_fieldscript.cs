using System.Collections.Generic;
using System.Linq;
using DigitalRuby.RainMaker;
using Unity.Collections;
using UnityEngine;

public class rain_fieldscript : MonoBehaviour
{
    public RainScript2D target;
    public RainScript2D parent;
    public List<ParticleSystem> p;
    public List<ParticleSystem> parentp;
    // Update is called once per frame
    void Start()
    {
        target = this.GetComponent<RainScript2D>();
    }
    void Update()
    {
        target.RainIntensity = parent.RainIntensity;
        target.WindForce = parent.WindForce;
        target.widthLimit = parent.widthLimit;
        int limit = p.Count;
        int parentlimit = parentp.Count;
        for (int i = 0; i < limit; i++)
        {
            for (int j = 0; j < parentlimit; j++)
            {
                Debug.Log(p[i].name == parentp[j].name);
                if (p[i].name == parentp[j].name)
                {
                    Debug.Log("true");
                    var shape = p[i].shape;
                    shape.radius = parentp[j].shape.radius;
                    var emit = p[i].emission;
                    emit.rateOverTime = parentp[j].emission.rateOverTime;
                    var velocity = p[i].velocityOverLifetime;
                    velocity.enabled = true;
                    velocity.x = parentp[j].velocityOverLifetime.x;
                    Debug.Log(velocity.x);
                }
            }
        }
    }
}
