using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void GoToScene(int sceneNumber) => SceneManager.LoadScene(sceneNumber);
}