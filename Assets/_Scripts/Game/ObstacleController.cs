using UnityEngine;
using DG.Tweening;
public class ObstacleController : MonoBehaviour
{
    public bool pointingOutward = true;
    public Transform obstacleTransform;
    public void RandomFlip()
    {
        if (Random.value < 0.5f)
        {
            pointingOutward = !pointingOutward;
            obstacleTransform.DOLocalMoveY((pointingOutward ? 0f : -0.35f), .4f);
        }
    }
}
