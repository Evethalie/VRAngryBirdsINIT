using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scoring")]
    [SerializeField] private int pointsPerPig = 5000;
    [SerializeField] private int pointsPerBlock = 500;
    [SerializeField] private int pointsPerUnusedBird = 10000;

    [Header("Losing")]
    [SerializeField] private float loseDelay = 6.0f;

    [Header("UI")]
    [SerializeField] private TMP_Text pigsLabel;
    [SerializeField] private TMP_Text birdsLabel;
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private TMP_Text resultTitle;
    [SerializeField] private TMP_Text resultScore;
    [SerializeField] private GameObject resultPanel;
}
