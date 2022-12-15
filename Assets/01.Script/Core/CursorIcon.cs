using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorIcon : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture = null;

    private void Awake()
    {
        SetCursorIcon();
    }

    private void SetCursorIcon() //커서 변경
    {
        Cursor.SetCursor(_cursorTexture, new Vector2(_cursorTexture.width / 1.5f, _cursorTexture.height / 1.5f), CursorMode.Auto);
    }
}
