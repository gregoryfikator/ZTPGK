using UnityEngine;

public class PickupBoxTrigger : MonoBehaviour
{
    public int effectId;

    private void Start()
    {
        if (effectId == -1)
        {
            effectId = Random.Range(0, 6);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ship")
        {
            switch (effectId)
            {
                case 0:
                    col.attachedRigidbody.AddForce(col.transform.forward * 20.0f, ForceMode.VelocityChange);
                    break;
                case 1:
                    col.attachedRigidbody.AddForce(-col.transform.forward * 20.0f, ForceMode.VelocityChange);
                    break;
                case 2:
                    col.GetComponent<ShipPhysics>().maxSpeed *= 2.0f;
                    break;
                case 3:
                    col.GetComponent<ShipPhysics>().maxSpeed /= 1.5f;
                    break;
                case 4:
                    col.GetComponent<ShipPhysics>().maneuverability *= 1.25f;
                    break;
                case 5:
                    col.GetComponent<ShipPhysics>().accelerationSide *= 1.25f;
                    break;
                case 6:
                    col.GetComponent<ShipPhysics>().accelerationForward *= 1.25f;
                    break;
            }

            col.GetComponent<Ship>().CollectBox();

            Destroy(gameObject);
        }
    }
}
