using UnityEngine;

public class FlashLightControl : MonoBehaviour
{
    private Light flashLight;

    private void Start()
    {
        flashLight = GetComponent<Light>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && GameModeSwitch.IsHumanoidControlled)
        {
            flashLight.enabled = !flashLight.enabled;
        }
    }
}
