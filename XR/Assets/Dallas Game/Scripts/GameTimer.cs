using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private int timer;
    private int maxTimer;
    [SerializeField] private Text timerText;
    [SerializeField] private Image image;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = $"{timer / 60}:{timer % 60}";
        maxTimer = timer;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
            timerText.text = $"{timer / 60}:{timer % 60}";
            image.fillAmount = Mathf.InverseLerp(0, maxTimer, timer);
        }
        SceneManager.LoadScene(3);
        //Do something
    }
}
