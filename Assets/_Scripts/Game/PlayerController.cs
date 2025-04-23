using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Transform centerPoint;
    public float innerRadius = 2f, outerRadius = 3f;
    public float angularSpeed = 100f;
    public float radiusTweenDuration = 0.2f;

    private float currentRadius;
    private Tween radiusTween;
    private InputAction touchPress;

    void Awake()
    {
        DOTween.Init();
        EnhancedTouchSupport.Enable();

        currentRadius = outerRadius;
        transform.position = centerPoint.position + Vector3.right * currentRadius;

        // define touch action
        touchPress = new InputAction(binding: "<Touchscreen>/touch*/press");
        touchPress.performed += _ => OnTap();
        touchPress.Enable();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            OnTap();
        }
        transform.RotateAround(centerPoint.position, Vector3.forward, angularSpeed * Time.deltaTime);
        // enforce radius
        Vector3 dir = (transform.position - centerPoint.position).normalized;
        transform.position = centerPoint.position + dir * currentRadius;
    }

    void OnTap()
    {
        float target = Mathf.Approximately(currentRadius, outerRadius) ? innerRadius : outerRadius;
        radiusTween?.Kill();
        radiusTween = DOTween.To(() => currentRadius, x => currentRadius = x, target, radiusTweenDuration)
                             .SetEase(Ease.OutQuad);
    }
}
