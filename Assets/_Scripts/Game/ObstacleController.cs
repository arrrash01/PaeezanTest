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
            obstacleTransform.DOLocalMoveY(obstacleTransform.localPosition.y + (pointingOutward ? 0.8f : -0.8f), 0.5f);//.localPosition = new Vector3(obstacleTransform.localPosition.x, obstacleTransform.localPosition.y + (pointingOutward ? 1 : -1));
        }
    }
}
