using UnityEngine;

/// <summary>
/// تحريك لاعب كرة القدم باستخدام Joystick.
/// يحتاج CharacterController على اللاعب.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 12f;

    [Header("References")]
    [SerializeField] private Joystick joystick;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // قراءة اتجاه الجويستيك
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);

        // منع الحركة من تجاوز السرعة الطبيعية
        if (inputDirection.magnitude > 1f)
            inputDirection.Normalize();

        // الحركة
        controller.Move(inputDirection * moveSpeed * Time.deltaTime);

        // تدوير اللاعب ناحية اتجاه الحركة
        if (inputDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(inputDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
