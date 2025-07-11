using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] private int maxScore = 9999;
    [SerializeField] private int comboTime = 1;
    private int comboMultiplier = 0;
    private float comboDelta = 0;
    [SerializeField] private TextMeshProUGUI scoreField;
    [SerializeField] private TextMeshProUGUI comboField;
    [SerializeField] private Slider comboBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (comboMultiplier > 0)
        {
            comboDelta -= Time.deltaTime;
        }
        UpdateComboLabel();
        UpdateComboBar();
    }

    private void UpdateComboLabel()
    {
        comboField.text = comboMultiplier.ToString();
    }

    private void HitCombo()
    {
        comboDelta = comboTime;
        comboBar.value = 1;
        comboMultiplier++;
        comboBar.enabled = true;
    }

    public void EndCombo()
    {
        comboDelta = 0;
        comboMultiplier = 0;
        comboBar.value = 0;
        comboBar.enabled = false;
    }

    public void ScoreHit(int targetScore)
    {
        if (score > maxScore)
        {
            score = maxScore;
        }
        else
        {
            Debug.Log("Updating score" + targetScore.ToString());
            HitCombo();
            score += targetScore * comboMultiplier;
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        scoreField.text = score.ToString();
    }

    private void UpdateComboBar()
    {
        if (comboDelta > 0)
        {
            comboBar.value = comboDelta / comboTime;
            comboBar.enabled = true;
        }
        else
        {
            comboBar.value = 0;
            comboBar.enabled = false;
        }
    }
}
