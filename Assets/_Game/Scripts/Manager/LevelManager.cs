using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;


public enum GridValueType
{
    Brick = 1,
    UnBrick = 2,
    PlayStart = 3,
    Finish = 4,
    Wall = 5,

}
public class LevelManager : Singleton<LevelManager>
{

    public List<GameObject> brickPrefabs = new();
    public List<TextAsset> listLevel = new();

    private int currentLevelIndex = 0;

    private TextAsset currentLevelText;

    public Player player;


    // Start is called before the first frame update
    void Start()
    {
        LoadLevel(currentLevelIndex);
        OnInit();
        UIManager.Instance.OpenMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckLevelCompletionCondition())
        {
            LoadNextLevel();
        }
    }

    public GameObject GetPrefabs(int gridValue)
    {
        int indexInList = gridValue - 1;

        if(indexInList >= 0 && indexInList < brickPrefabs.Count)
        {
            return brickPrefabs[indexInList];
        }
        return null;
    }

    public Vector3 GridToVector3(int row, int col)
    {
        return new Vector3(col, 0, row);
    }

    public void CreateMap()
    {
        
        if (currentLevelText == null)
        {
            return;
        }

        string[] lines = currentLevelText.text.Split('\n');
       
        for (int row = 0; row < lines.Length; row++)
        {
            string line = lines[row].Trim();
          

            for (int col = 0; col < line.Length; col++)
            {
                char cellChar = line[col];
                int gridValue = int.Parse(cellChar.ToString());

                // Convert grid coordinates to world coordinates
                Vector3 position = GridToVector3(row, col);

                // Instantiate the corresponding prefab based on gridValue
                GameObject prefab = GetPrefabs(gridValue);

                if (prefab != null)
                {
                    Instantiate(prefab, position, Quaternion.identity);
                }
            }
        }
    }

    bool CheckLevelCompletionCondition()
    {
        return false;
    }

    public void LoadLevel()
    {
        LoadLevel(currentLevelIndex);
    }


    public void LoadLevel(int levelIndex)
    {
        //player.transform.position = Vector3.zero;
        if (levelIndex >= 0 && levelIndex < listLevel.Count)
        {
            currentLevelIndex = levelIndex;
            currentLevelText = listLevel[levelIndex];
            CreateMap();
        }
        else
        {
            Debug.Log("Invalid level index.");
        }
    }


    public void LoadNextLevel()
    {
        int nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex < listLevel.Count)
        {
            LoadLevel(nextLevelIndex);
        }
        else
        {
            Debug.Log("No more levels to load.");
        }
    }

    public void RetryLevel()
    {
      
        LoadLevel(currentLevelIndex);
    }


    public void OnInit()
    {
        //player.transform.position = Vector3.zero;
        player.OnInit();
    }

    public void OnStart()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }

    public void OnFinish()
    {
        UIManager.Instance.OpenFinish();
        GameManager.Instance.ChangeState(GameState.Finish);

    }


}
