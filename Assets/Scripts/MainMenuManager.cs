using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    private Button mission1;
    private Button mission2;
    private Button closeGame;
    private VisualElement root;

    void Start()
    {
        root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        mission1 = root.Q<Button>("Mission1");
        mission2 = root.Q<Button>("Mission2");
        closeGame = root.Q<Button>("CloseGame");

        mission1.RegisterCallback<ClickEvent>(Mission1);
        mission2.RegisterCallback<ClickEvent>(Mission2);
        closeGame.RegisterCallback<ClickEvent>(CloseGame);

    }

    private void Mission2(ClickEvent evt)
    {
        SceneManager.LoadScene("Mission2");
    }

    private void CloseGame(ClickEvent evt)
    {
        Application.Quit();
    }

    private void Mission1(ClickEvent evt)
    {
        SceneManager.LoadScene("Mission1");
    }

    void Update()
    {
        
    }
}
