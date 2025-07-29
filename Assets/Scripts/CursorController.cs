using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    public static bool isLocked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && isLocked == false)
        {
            isLocked = true;
            LockCursor();
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame && isLocked)
        {
            isLocked = false;
            UnlockCursor();
        }
    }
    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
