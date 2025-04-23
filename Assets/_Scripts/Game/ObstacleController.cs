using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public bool pointingOutward = true;

    // call this to randomly flip directions
    public void RandomFlip()
    {
        if (Random.value < 0.5f)
        {
            pointingOutward = !pointingOutward;
            transform.localRotation = Quaternion.Euler(0, 0, pointingOutward ? 0 : 180);
        }
    }
}
