// Load an assetbundle which contains Scenes.
// When the user clicks a button the first Scene in the assetbundle is
// loaded and replaces the current Scene.

using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour 
{

    public void FetchApiScene()
    {
        SceneManager.LoadScene("FetchApiScene");
    }
    public void LoadScene()
    {
         SceneManager.LoadScene("LoadWeatherInfo");
    }
    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}