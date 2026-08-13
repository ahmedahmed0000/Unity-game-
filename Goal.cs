using UnityEngine;

/// <summary>
/// يكتشف دخول الكرة للجون ويزيد السكور.
/// ضع Collider على منطقة الجون واجعله Is Trigger.
/// </summary>
public class Goal : MonoBehaviour
{
    [Header("Goal")]
    [SerializeField] private bool isPlayerGoal = false;

    private void OnTriggerEnter(Collider other)
    {
        // نتأكد أن الجسم هو الكرة
        if (!other.CompareTag("Ball"))
            return;

        // تسجيل الهدف
        ScoreManager.Instance.AddGoal(isPlayerGoal);

        // إعادة الكرة
        Rigidbody ballRb = other.GetComponent<Rigidbody>();

        if (ballRb != null)
        {
            ballRb.linearVelocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
        }

        // إعادة الكرة لمنتصف الملعب
        other.transform.position =
            ScoreManager.Instance.GetBallSpawnPosition();
    }
}
