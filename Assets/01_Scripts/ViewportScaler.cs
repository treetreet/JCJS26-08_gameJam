using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ViewportScaler : MonoBehaviour
{
    private Camera _camera;

    [Header("기준 설정")]
    [Tooltip("스프라이트 등에 설정된 Pixels Per Unit (PPU) 값")]
    [SerializeField] private float pixelsPerUnit = 100f;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (_camera == null) return;

        if (_camera.orthographic)
        {
            float targetSize = Screen.height / (2f * pixelsPerUnit);
            
            if (targetSize > 0.01f)
            {
                _camera.orthographicSize = targetSize;
            }
        }
    }
}
