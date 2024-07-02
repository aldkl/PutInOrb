using UnityEngine;
using Spine.Unity;
using System.Collections;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using Unity.VisualScripting;

namespace PutInOrb
{
    public enum State
    {
        Move,
        Chatching,
        Putting,
        Beadmove,
        Hit,
    };

    public enum NomalType
    {
        Front,
        LSideDown,
        LSideCenter,
        LSideUp,
        Back,
        RSideUp,
        RSideCenter,
        RSideDown
    };
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float BeadmoveSpeed = 5f;
        public Vector2[] ChatchSetVec = new Vector2[8];
        [SerializeField] private Transform chatchTransform;

        public bool IsBeadMove;
        public int HitMeteoCount = 0;
        private PlayerInput playerInput;
        private Rigidbody2D playerRigidbody;
        private Animator playerAnimator;

        public State movingType = State.Move;
        public NomalType nomaltype = NomalType.Front;

        public GameObject StunnEffect;
        private SkeletonAnimation skeletonAnimation;
        private Vector2 _dir;

        private bool isChangeStateHitRunning = false;
        private string idle = "Idle";

        void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            playerRigidbody = GetComponent<Rigidbody2D>();
            playerAnimator = GetComponent<Animator>();
            skeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        void Update()
        {
            if (movingType == State.Move)
            {
                _dir.x = playerInput.move;
                _dir.y = playerInput.rotate;

                _dir.Normalize();

                UpdateSpineAnimation("Walk", _dir.y, _dir.x);
            }
            else if(movingType == State.Beadmove)
            {
                _dir.x = playerInput.move;
                _dir.y = playerInput.rotate;

                _dir.Normalize();

                UpdateSpineAnimation("Bead", _dir.y, _dir.x);
            }
            else if(movingType == State.Hit)
            {

            }
        }

        void UpdateSpineAnimation(string Type, float horizontal, float vertical)
        {
            string currentAnimation = "";

            if (vertical > 0.5f)
            {
                if (horizontal > 0.5f)
                {
                    currentAnimation = Type + "_RSide_Up";
                    nomaltype = NomalType.RSideUp;
                }
                else if (horizontal < -0.5f)
                {
                    currentAnimation = Type + "_LSide_Up";
                    nomaltype = NomalType.LSideUp;
                }
                else
                {
                    currentAnimation = Type + "_Back";
                    nomaltype = NomalType.Back;
                }
            }
            else if (vertical < -0.5f)
            {
                if (horizontal > 0.5f)
                {
                    currentAnimation = Type + "_RSide_Down";
                    nomaltype = NomalType.RSideDown;
                }
                else if (horizontal < -0.5f)
                {
                    currentAnimation = Type + "_LSide_Down";

                    nomaltype = NomalType.LSideDown;
                }
                else
                {
                    currentAnimation = Type + "_Front";

                    nomaltype = NomalType.Front;
                }
            }
            else
            {
                if (horizontal > 0.5f)
                {
                    currentAnimation = Type + "_RSide_Center";
                    nomaltype = NomalType.RSideCenter;
                }
                else if (horizontal < -0.5f)
                {
                    currentAnimation = Type + "_LSide_Center";
                    nomaltype = NomalType.LSideCenter;
                }
                else
                {
                    currentAnimation = TypesIdle(currentAnimation);
                }
            }
            SetchatchTransform();
            SetskeletonAnimation(currentAnimation);
        }
        string TypesIdle(string currentAnimation)
        {
            switch(nomaltype)
            {

                case NomalType.Front:
                    currentAnimation = idle + "_Front";
                    break;
                case NomalType.LSideDown:
                    currentAnimation = idle + "_LSide_Down";
                    break;
                case NomalType.LSideCenter:
                    currentAnimation = idle + "_LSide_Center";
                    break;
                case NomalType.LSideUp:
                    currentAnimation = idle + "_LSide_Up";
                    break;
                case NomalType.Back:
                    currentAnimation = idle + "_Back";
                    break;
                case NomalType.RSideUp:
                    currentAnimation = idle + "_RSide_Up";
                    break;
                case NomalType.RSideCenter:
                    currentAnimation = idle + "_RSide_Center";
                    break;
                case NomalType.RSideDown:
                    currentAnimation = idle + "_RSide_Down";
                    break;
            }
            return currentAnimation;
        }
        private IEnumerator ChangeStateBead()
        {
            yield return new WaitForSeconds(GameManager.instance.GetOrbduration);
            movingType = State.Beadmove;
            GameManager.instance.SetPlayerState_Put();
        }

        private IEnumerator ChangeStatePut()
        {
            OrbCreater orbCreater = FindObjectOfType<OrbCreater>();
            orbCreater.CreateOrb();
            yield return new WaitForSeconds(GameManager.instance.GetOrbduration);
            movingType = State.Move;
            GameManager.instance.SetPlayerState_Chach();
        }
        private IEnumerator ChangeStateHit()
        {
            isChangeStateHitRunning = true; // 코루틴 시작 시 true로 설정
            yield return new WaitForSeconds(GameManager.instance.HitStunTime);
            Debug.Log("HitPlat222");

            if (IsBeadMove)
                movingType = State.Beadmove;
            else
                movingType = State.Move;
            StunnEffect.SetActive(false);

            isChangeStateHitRunning = false; // 코루틴 종료 시 false로 설정
        }

        public void HitMeteo()
        {
            if (movingType == State.Beadmove)
            {
                IsBeadMove = true;
            }
            else
            {
                IsBeadMove = false;
            }
            Debug.Log("HitPlat");
            movingType = State.Hit;
            HitMeteoCount += 1;
            UpdateSpineAnimation("Hit", _dir.y, _dir.x);
            playerRigidbody.velocity = Vector2.zero;

            _dir.x = 0;
            _dir.y = 0;
            StunnEffect.SetActive(true);

            // 이미 코루틴이 실행 중인지 확인
            if (!isChangeStateHitRunning)
            {
                StartCoroutine(ChangeStateHit());
            }
        }

        void SetchatchTransform()
        {
            chatchTransform.localPosition = ChatchSetVec[(int)nomaltype];
        }

        void SetskeletonAnimation(string AnimationName)
        {
            if (skeletonAnimation.AnimationName != AnimationName)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, AnimationName, true);
            }
        }

        private void FixedUpdate()
        {
            Move();
        }
        // 입력값에 따라 캐릭터를 앞뒤로 움직임
        private void Move()
        {
            playerRigidbody.velocity = new Vector2(_dir.y * moveSpeed * Time.deltaTime, _dir.x * moveSpeed * Time.deltaTime);
        }
    }

}