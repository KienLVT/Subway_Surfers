using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public TextMesh scoreText;
    [SerializeField] private Transform player;

    private float score;
    [SerializeField] private Vector3 startPos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startPos = player.position;
    }
    private void Update()
    {
        score = Vector3.Distance(startPos, player.position);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score:" + Mathf.FloorToInt(score).ToString();
    }
    public void Reset()
    {
        score = 0;
        startPos = player.position;
    }
}
