using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public enum WalkState
    {
        Idle,   // 걷지 않는 상태
        Walking // 걷는 상태
    }

    public GameObject model; // 모델 게임 오브젝트를 Inspector에서 설정
    private Animator modelAnimator; // 모델의 Animator를 Inspector에서 설정
    private WalkState currentWalkState = WalkState.Idle; // 현재 Walk 상태를 저장
    public bool isWalking = false; // 걷기 상태 플래그
    void Start()
    {
        if (model != null)
        {
            modelAnimator = model.GetComponent<Animator>();
            if (modelAnimator == null)
            {
                Debug.LogWarning("모델에 Animator 컴포넌트가 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("모델 게임 오브젝트가 설정되지 않았습니다.");
        }
    }
    // Walk 애니메이션 파라미터를 설정
    public void SetWalkAnimation(WalkState state)
    {
        Debug.Log($"Requested Walk Animation State: {state}");
        if (modelAnimator == null)
        {
            Debug.LogWarning("Animator가 설정되지 않았습니다.");
            return;
        }

        // 상태가 변경된 경우에만 Animator 업데이트
        if (currentWalkState != state)
        {
            bool isWalking = (state == WalkState.Walking);
            Debug.Log($"Setting Walk Animation to: {isWalking}");

            modelAnimator.SetBool("Walk", isWalking); // Walk 값을 설정

            // 현재 Walk 파라미터 값 확인
            Debug.Log($"Animator Walk Parameter Value: {modelAnimator.GetBool("Walk")}");

            currentWalkState = state; // 현재 상태 업데이트
        }
    }
}