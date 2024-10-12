using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RainToMouseHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particleSystems;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        ParallaxRain();
    }

    private void ParallaxRain()
    {
        var screenWidth = Screen.width;
        var mouseX = Mathf.Clamp(Input.mousePosition.x, 0, screenWidth);
        var relativePosition = (mouseX / screenWidth) - 0.5f;

        foreach (var particleSystem in particleSystems)
        {
            var fo = particleSystem.forceOverLifetime;
            fo.enabled = true;
            fo.x = relativePosition * -1.5f;
        }

        //Debug.Log($"{mouseX} , {Screen.width}, {relativePosition}");
    }
}
