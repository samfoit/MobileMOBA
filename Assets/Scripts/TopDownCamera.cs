using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target;
    public float height = 10f;
    public float distance = 20f;
    public float angle = 0f;


    // Start is called before the first frame update
    void Start()
    {
        HandleCamera();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();
    }

    public void HandleCamera()
    {
        if (!target) { return; }

        //Camera's world position
        Vector3 worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);
        Debug.DrawLine(target.position, worldPosition, Color.red);

        //Camera's rotation
        Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;
        Debug.DrawLine(target.position, rotatedVector, Color.green);

        //Move our position
        Vector3 flatTargetPosition = target.position;
        flatTargetPosition.y = 0f;
        Vector3 finalPosition = flatTargetPosition + rotatedVector;
        Debug.DrawLine(target.position, finalPosition, Color.blue);

        transform.position = finalPosition;
        transform.LookAt(flatTargetPosition);
    }
}
