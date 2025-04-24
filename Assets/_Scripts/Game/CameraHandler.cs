using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] float portraitSize, landScapeSize;
    ScreenOrientation lastOrient=ScreenOrientation.Portrait;
    void Update()
    {
        if (Screen.orientation != lastOrient)
        {
            lastOrient = Screen.orientation;
            OnOrientationChanged(lastOrient);
        }
    }
    void OnOrientationChanged(ScreenOrientation o)
    {
        Debug.Log(Camera.main.sensorSize);
        if (o == ScreenOrientation.LandscapeLeft || o == ScreenOrientation.LandscapeRight)
        {
            Camera.main.orthographicSize = landScapeSize;
        }
        else
        {
            Camera.main.orthographicSize = portraitSize;
        }
    }

}
