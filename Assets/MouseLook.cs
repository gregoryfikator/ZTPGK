using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 50.0f;

    protected float mouseX;
    protected float mouseY;

    protected float xRotation;
    protected float yRotation;

    protected void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        xRotation = transform.localRotation.eulerAngles.x;
        yRotation = transform.localRotation.eulerAngles.y;
    }

    protected void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        yRotation -= mouseX;
    }
}
