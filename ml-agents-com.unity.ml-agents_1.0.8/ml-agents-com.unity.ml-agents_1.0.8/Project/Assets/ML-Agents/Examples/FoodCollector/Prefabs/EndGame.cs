using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    private FoodCollectorSettings m_FoodCollecterSettings;
    private Controller m_pPlayer;
    public Image panel;
    public Text winer;

    // Start is called before the first frame update
    void Start()
    {
        m_FoodCollecterSettings = FindObjectOfType<FoodCollectorSettings>();
        m_pPlayer = FindObjectOfType<Controller>();
        panel.gameObject.SetActive(false);
    }


    int GameStateCheck()
    {
        if (!m_FoodCollecterSettings)
            return 0;

        Debug.Log($"Any Score: {m_pPlayer.TotalScore} Player Score: {m_pPlayer.Score}");

        if (m_pPlayer.TotalScore <= 0 && m_FoodCollecterSettings.totalScore < m_pPlayer.Score)
            return 3;

        if (m_pPlayer.TotalScore <= 0 && m_FoodCollecterSettings.totalScore > m_pPlayer.Score)
            return 1;

        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateCheck() != 0)
        {
            if (GameStateCheck() > 2)
            {
                panel.gameObject.SetActive(true);
                winer.text = "THANKS FOR PLAYING! \r YOU WIN!";
                return;
            }

            panel.gameObject.SetActive(true);
            winer.text = "THANKS FOR PLAYING! \r YOU LOSE!";
        }
    }
}
