using UnityEngine;

/// <summary>
/// التحكم في الكرة باستخدام Rigidbody.
/// اللاعب يستطيع تسديد الكرة عندما تكون قريبة منه.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class FootballController : MonoBehaviour
{
    [Header("Ball Settings")]
    [SerializeField] private float kickForce = 10f;
    [SerializeField] private float upwardForce = 1.5f;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Kick Settings")]
    [SerializeField] private float kickDistance = 2f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // إعدادات فيزياء الكرة
        rb.mass = 0.45f;
        rb.drag = 0.15f;
        rb.angularDrag = 0.05f;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode =
            CollisionDetectionMode.ContinuousDynamic;
    }

    /// <summary>
    /// اربط هذه الدالة بزرار الشوت في الـ UI.
    /// </summary>
    public void KickBall()
    {
        if (player == null)
            return;

        // التأكد أن اللاعب قريب من الكرة
        float distance = Vector3.Distance(
            player.position,
            transform.position
        );

        if (distance > kickDistance)
            return;

        // اتجاه الشوت = اتجاه اللاعب
        Vector3 direction = player.forward;

        // إضافة قوة بسيطة لأعلى
        direction += Vector3.up * upwardForce;

        direction.Normalize();

        // تصفير جزء من سرعة الكرة قبل الشوت
        rb.linearVelocity *= 0.3f;

        // الشوت باستخدام الفيزياء
        rb.AddForce(
            direction * kickForce,
            ForceMode.Impulse
        );
    }
}
