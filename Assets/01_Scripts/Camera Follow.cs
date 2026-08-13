using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_Target;
    [SerializeField] private Vector3 m_Offset;

    [SerializeField] private float m_LimitMinX = -1000;
    [SerializeField] private float m_LimitMaxX = 1000;
    [SerializeField] private float m_LimitMinY = -1000;
    [SerializeField] private float m_LimitMaxY = 1000;

    private void LateUpdate()
    {
        Vector3 newPos = m_Target.position + m_Offset;
        if(newPos.x > m_LimitMaxX) newPos.x = m_LimitMaxX;
        if(newPos.x < m_LimitMinX) newPos.x = m_LimitMinX;
        if(newPos.y > m_LimitMaxY) newPos.y = m_LimitMaxY;
        if(newPos.y < m_LimitMinY) newPos.y = m_LimitMinY;
        transform.position = newPos + m_Offset;
    }
}