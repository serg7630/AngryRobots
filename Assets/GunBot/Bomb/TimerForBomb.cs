using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerForBomb : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    [SerializeField] private float time;
    [SerializeField] private TMPro.TextMeshProUGUI timerText;

    private float _timeLeft = 0f;

    public bool ActiveCorutine=false;
    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            var normalizedValue = Mathf.Clamp(_timeLeft / time, 0.0f, 1.0f);
            timerImage.fillAmount = normalizedValue;
            UpdateTimeText();
            yield return null;
        }
    }

    private void Start()
    {
        _timeLeft = time;
        //StartCoroutine(StartTimer());
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0) {
            _timeLeft = 0;
            GetComponent<Explorer2D>().Explosion2D(this.transform.position);
            GetComponent<BombExplorer>().ExplosionBomb();
            //Debug.LogError("exposivle");
        }
           

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00}", seconds);
        //timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
    public void StartCorutineExplosion()
    {
        StartCoroutine(StartTimer());
    }
}
