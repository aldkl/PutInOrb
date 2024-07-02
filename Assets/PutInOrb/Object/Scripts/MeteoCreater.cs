using UnityEngine;
using System.Collections;

public class MeteoCreater : MonoBehaviour
{
    public GameObject meteoPrefab; // Meteo ������
    public Transform playerTransform; // �÷��̾��� Transform
    public float initialDelay = 20f; // �ʱ� ���� �ð� (20��)
    public float spawnInterval = 5f; // Meteo ���� ���� (5��)
    public float spawnRadius = 5f; // Meteo�� �÷��̾� ��ó�� ������ �ݰ�

    void Start()
    {
        // �ʱ� ���� �ð� �� �ֱ������� Meteo ���� ����
        Invoke("StartMeteoSpawning", initialDelay);
    }

    void StartMeteoSpawning()
    {
        StartCoroutine(SpawnMeteo());
    }

    IEnumerator SpawnMeteo()
    {
        while (true)
        {
            // Meteo ����
            CreateMeteo();
            // ���� �������� ���
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void CreateMeteo()
    {
        // �÷��̾� ��ġ ��ó�� ������ ��ġ ���
        Vector3 spawnPosition = playerTransform.position + (Vector3)(Random.insideUnitCircle * spawnRadius);
        // Meteo ����
        Instantiate(meteoPrefab, spawnPosition, Quaternion.identity);
    }
}
