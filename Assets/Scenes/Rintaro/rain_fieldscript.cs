using DigitalRuby.RainMaker;
using UnityEngine;

public class rain_fieldscript : MonoBehaviour
{
    public RainScript2D target;
    public RainScript2D parent;
    // Update is called once per frame
    void Start()
    {
        target = this.GetComponent<RainScript2D>();
    }
    void Update()
    {
        target.RainIntensity = parent.RainIntensity;
        target.WindForce = parent.WindForce;
    }
}
