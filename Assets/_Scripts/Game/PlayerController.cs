using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Transform centerPoint;
    public float angularSpeed = 100f;
    public float radiusTweenDuration = 0.2f;

    private float currentRadius;
    private Tween radiusTween;
    private InputAction touchPress;

    void Awake()
    {
        DOTween.Init();
        EnhancedTouchSupport.Enable();

        // define touch action
        touchPress = new InputAction(binding: "<Touchscreen>/touch*/press");
        touchPress.performed += _ => OnTap();
        touchPress.Enable();
    }
    private void Start()
    {
        currentRadius = GameManager.instance.outerRadius;
        transform.position = centerPoint.position + Vector3.right * currentRadius;
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
        float target = Mathf.Approximately(currentRadius, GameManager.instance.outerRadius) ? GameManager.instance.innerRadius : GameManager.instance.outerRadius;
        radiusTween?.Kill();
        radiusTween = DOTween.To(() => currentRadius, x => currentRadius = x, target, radiusTweenDuration)
                             .SetEase(Ease.OutQuad);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided in Collision with: ");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.EndGame();
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided in trigger with: ");
        Debug.Log(collision.tag);

        if (collision.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            GameManager.instance.OnPointCollected();
        }
    }
}
