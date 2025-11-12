using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : MonoBehaviour
{
    public Transform cameraTransform; // 카메라의 Transform을 Inspector에서 설정
    public float moveSpeed = 5.0f; // 이동 속도
    public CharacterAnimation characterAnimation; // CharacterAnimation 스크립트 참조

    private Vector2 moveInput; // InputAction에서 받은 이동 입력 값

    // Input System의 OnMove 액션에 연결될 메서드
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // 이동 입력 값 (x: 좌우, y: 앞뒤)

        // Walk 애니메이션 파라미터를 설정
        if (characterAnimation != null)
        {
            // bool 값을 CharacterAnimation.WalkState로 변환
            CharacterAnimation.WalkState state = moveInput != Vector2.zero
                ? CharacterAnimation.WalkState.Walking
                : CharacterAnimation.WalkState.Idle;

            characterAnimation.SetWalkAnimation(state);
        }
    }

    void Update()
    {
        HandleMovement(); // 매 프레임 이동 처리
    }

    // 이동 처리 메서드
    public void HandleMovement()
    {
        if (cameraTransform == null)
        {
            Debug.LogWarning("Camera Transform이 설정되지 않았습니다.");
            return;
        }

        // 입력 값이 없으면 이동하지 않음
        if (moveInput == Vector2.zero) return;

        // 카메라의 방향을 기준으로 이동 방향 계산
        Vector3 forward = cameraTransform.forward; // 카메라의 앞 방향
        Vector3 right = cameraTransform.right; // 카메라의 오른쪽 방향

        forward.y = 0; // 수평 이동만 적용
        right.y = 0; // 수평 이동만 적용

        Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x).normalized;

        // 캐릭터 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}