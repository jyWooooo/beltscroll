using System.Collections;

public class SceneManager
{
    private SceneBase _currentScene;

    public void ChangeScene(string sceneName)
    {
        _currentScene.UnloadScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void SetScene(SceneBase scene)
    {
        if (scene == null) return;

        _currentScene = scene;
        GameManager.Instance.StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        while (!GameManager.DataManager.IsDone)
            yield return null;

        _currentScene.LoadScene();
    }
}