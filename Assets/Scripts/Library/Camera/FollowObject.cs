using UnityEngine;

public class FollowObject : MonoBehaviour {
    public Transform objectToFollow;
    public float followingTime = Constants.CAMERA_FOLLOWING_SPEED;
    private Vector3 velocity = Vector3.zero;
    
    void LateUpdate(){
        Vector3 targetPosition = new Vector3(objectToFollow.position.x, objectToFollow.position.y, transform.position.z);
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followingTime);
        transform.position = newPosition;
    }
}