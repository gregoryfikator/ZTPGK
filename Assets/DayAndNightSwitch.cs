using UnityEngine;

public class DayAndNightSwitch : MonoBehaviour
{
    public static bool IsDay = true;

    public GameObject water;
    public Shader waterAtDayShader;
    public Shader waterAtNightShader;

    public Light light;
    public Flare sunFlare;

    public GameObject horizen;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 25), "Toggle day/night (N)"))
        {
            ToggleDayAndNight();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ToggleDayAndNight();
        }
    }

    private void ToggleDayAndNight()
    {
        IsDay = !IsDay;

        AdjustWater();

        AdjustLight();

        AdjustSky();
    }

    private void AdjustWater()
    {
        if (water)
        {
            water.GetComponent<MeshRenderer>().material.shader = IsDay ? waterAtDayShader : waterAtNightShader;
        }
    }

    private void AdjustLight()
    {
        if (light)
        {
            if (sunFlare)
            {
                light.flare = IsDay ? sunFlare : null;
            }

            light.intensity = IsDay ? 1.0f : 0.1f;
        }
    }

    private void AdjustSky()
    {
        if (horizen)
        {
            var horizonMaterial = horizen.GetComponent<MeshRenderer>().material;

            if (ColorUtility.TryParseHtmlString(IsDay ? "#0015BF00" : "#000000C0", out Color colorLevel1))
            {
                horizonMaterial.SetColor("_Level1Color", colorLevel1);
                horizonMaterial.SetFloat("_Level1", IsDay ? 10000 : 1000);
            }

            if (ColorUtility.TryParseHtmlString(IsDay ? "#93C7FDFF" : "#101020FF", out Color colorLevel0))
            {
                horizonMaterial.SetColor("_Level0Color", colorLevel0);
            }
        }
    }
}
