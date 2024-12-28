using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneChangerController : MonoBehaviour
{
    public UnityEvent sceneChangementAction;
    public int sceneId;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        sceneChangementAction.Invoke();
        SceneManager.LoadScene(sceneId);
    }
}
