using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    GameManager gm;

    public int positionIndex;

    public int numberOfBlocksMarked;

    public int score;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI numberOfBlocksMarkedText;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        scoreText.gameObject.SetActive(false);
    }

    private void Update()
    {
        //getting player input and moving acordingly

        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (positionIndex - 1 >= 0)
            {
                positionIndex -= 1;
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (positionIndex + 1 <= gm.gridLenth)
            {
                positionIndex += 1;
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (positionIndex + gm.gridSize.x <= gm.gridLenth) 
            { 
                positionIndex += gm.gridSize.x;
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (positionIndex - gm.gridSize.x >= 0)
            {
                positionIndex -= gm.gridSize.x;
            }
        }

        //setting the position 
        transform.position = gm.bgBlockList[positionIndex].blockObject.transform.position;

        
        if (Input.GetKeyDown(KeyCode.F) && numberOfBlocksMarked < 10)
        {
            MarkBlock();
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            DeleteMarkedBlock();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            RestartLvel();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CheckScore();
        }

        numberOfBlocksMarkedText.text = "Number of Blocks Marked: " + numberOfBlocksMarked;

    }

    void MarkBlock()
    {
        if (!gm.bgBlockList[positionIndex].isMarked)
        {
            gm.bgBlockList[positionIndex].blockObject.GetComponent<Image>().color = new Color(0, 0.6f, 0.6f);
            gm.bgBlockList[positionIndex].isMarked = true;
            numberOfBlocksMarked++;
        }
    }

    void DeleteMarkedBlock()
    {
        if (gm.bgBlockList[positionIndex].isMarked)
        {
            gm.bgBlockList[positionIndex].blockObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            gm.bgBlockList[positionIndex].isMarked = false;
            numberOfBlocksMarked--; 
        }
    }

    void ResetLevel()
    {
        for (int i = 0; i < gm.bgBlockList.Count; i++)
        {
            gm.bgBlockList[i].blockObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            gm.bgBlockList[i].isMarked = false;
            numberOfBlocksMarked = 0;
        }
        positionIndex = 0;
        score = 0;
        scoreText.gameObject.SetActive(false);

        for (int i = 0; i < gm.starBlockList.Count; i++)
        {
            gm.starBlockList[i].SetActive(false);
        }
    }

    void RestartLvel()
    {
        SceneManager.LoadScene(0);
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void CheckScore()
    {
        for (int i = 0; i < gm.bgBlockList.Count; i++)
        {
            if (gm.bgBlockList[i].isMarked && !gm.bgBlockList[i].isEmpty)
            {
                score++;
            }
        }

        for (int i = 0; i < gm.starBlockList.Count; i++)
        {
            gm.starBlockList[i].SetActive(true);
        }

        scoreText.gameObject.SetActive(true);
        scoreText.text= "Score: " + score;
    }
}
