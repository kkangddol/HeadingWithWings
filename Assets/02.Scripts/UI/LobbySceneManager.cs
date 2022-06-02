using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneManager : MonoBehaviour
{
    public GameObject collectionPrefab;
    public GameObject optionPrefab;

    public void OnClickStartButton()
    {
        //SceneManager.LoadSceneAsync("MainGameScene");
        LoadingSceneController.Instance.LoadScene("01TroposphereScene");
        //SceneManager.LoadScene("01TroposphereScene", LoadSceneMode.Additive);
    }

    public void OnClickCollectionButton()
    {
        if(collectionPrefab == null)
        {
            Debug.Log("CollectionPrefab�� �����ϴ�!");
            return;
        }
        Instantiate(collectionPrefab);
    }
    public void OnClickOptionButton()
    {
        if (optionPrefab == null)
        {
            Debug.Log("optionPrefab�� �����ϴ�!");
            return;
        }
        Instantiate(optionPrefab);
    }
}
