using UnityEngine;

public class PlayerMouseLook : MouseLook
{
    public Transform player;

    protected new void Update()
    {
        if (GameModeSwitch.IsHumanoidControlled)
        {
            base.Update();

            transform.localRotation = Quaternion.Euler(xRotation, -yRotation, 0.0f);

            player.Rotate(Vector3.up * mouseX);
        }
    }
}
