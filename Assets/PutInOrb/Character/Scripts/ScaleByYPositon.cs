using UnityEngine;

public class ScaleByYPosition : MonoBehaviour
{
    // Y�࿡ ���� �ּ� �� �ִ� ������ ��
    public float minScale = 1;
    public float maxScale = 2;

    // ������ ��ȭ�� ����� Y�� ����
    public float minY = -33f;
    public float maxY = 26f;

    void Update()
    {
        // ��ü�� ���� Y ��ġ
        float yPosition = transform.position.y;

        // Y ��ġ�� �ּ� �� �ִ� Y �� ���̷� ����ȭ
        float normalizedY = Mathf.InverseLerp(minY, maxY, yPosition);

        // ����ȭ�� Y ���� �̿��� ������ ���
        float scale = Mathf.Lerp(maxScale, minScale, normalizedY);

        // ��ü�� ������ ����
        transform.localScale = new Vector2(scale, scale);
    }
}