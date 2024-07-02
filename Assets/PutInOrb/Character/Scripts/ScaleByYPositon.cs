using UnityEngine;

public class ScaleByYPosition : MonoBehaviour
{
    // Y축에 따른 최소 및 최대 스케일 값
    public float minScale = 1;
    public float maxScale = 2;

    // 스케일 변화가 적용될 Y축 범위
    public float minY = -33f;
    public float maxY = 26f;

    void Update()
    {
        // 객체의 현재 Y 위치
        float yPosition = transform.position.y;

        // Y 위치를 최소 및 최대 Y 값 사이로 정규화
        float normalizedY = Mathf.InverseLerp(minY, maxY, yPosition);

        // 정규화된 Y 값을 이용해 스케일 계산
        float scale = Mathf.Lerp(maxScale, minScale, normalizedY);

        // 객체의 스케일 변경
        transform.localScale = new Vector2(scale, scale);
    }
}