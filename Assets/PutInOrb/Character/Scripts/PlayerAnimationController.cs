using UnityEngine;
using Spine.Unity;
using UnityEditor.Animations;
using UnityEditorInternal;
using Spine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private SkeletonAnimation skeletonAnimation;
    public UnityEditor.Animations.BlendTree blendTree;

    private void Start()
    {
        animator = GetComponent<Animator>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    void OnAnimatorMove()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        Debug.Log(blendTree.children[0].ToString());
        
        // 현재 Animator 상태에 따른 Spine 애니메이션 설정
        if (stateInfo.IsName("Walk_Front"))
        {
            SetSpineAnimation("Walk_Front");
        }
        else if (stateInfo.IsName("Walk"))
        {
            SetSpineAnimation("Walk_LSide_Down");
        }
        else if (stateInfo.IsName("Walk_LSide_Center"))
        {
            SetSpineAnimation("Walk_LSide_Center");
        }
        else if (stateInfo.IsName("Walk_LSide_Up"))
        {
            SetSpineAnimation("Walk_LSide_Up");
        }
        else if (stateInfo.IsName("Walk_RSide_Down"))
        {
            SetSpineAnimation("Walk_RSide_Down");
        }
        else if (stateInfo.IsName("Walk_RSide_Center"))
        {
            SetSpineAnimation("Walk_RSide_Center");
        }
        else if (stateInfo.IsName("Walk_RSide_Up"))
        {
            SetSpineAnimation("Walk_RSide_Up");
        }
        else
        {
            SetSpineAnimation("Walk_Front");
        }
    }

    void SetSpineAnimation(string animationName)
    {
        if (skeletonAnimation.AnimationName != animationName)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, animationName, true);
        }
    }
}