using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private LoadSceneMode sceneMode;

    /// <summary>
    /// Load a scene asynchronously
    /// </summary>
    public void LoadScene()
    {
        StartCoroutine(AsyncLoadScene(sceneIndex));

        IEnumerator AsyncLoadScene(int scene)
        {
            var request = SceneManager.LoadSceneAsync(scene, sceneMode);
            yield return request;

            Scene newScene = SceneManager.GetSceneByBuildIndex(scene);
            Scene oldScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(newScene);
            SceneManager.UnloadSceneAsync(oldScene);
        }
    }

    /// <summary>
    /// Load the scene in the background
    /// </summary>
    public void LoadSceneAsync()
    {
        SceneManager.LoadSceneAsync(sceneIndex, sceneMode);
    }


    /// <summary>
    /// Finish loading the scene in the background.
    /// Should only be used when the load scene mode is additive.
    /// </summary>
    public void FinishLoad()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
        SceneManager.UnloadSceneAsync(activeScene);
    }
}
