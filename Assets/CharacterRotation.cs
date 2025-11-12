using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    public Camera mainCamera; // 메인 카메라를 Inspector에서 설정
    public float rotationSpeed = 5.0f; // 회전 속도

    void Update()
    {
        if (mainCamera == null)
        {
            return;
        }

        // 카메라의 반대 방향을 기준으로 회전 적용
        ApplyRotation(-mainCamera.transform.forward);
    }

    // 회전 적용 메서드
    private void ApplyRotation(Vector3 forwardDirection)
    {
        forwardDirection.y = 0; // 캐릭터가 수평으로만 회전하도록 Y축 방향 제거

        if (forwardDirection.sqrMagnitude > 0.01f) // 방향 벡터가 유효한 경우에만 회전
        {
            Quaternion targetRotation = Quaternion.LookRotation(forwardDirection.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}