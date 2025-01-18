using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadSceneOnClick : MonoBehaviour
{
    public string sceneName = "MainMenu";

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(action);
    }

    void action()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

}
