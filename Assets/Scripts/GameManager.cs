﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool m_IsConsoleTyping;

    // Singleton
    public static GameManager m_Instance;

    // Singleton initialization
    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        HideCursor();
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}