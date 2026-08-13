using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public sealed class RestartCurrentScene : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current?.rKey.wasPressedThisFrame != true)
        {
            return;
        }

        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}