using UnityEngine;


namespace PutInOrb
{
    public class OrbCreater : MonoBehaviour
    {
        public GameObject[] orbPrefabs; // 각 색깔의 Orb 프리팹 배열
        public Transform spawnPoint; // Orb가 생성될 위치
        public Transform entryPoint; // Orb가 멈추는 위치
        private GameObject currentOrb; // 현재 통 입구에 있는 Orb

        public int MaxGoldOrbCreate = 10;
        private int curGoldOrbcreated = 0;
        void Start()
        {
            CreateOrb();
        }

        public void CreateOrb()
        {
            // 랜덤으로 Orb 프리팹 선택
            int randomIndex = 0;
            if (GameManager.instance.IsFever)
            {
                if(MaxGoldOrbCreate > curGoldOrbcreated)
                {
                    randomIndex = Random.Range(0, orbPrefabs.Length);
                    if(randomIndex == 5)
                    {
                        curGoldOrbcreated += 1;
                    }
                }
                else
                {
                    GameManager.instance.IsFever = false;
                }
            }
            else
            {

                randomIndex = Random.Range(0, orbPrefabs.Length - 1);
            }    

            GameObject orbPrefab = orbPrefabs[randomIndex];

            // 새로운 Orb 생성
            currentOrb = Instantiate(orbPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = currentOrb.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1.0f; // 중력 적용
        }

        void Update()
        {
            if (currentOrb != null)
            {
                // Orb가 통 입구에 도착하면 멈추기
                if (Vector2.Distance(currentOrb.transform.position, entryPoint.position) < 0.3f)
                {
                    Rigidbody2D rb = currentOrb.GetComponent<Rigidbody2D>();
                    rb.gravityScale = 0.0f; // 중력 비활성화
                    currentOrb.transform.position = entryPoint.position;
                    currentOrb.GetComponent<Rigidbody2D>().velocity = Vector2.zero;// 정확히 입구 위치로 설정
                    //ReleaseOrb();
                }
            }
        }

        void ReleaseOrb()
        {
            if (currentOrb != null)
            {
                // Orb를 통 안으로 배출
                Rigidbody2D rb = currentOrb.GetComponent<Rigidbody2D>();
                rb.gravityScale = 1.0f; // 중력 다시 활성화
                currentOrb = null; // 현재 Orb 리셋
            }
        }
        void OnDestroy()
        {
            // 씬이 종료될 때 생성된 Orb를 제거
            if (currentOrb != null)
            {
                Destroy(currentOrb);
            }
        }
    }
}