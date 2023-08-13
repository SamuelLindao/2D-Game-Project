using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MenuUI : MonoBehaviour
{
    public TMP_InputField field;

    public void PlayerButton()
    {
        PlayerInfo.instance.PlayerName = field.text;
        StartCoroutine(LoadAsync());
        
    }
    public IEnumerator LoadAsync()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        while(!async.isDone)
        {
            print(async.progress);

            yield return null;            
        }
    }

    public void SamuelScreen()
    {
        Screen.SetResolution(1366, 768, false);
    }
}
