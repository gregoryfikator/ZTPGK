using UnityEngine;

public class Ship : MonoBehaviour
{
    private int collectedBoxes = 0;
    private int boxesToCollect = 10;

    public bool MissionCompleted
    {
        get
        {
            return collectedBoxes == boxesToCollect;
        }
    }

    private void OnGUI()
    {
        if (!GameModeSwitch.IsHumanoidControlled)
        {
            GUI.Label(new Rect(10, 70, 300, 25), $"Boxes collected: {collectedBoxes}/{boxesToCollect}");

            if (MissionCompleted)
            {
                GUI.Label(new Rect(10, 100, 300, 25), $"Sailing mission completed! Congratulations!");
            }
        }
    }

    public void CollectBox()
    {
        collectedBoxes++;
    }
}