using UnityEngine;

public class Player : MonoBehaviour
{
    private int collectedBarrels = 0;
    private int barrelsToCollect = 5;

    public bool MissionCompleted
    {
        get
        {
            return collectedBarrels == barrelsToCollect;
        }
    }

    private void OnGUI()
    {
        if (GameModeSwitch.IsHumanoidControlled)
        {
            GUI.Label(new Rect(10, 70, 300, 25), $"Barrels collected: {collectedBarrels}/{barrelsToCollect}");

            if (MissionCompleted)
            {
                GUI.Label(new Rect(10, 100, 300, 25), $"Land mission completed!");
            }
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (!MissionCompleted)
        {
            if (col.gameObject.tag == "Barrel")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    collectedBarrels++;
                    Destroy(col.gameObject);
                }
            }
        }
    }
}
