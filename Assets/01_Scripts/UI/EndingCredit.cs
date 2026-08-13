using UnityEngine;

public class EndingCredit : MonoBehaviour
{
    [SerializeField] private RectTransform _creditRect;

    void Awake()
    {
        _creditRect = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        _creditRect.anchoredPosition += Vector2.up * Time.deltaTime * 100;
    }
}
