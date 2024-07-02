using UnityEngine;


namespace PutInOrb
{
    public class OrbCreater : MonoBehaviour
    {
        public GameObject[] orbPrefabs; // �� ������ Orb ������ �迭
        public Transform spawnPoint; // Orb�� ������ ��ġ
        public Transform entryPoint; // Orb�� ���ߴ� ��ġ
        private GameObject currentOrb; // ���� �� �Ա��� �ִ� Orb

        public int MaxGoldOrbCreate = 10;
        private int curGoldOrbcreated = 0;
        void Start()
        {
            CreateOrb();
        }

        public void CreateOrb()
        {
            // �������� Orb ������ ����
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

            // ���ο� Orb ����
            currentOrb = Instantiate(orbPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = currentOrb.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1.0f; // �߷� ����
        }

        void Update()
        {
            if (currentOrb != null)
            {
                // Orb�� �� �Ա��� �����ϸ� ���߱�
                if (Vector2.Distance(currentOrb.transform.position, entryPoint.position) < 0.3f)
                {
                    Rigidbody2D rb = currentOrb.GetComponent<Rigidbody2D>();
                    rb.gravityScale = 0.0f; // �߷� ��Ȱ��ȭ
                    currentOrb.transform.position = entryPoint.position;
                    currentOrb.GetComponent<Rigidbody2D>().velocity = Vector2.zero;// ��Ȯ�� �Ա� ��ġ�� ����
                    //ReleaseOrb();
                }
            }
        }

        void ReleaseOrb()
        {
            if (currentOrb != null)
            {
                // Orb�� �� ������ ����
                Rigidbody2D rb = currentOrb.GetComponent<Rigidbody2D>();
                rb.gravityScale = 1.0f; // �߷� �ٽ� Ȱ��ȭ
                currentOrb = null; // ���� Orb ����
            }
        }
        void OnDestroy()
        {
            // ���� ����� �� ������ Orb�� ����
            if (currentOrb != null)
            {
                Destroy(currentOrb);
            }
        }
    }
}