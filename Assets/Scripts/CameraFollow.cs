using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [Header("Camera Offset")]
    public Vector3 offset = new Vector3(0f, 6f, -10f);

    [Header("Side Movement")]
    public float sideOffset = 2f;
    public float sideMoveSpeed = 5f;

    [Header("Wall Clipping Stuff")]
    public float cameraRadius = 0.3f;
    public float wallPadding = 0.1f;
    public LayerMask wallLayers;

    private float targetSideOffset = 0f;
    private float currentSideOffset = 0f;

    void LateUpdate()
    {
        if (target == null)
        return;

        currentSideOffset = Mathf.Lerp
        (currentSideOffset, targetSideOffset, sideMoveSpeed * Time.deltaTime);
        // above, this moves the camera

        Vector3 finalOffset = offset;
        finalOffset.x += currentSideOffset;
        // above, this finds the position

        Vector3 rotatedOffset = target.rotation * finalOffset;
        Vector3 desiredPosition = target.position + rotatedOffset;

        //below, wall stuff
        Vector3 direction = desiredPosition - target.position;
        float distance = direction.magnitude;

        if (Physics.SphereCast(target.position, cameraRadius, direction.normalized, out RaycastHit hit,
        distance, wallLayers)) //no idea here man
        {
            desiredPosition = target.position + direction.normalized * (hit.distance - wallPadding);
        }

        transform.position = desiredPosition;

        transform.LookAt(target.position + target.forward *2f);
        //direction of camera at all times, looking ahead of the player???
    }

    public void MoveCameraLeft()
    {
        targetSideOffset = -sideOffset;
    }

    public void MoveCameraRight()
    {
        targetSideOffset = sideOffset;
    }

    public void ResetCameraSide()
    {
        targetSideOffset = 0f;
    }

    

}
