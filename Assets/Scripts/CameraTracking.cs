using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour {

    public Transform target;

    void Update()
    {
        float targetX = target.position.x;
        float targetY = target.position.y;
        transform.position = new Vector3(targetX, targetY, -100);
    }
}
