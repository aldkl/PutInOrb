using UnityEngine;
using System.Collections;

public class MeteoCreater : MonoBehaviour
{
    public GameObject meteoPrefab; // Meteo 프리팹
    public Transform playerTransform; // 플레이어의 Transform
    public float initialDelay = 20f; // 초기 지연 시간 (20초)
    public float spawnInterval = 5f; // Meteo 생성 간격 (5초)
    public float spawnRadius = 5f; // Meteo가 플레이어 근처에 생성될 반경

    void Start()
    {
        // 초기 지연 시간 후 주기적으로 Meteo 생성 시작
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
            // Meteo 생성
            CreateMeteo();
            // 다음 생성까지 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void CreateMeteo()
    {
        // 플레이어 위치 근처의 랜덤한 위치 계산
        Vector3 spawnPosition = playerTransform.position + (Vector3)(Random.insideUnitCircle * spawnRadius);
        // Meteo 생성
        Instantiate(meteoPrefab, spawnPosition, Quaternion.identity);
    }
}
