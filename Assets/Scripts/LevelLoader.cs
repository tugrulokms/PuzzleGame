using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Draggable.score == 9)
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else if(SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }

    }
}
