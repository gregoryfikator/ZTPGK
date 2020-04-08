using UnityEngine;

public class LightPulse : MonoBehaviour
{
    private Light light;

    public float minIntensity;
    public float maxIntensity;

    private float targetIntensity;
    private float currentIntensity;

    private void Start()
    {
        light = GetComponent<Light>();
        targetIntensity = maxIntensity;
    }

    private void Update()
    {
        currentIntensity = Mathf.MoveTowards(light.intensity, targetIntensity, 0.1f);

        if (currentIntensity >= maxIntensity)
        {
            targetIntensity = minIntensity;
            currentIntensity = maxIntensity;
        }
        else if (currentIntensity <= minIntensity)
        {
            targetIntensity = maxIntensity;
            currentIntensity = minIntensity;
        }

        light.intensity = currentIntensity;
    }
}
