using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 캐릭터
    public float distance = 5.0f; // 캐릭터와 카메라 사이의 거리
    public float rotationSpeed = 5.0f; // 마우스 회전 속도
    public float zoomSpeed = 2.0f; // 마우스 휠 줌 속도
    public float minDistance = 2.0f; // 최소 거리
    public float maxDistance = 10.0f; // 최대 거리

    private float currentRotationX = 0f; // 현재 X축 회전 값
    private float currentRotationY = 0f; // 현재 Y축 회전 값

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        HandleMouseInput();
        AdjustDistance();
        UpdateCameraPosition();
    }

    // 마우스 입력 처리
    private void HandleMouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentRotationX += mouseX * rotationSpeed;
        currentRotationY -= mouseY * rotationSpeed;
        currentRotationY = Mathf.Clamp(currentRotationY, -80f, 80f); // Y축 회전 제한
    }

    // 마우스 휠 입력으로 거리 조절
    private void AdjustDistance()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance); // 거리 제한
    }

    // 카메라 위치와 회전 업데이트
    private void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        Vector3 offset = new Vector3(0, 0, -distance);
        Vector3 cameraPosition = target.position + rotation * offset;

        transform.position = cameraPosition;
        transform.LookAt(target);

        // 디버깅: 카메라와 타겟의 위치 출력
    }
}