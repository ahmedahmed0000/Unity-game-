using UnityEngine;

/// <summary>
/// كاميرا Third Person تتبع اللاعب من الخلف.
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Camera")]
    [SerializeField] private Vector3 offset =
        new Vector3(0f, 4f, -6f);

    [SerializeField] private float followSpeed = 8f;
    [SerializeField] private float rotationSpeed = 8f;

    private void LateUpdate()
    {
        if (target == null)
            return;

        // المكان المطلوب للكاميرا
        Vector3 targetPosition =
            target.TransformPoint(offset);

        // حركة ناعمة
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        // اتجاه الكاميرا ناحية اللاعب
        Vector3 lookDirection =
            target.position - transform.position;

        if (lookDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(lookDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
