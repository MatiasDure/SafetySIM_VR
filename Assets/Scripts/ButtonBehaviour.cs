using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{

    public void SwitchScenes(string sceneName) => SceneManager.LoadScene(sceneName);

    public void QuitGame() => Application.Quit();

}
