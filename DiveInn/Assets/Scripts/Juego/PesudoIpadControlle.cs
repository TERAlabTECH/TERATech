using UnityEngine;
using UnityEngine.UI; // Include for UI elements

public class PesudoIpadControlle : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject lvlManagerObject;
    public LevelManager lvlManagerScript; // Your level manager

    private float moveY, moveX;

    void Start()
    {
        lvlManagerScript=lvlManagerObject.GetComponent<LevelManager>();
    }

    void Update()
    {
        if (!lvlManagerScript.paused)
        {
            // Apply movement and rotation based on button input
            Vector2 movement = new Vector2(0, moveY).normalized;
            transform.Translate(movement * moveSpeed * Time.deltaTime * 0.1f);

            Vector3 rotation = new Vector3(0, 0, -moveX).normalized;
            transform.Rotate(rotation * 100f * Time.deltaTime);
        }
    }

    // Functions triggered by button presses
    public void MoveUp() { moveY = 1f; }
    public void MoveDown() { moveY = -1f; }
    public void RotateLeft() { moveX = 1f; }
    public void RotateRight() { moveX = -1f; }

    // Optionally, reset movement/rotation when button is released
    public void StopMoving() { moveY = 0f; moveX = 0f; }
}
