using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    private ParticleSystem ps;
    public float hSliderValue = 1.0F;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        var main = ps.main;
        main.simulationSpeed = hSliderValue;
    }
}