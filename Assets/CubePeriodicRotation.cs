using System.Collections;
using UnityEngine;

public class CubePeriodicRotation : MonoBehaviour
{
    private float time = 0.0f;
    private float periodTime = 2.0f;

    private bool isRotating = false;

    public float rotationTime = 2.0f;
    public float rotationSpeed = 45.0f;

    private void Start()
    {
        time = Random.Range(0.0f, periodTime);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > periodTime && !isRotating)
        {
            time -= periodTime;
            
            StartCoroutine(RotateForSeconds());
        }
    }

    private IEnumerator RotateForSeconds() 
    {
        isRotating = true;

        float remainingRotationTime = rotationTime;

        while (true)
        {
            if (remainingRotationTime > 0.0f)
            {
                transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
                remainingRotationTime -= Time.deltaTime;
                yield return null;
            }
            else
            {
                isRotating = false;
                time = 0.0f;
                yield break;
            }
        }
    }
}
