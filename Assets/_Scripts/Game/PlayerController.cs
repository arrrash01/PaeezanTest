using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Transform centerPoint;
    public float angularSpeed = 100f;
    public float maxAngularSpeed = 150f;
    public float radiusTweenDuration = 0.2f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip loseClip;
    private float currentRadius;
    private Tween radiusTween;
    

    void Awake()
    {
        DOTween.Init();
    }
    private void Start()
    {
        currentRadius = GameManager.instance.outerRadius;
        transform.position = centerPoint.position + Vector3.right * currentRadius;
    }
    void Update()
    {
        if (!GameManager.instance.isPlaying)
            return;
        if (Input.GetMouseButtonDown(0)) {
            OnTap();
        }
        transform.RotateAround(centerPoint.position, Vector3.forward, angularSpeed * Time.deltaTime);
        Vector3 dir = (transform.position - centerPoint.position).normalized;
        transform.position = centerPoint.position + dir * currentRadius;
    }

    void OnTap()
    {
        float target = Mathf.Approximately(currentRadius, GameManager.instance.outerRadius) ? GameManager.instance.innerRadius : GameManager.instance.outerRadius;
        radiusTween?.Kill();
        if (GameManager.instance.isPlaying)
        {
            audioSource.PlayOneShot(jumpClip);
        }
        radiusTween = DOTween.To(() => currentRadius, x => currentRadius = x, target, radiusTweenDuration)
                             .SetEase(Ease.OutQuad);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            audioSource.PlayOneShot(loseClip);
            if (PlayerPrefs.GetInt("Vibration_On", 1) == 1)
                Handheld.Vibrate();
            GameManager.instance.EndGame();
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            GameManager.instance.OnPointCollected();
            if (PlayerPrefs.GetInt("Vibration_On", 1) == 1)
                Handheld.Vibrate();
            audioSource.PlayOneShot(coinClip);
        }
    }
}
