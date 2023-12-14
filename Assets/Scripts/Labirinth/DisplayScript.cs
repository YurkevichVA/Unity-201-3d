using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clock;
    private float gameTime;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;

    [SerializeField] private TextMeshProUGUI checkPointsPassed;

    [SerializeField] private Image image1; // for checkpoint1 indicator
    [SerializeField] private Image image2; // for checkpoint2 indicator

    void Start()
    {
        LabirinthState.AddPropertyListener(nameof(LabirinthState.checkPoint1Amount), OnCheckPoint1AmountChanged);
        LabirinthState.AddPropertyListener(nameof(LabirinthState.checkPoint2Amount), OnCheckPoint2AmountChanged);
        LabirinthState.AddPropertyListener(nameof(LabirinthState.checkPoint1Passed), OnCheckPointPassed);
        LabirinthState.AddPropertyListener(nameof(LabirinthState.checkPoint2Passed), OnCheckPointPassed);
        gameTime = 0f;
        score = 100;
        StartCoroutine("ScoreReduce");
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    private void LateUpdate()
    {
        int time = (int)gameTime;
        int hour = time / 3600;
        int minute = (time % 3600) / 60;
        int second = time % 60;
        int decisecond = (int)((gameTime - time) * 10);
        clock.text = $"{hour:00}:{minute:00}:{second:00}.{decisecond:0}";
    }

    private IEnumerator ScoreReduce()
    {
        while (score > 0)
        {
            yield return new WaitForSeconds(5);
            score -= 1;
            scoreText.text = score.ToString();
        }
    }

    private void OnLabirinthStateChanged(string propertyName)
    {
        if(propertyName == nameof(LabirinthState.checkPoint1Amount))
        {
            image1.fillAmount = LabirinthState.checkPoint1Amount;
        }
    }
    private void OnCheckPoint1AmountChanged()
    {
        image1.fillAmount = LabirinthState.checkPoint1Amount;
    }
    private void OnCheckPoint2AmountChanged()
    {
        image2.fillAmount = LabirinthState.checkPoint2Amount;
    }
    private void OnCheckPointPassed()
    {
        if (LabirinthState.checkPoint2Passed)
        {
            checkPointsPassed.text = "2";
        }
        else if (LabirinthState.checkPoint1Passed)
        {
            checkPointsPassed.text = "1";
        }
    }
    private void OnDestroy()
    {
        LabirinthState.RemoveNotifyListener(OnLabirinthStateChanged);
        LabirinthState.RemovePropertyListener(nameof(LabirinthState.checkPoint1Amount), OnCheckPoint1AmountChanged);
        LabirinthState.RemovePropertyListener(nameof(LabirinthState.checkPoint2Amount), OnCheckPoint2AmountChanged);

        LabirinthState.RemovePropertyListener(nameof(LabirinthState.checkPoint1Passed), OnCheckPointPassed);
        LabirinthState.RemovePropertyListener(nameof(LabirinthState.checkPoint2Passed), OnCheckPointPassed);
    }
}
