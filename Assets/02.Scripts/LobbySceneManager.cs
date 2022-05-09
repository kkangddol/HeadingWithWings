using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneManager : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("MainGameScene");
        SceneManager.LoadScene("01TroposphereScene", LoadSceneMode.Additive);
    }
}
