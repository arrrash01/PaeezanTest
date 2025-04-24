using UnityEngine;
using DG.Tweening;
public class ObstacleController : MonoBehaviour
{
    public bool pointingOutward = true;
    public Transform obstacleTransform;
    // call this to randomly flip directions
    public void RandomFlip()
    {
        if (Random.value < 0.5f)
        {
            Debug.Log("Changed direction of obstacle");
            pointingOutward = !pointingOutward;
            obstacleTransform.DOLocalMoveY((pointingOutward ? 0f : -0.35f), 1f);
        }
    }
}
