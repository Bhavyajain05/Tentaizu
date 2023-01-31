using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class bgBlock
    {
        public GameObject blockObject;
        public bool isEmpty = true;
        public float numberOfStarsNearby;
        public TextMeshProUGUI text;
        public bool isMarked;
    }
    public Transform parentObject;

    [SerializeField] GameObject bgBlockPrefab;
    [SerializeField] GameObject starBlockPrefab;

    public List<bgBlock> bgBlockList = new();
    public List<GameObject> starBlockList = new();

    public Vector2Int gridSize;
    [SerializeField] float starCount;

    Vector2 spwanPosi;
    public int gridLenth;

    private void Awake()
    {
        gridLenth = (gridSize.x * gridSize.y) -1;

        //Spwaning the Grid
        for (int y = 0; y < gridSize.x; y++)
        {
            for (int x = 0; x < gridSize.y; x++)
            {
                spwanPosi = new Vector2(x, y);
                bgBlockList.Add(new bgBlock { blockObject = Instantiate(bgBlockPrefab, spwanPosi, Quaternion.identity, parentObject) });
            }
        }

        //Spwaning Stars
        for (int i = 0; i < starCount; i++)
        {
            int randNum = Random.Range(0, bgBlockList.Count);

            //checking if the selected index have star already spwaned or not
            if (bgBlockList[randNum].isEmpty)
            {
                //if yes then spwan
                starBlockList.Add(Instantiate(starBlockPrefab, bgBlockList[randNum].blockObject.transform.position, Quaternion.identity, parentObject));
                bgBlockList[randNum].isEmpty = false;

                //Setting numbers to the blocks Nearby Stars

                if (randNum - 1 >= 0 && randNum - 1 <= bgBlockList.Count - 1 && (randNum) % 7 != 0)//left
                {
                    if (bgBlockList[randNum - 1].isEmpty)
                    {
                        bgBlockList[randNum - 1].numberOfStarsNearby++;
                    }
                }


                if (randNum + 1 >= 0 && randNum + 1 <= bgBlockList.Count - 1 && (randNum + 1) % 7 != 0)//right
                {
                    if (bgBlockList[randNum + 1].isEmpty)
                    {
                        bgBlockList[randNum + 1].numberOfStarsNearby++;
                    }
                }


                if (randNum + gridSize.x >= 0 && randNum + gridSize.x <= bgBlockList.Count - 1 && (randNum + gridSize.x) % 7 != 0)//top
                {
                    if (bgBlockList[randNum + gridSize.x].isEmpty)
                    {
                        bgBlockList[randNum + gridSize.x].numberOfStarsNearby++;
                    }
                }


                if (randNum - gridSize.x >= 0 && randNum - gridSize.x <= bgBlockList.Count - 1 && (randNum - gridSize.x) % 7 != 0)//bottom
                {
                    if (bgBlockList[randNum - gridSize.x].isEmpty)
                    {
                        bgBlockList[randNum - gridSize.x].numberOfStarsNearby++;
                    }
                }


                if ((randNum + gridSize.x - 1) >= 0 && (randNum + gridSize.x - 1) <= bgBlockList.Count - 1 && (randNum + gridSize.x) % 7 != 0)//top left
                {
                    if (bgBlockList[randNum + gridSize.x - 1].isEmpty)
                    {
                        bgBlockList[randNum + gridSize.x - 1].numberOfStarsNearby++;
                    }
                }


                if ((randNum + gridSize.x + 1) >= 0 && (randNum + gridSize.x + 1) <= bgBlockList.Count - 1 && (randNum + gridSize.x + 1) % 7 != 0)//top right
                {
                    if (bgBlockList[randNum + gridSize.x + 1].isEmpty)
                    {
                        bgBlockList[randNum + gridSize.x + 1].numberOfStarsNearby++;
                    }
                }


                if ((randNum - gridSize.x - 1) >= 0 && (randNum - gridSize.x - 1) <= bgBlockList.Count - 1 && (randNum - gridSize.x) % 7 != 0)//bottom left
                {
                    if (bgBlockList[randNum - gridSize.x - 1].isEmpty)
                    {
                        bgBlockList[randNum - gridSize.x - 1].numberOfStarsNearby++;
                    }
                }


                if ((randNum - gridSize.x + 1) >= 0 && (randNum - gridSize.x + 1) <= bgBlockList.Count - 1 && (randNum - gridSize.x + 1) % 7 != 0)//bottom right
                {
                    if (bgBlockList[randNum - gridSize.x + 1].isEmpty)
                    {
                        bgBlockList[randNum - gridSize.x + 1].numberOfStarsNearby++;
                    }
                }
            }
            //if no iterate again
            else
            {
                i--;
            }
        }

        //Setting all stars to false for the puzzle
        for (int i = 0; i < starBlockList.Count; i++)
        {
            starBlockList[i].SetActive(false);
        }

        //Setting Text value for the numbers
        for (int i = 0; i < bgBlockList.Count; i++)
        {
            //if has a star nearby set the values and disabling the text
            if (bgBlockList[i].numberOfStarsNearby != 0)
            {
                bgBlockList[i].text = bgBlockList[i].blockObject.GetComponentInChildren<TextMeshProUGUI>();
                bgBlockList[i].text.text = bgBlockList[i].numberOfStarsNearby.ToString();
                bgBlockList[i].text.gameObject.SetActive(false);
            }
            else
            {
                bgBlockList[i].text = bgBlockList[i].blockObject.GetComponentInChildren<TextMeshProUGUI>();
                bgBlockList[i].text.gameObject.SetActive(false);
            }
        }


        //Randomlly Selecting 15 numbers to display
        for (int i = 0; i < 15; i++)
        {
            int randNum = Random.Range(0, bgBlockList.Count);
            //checking if the selected index is empty and has a star nearby
            if (bgBlockList[randNum].numberOfStarsNearby != 0 && bgBlockList[randNum].isEmpty)
            {
                //cheking if we already activated the same text before
                if (!bgBlockList[randNum].text.gameObject.activeInHierarchy)
                {
                    bgBlockList[randNum].text.gameObject.SetActive(true);
                }
                else
                {
                    i--;
                }
            }
            else
            {
                i--;
            }
        }
    }
}
