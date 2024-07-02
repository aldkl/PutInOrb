using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PutInOrb
{
    public class JoystickManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image imgJoystickBG;
        [SerializeField] private Image imgJoystick;

        private Vector2 posInput;

        public float Horizontal { get { return posInput.x; } }
        public float Vertical { get { return posInput.y; } }

        public bool chatch = false;
        public bool put = false;


        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgJoystickBG.rectTransform, eventData.position, eventData.pressEventCamera, out posInput))
            {//드래그가 일어나면 imgJoystickBG의 중심점을 기준으로 좌표를 out해준다

                //좌표 영역을 imgJoystickBG.rectTransform 크기에 비례시킨다
                posInput.x = posInput.x / (imgJoystickBG.rectTransform.sizeDelta.x / 2);
                posInput.y = posInput.y / (imgJoystickBG.rectTransform.sizeDelta.y / 2);
                //Debug.Log(posInput.ToString());

                //영역 넘어가면 노멀라이즈드 시켜주기
                if (posInput.magnitude > 1.0f)
                {
                    posInput = posInput.normalized;
                }


                //조이스틱 이동

                imgJoystick.rectTransform.anchoredPosition =
                    new Vector2(posInput.x * imgJoystickBG.rectTransform.sizeDelta.x / 2,
                                posInput.y * imgJoystickBG.rectTransform.sizeDelta.y / 2);

            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            posInput = Vector2.zero;
            imgJoystick.rectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetChatchDown(BaseEventData eventData)
        {
            chatch = true;
        }
        public void SetChatchUp(BaseEventData eventData)
        {
            chatch = false;
        }

        public void SetPutDown(BaseEventData eventData)
        {
            put = true;
        }
        public void SetPutUp(BaseEventData eventData)
        {
            put = false;
        }

    }
}