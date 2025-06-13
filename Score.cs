using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;

    private PlayerBehaviour player;

    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    void Update()
    {
        if (player != null)
        {
            scoreText.text = "Score: " + player.GetScore();
        }
    }
}
