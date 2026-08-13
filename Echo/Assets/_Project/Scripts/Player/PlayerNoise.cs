using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public sealed class PlayerNoise : MonoBehaviour
{
    [SerializeField, Min(0f)] private float noiseRadiusWhileMoving = 5f;

    private CharacterController characterController;

    public bool IsMakingNoise =>
        characterController != null &&
        characterController.isGrounded &&
        characterController.velocity.sqrMagnitude > 0.01f;

    public float NoiseRadius =>
        IsMakingNoise ? noiseRadiusWhileMoving : 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
}