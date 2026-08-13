using UnityEngine;
using Player;

public class Beam : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    public bool hasReflected = false;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // 물리적으로 서로 밀쳐내지 않고 통과(겹침)하도록 트리거로 설정
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }
    
    void Update()
    {
        this.transform.Translate(Vector2.right * Time.deltaTime * 5);

        if (mainCamera != null)
        {
            Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);
            bool outOfBounds = false;
            Vector2 normal = Vector2.zero;

            if (viewPos.x < 0f)
            {
                normal = Vector2.right;
                viewPos.x = 0.01f;
                outOfBounds = true;
            }   
            else if (viewPos.x > 1f)
            {
                normal = Vector2.left;
                viewPos.x = 0.99f;
                outOfBounds = true;
            }

            if (viewPos.y < 0f)
            {
                normal = Vector2.up;
                viewPos.y = 0.01f;
                outOfBounds = true;
            }
            else if (viewPos.y > 1f)
            {
                normal = Vector2.down;
                viewPos.y = 0.99f;
                outOfBounds = true;
            }

            if (outOfBounds)
            {
                if(hasReflected)
                    Destroy(this.gameObject);
                Vector2 reflectDir = Vector2.Reflect(transform.right, normal);
                float angle = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);

                Vector3 worldPos = mainCamera.ViewportToWorldPoint(viewPos);
                worldPos.z = transform.position.z;
                transform.position = worldPos;

                hasReflected = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.DecreaseHealth(damage);
                Destroy(gameObject);
            }
        }
        else if (hasReflected && collision.gameObject.TryGetComponent<TowerHealth>(out var towerHealth))
        {
            towerHealth.Damaged(damage);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.DecreaseHealth(damage);
                Destroy(gameObject);
            }
        }
        else if (hasReflected && collision.gameObject.TryGetComponent<TowerHealth>(out var towerHealth))
        {
            towerHealth.Damaged(damage);
            Destroy(gameObject);
        }
    }
}
