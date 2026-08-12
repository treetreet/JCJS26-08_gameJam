using UnityEngine;

public class Beam : MonoBehaviour
{
    float timeAfterGened = 0f;
    public bool hasReflected = false;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector2.right * Time.deltaTime * 5);
        timeAfterGened += Time.deltaTime;
        
        if(timeAfterGened >= 5f)
        {
            Destroy(this.gameObject);
            return;
        }

        if (!hasReflected && mainCamera != null)
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
}
