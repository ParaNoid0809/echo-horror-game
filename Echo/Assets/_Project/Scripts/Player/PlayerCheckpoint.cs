using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public sealed class PlayerCheckpoint : MonoBehaviour
{
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        RestoreSavedCheckpoint();
    }

    public void SaveCheckpoint(Vector3 position, Quaternion rotation)
    {
        CheckpointSave.Save(
            SceneManager.GetActiveScene().name,
            position,
            rotation
        );
    }

    private void RestoreSavedCheckpoint()
    {
        if (!CheckpointSave.TryLoad(
                SceneManager.GetActiveScene().name,
                out Vector3 position,
                out Quaternion rotation))
        {
            return;
        }

        characterController.enabled = false;
        transform.SetPositionAndRotation(position, rotation);
        characterController.enabled = true;
    }
}