using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAsync : MonoBehaviour
{
    [SerializeField] private int sceneIndex;

    // Start is called before the first frame update
    private void Start()
    {
        LoadScene(sceneIndex);
    }

    /// <summary>
    /// Load a scene asynchronously
    /// </summary>
    /// <param name="sceneBuildIndex">The scene index you want to load</param>
    private void LoadScene(int sceneBuildIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneBuildIndex));

        IEnumerator LoadSceneAsync(int scene)
        {
            var request = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            yield return request;

            Scene newScene = SceneManager.GetSceneByBuildIndex(scene);
            Scene oldScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(newScene);
            SceneManager.UnloadSceneAsync(oldScene);
        }
    }
}
