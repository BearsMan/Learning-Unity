using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private float xRot;
    public float mouseSensitivityX = 100f;
    public float mouseSensitivityY = 100f;
    public Transform cam;
    public Transform rotPoint;
    public Vector3 optimalCameraPosition;
    private float distance;
    public LayerMask playerMask;
    // Start is called before the first frame update
    void Start()
    {
        distance = optimalCameraPosition.magnitude;
    }

    private void Update()
    {

        transform.Rotate(Vector3.up * Input.GetAxis("Look Right"));
        xRot -= Input.GetAxis("Look Up");
        xRot = Mathf.Clamp(xRot, -10, 15);
        rotPoint.localRotation = Quaternion.Euler(xRot, 0, 0);

        RaycastHit hit;
        Ray ray = new Ray(rotPoint.position, optimalCameraPosition.normalized);
        if (Physics.Raycast(ray, out hit, distance, playerMask))
        {
            float newDistance = Vector3.Distance(transform.position, hit.point);
            float percent = (newDistance / distance);
            cam.localPosition = optimalCameraPosition * percent;

        }
        else
        {
            cam.localPosition = optimalCameraPosition;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position;
        //transform.rotation = target.rotation;
    }
}
