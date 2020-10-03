﻿using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEvent OnRestart = new UnityEvent();
    public UnityEvent<int> OnNextLevel = new UnityEvent<int>();
    public int CurrentLevel;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        OnRestart?.Invoke();
    }

    public void NextLevel()
    {
        CurrentLevel          ++         ;
        OnNextLevel?.Invoke(CurrentLevel);
    }
}

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameManager gameManager = GameManager.Instance;
        
        DrawDefaultInspector();

        GUILayout.BeginVertical();
        if (Application.isPlaying)
        {
            if (GUILayout.Button("Restart Game")) { gameManager.RestartGame(); }

            if (GUILayout.Button("Restart Level")) { gameManager.RestartLevel(); }

            if (GUILayout.Button("Next Level")) { gameManager.NextLevel(); }
        }
        GUILayout.EndVertical();
    }
}
