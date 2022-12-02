using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] PlayerController player;
    [SerializeField] Hole hole;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hole.Entered && gameOverPanel.activeInHierarchy == false) {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Shoot Count : "+ player.ShootCount;
        }
    }
}
