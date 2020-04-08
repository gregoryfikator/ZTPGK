using UnityEngine;

public class ShipPhysics : MonoBehaviour
{
    public float accelerationForward = 25.0f;
    public float accelerationBackward = 10.0f;
    public float accelerationSide = 15.0f;

    public float maxSpeed = 10.0f;
    public float maneuverability = 0.1f; 

    private Rigidbody rigidbody;

    public Player player;

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();

        maneuverability = Mathf.Clamp(maneuverability, 0.01f, 0.15f);
    }

    private void OnGUI()
    {
        if (!GameModeSwitch.IsHumanoidControlled)
        {
            if (!player.MissionCompleted)
            {
                var rect = new Rect(Screen.currentResolution.width / 2 - 150, Screen.currentResolution.height / 2 - 100, 300, 200);
                var guiStyle = new GUIStyle();
                guiStyle.normal.textColor = Color.red;
                guiStyle.fontSize = 20;
                GUI.Label(rect, "We cannot sail without rum! Before we go anywhere, we need to go ashore to find some barrels.", guiStyle);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!GameModeSwitch.IsHumanoidControlled && player.MissionCompleted)
        {
            if (rigidbody.velocity.magnitude < maxSpeed)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rigidbody.AddForce(transform.forward * Time.fixedDeltaTime * accelerationForward * 10.0f);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    rigidbody.AddForce(-transform.forward * Time.fixedDeltaTime * accelerationBackward * 10.0f);
                }
            }

            if (Mathf.Abs(rigidbody.angularVelocity.y) < maneuverability)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    rigidbody.AddTorque(transform.up * Time.fixedDeltaTime * accelerationSide * 50.0f);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    rigidbody.AddTorque(-transform.up * Time.fixedDeltaTime * accelerationSide * 50.0f);
                }
            }

            Debug.Log("Speed = " + rigidbody.velocity.magnitude + " Velocity = " + rigidbody.velocity + " Angular velocity = " + rigidbody.angularVelocity);
        }
    }
}
