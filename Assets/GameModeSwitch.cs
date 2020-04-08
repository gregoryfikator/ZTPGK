using UnityEngine;

public class GameModeSwitch : MonoBehaviour
{
    public static bool IsHumanoidControlled { get; set; }

    public Camera shipCamera;
    public Camera humanCamera;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 40, 200, 25), (IsHumanoidControlled ? "Go back to the ship" : "Go ashore") + " (C)"))
        {
            ToggleGameMode();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleGameMode();
        }
    }

    private void ToggleGameMode()
    {
        IsHumanoidControlled = !IsHumanoidControlled;

        SwitchCamera();
    }

    private void SwitchCamera()
    {
        humanCamera.enabled = IsHumanoidControlled;
        shipCamera.enabled = !IsHumanoidControlled;
    }
}
