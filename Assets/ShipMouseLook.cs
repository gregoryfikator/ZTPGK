using UnityEngine;

public class ShipMouseLook : MouseLook
{
    protected new void Update()
    {
        if (!GameModeSwitch.IsHumanoidControlled)
        {
            base.Update();

            transform.localRotation = Quaternion.Euler(xRotation, -yRotation, 0.0f);
        }
    }
}
