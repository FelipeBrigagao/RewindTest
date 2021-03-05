using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Game manager not found.");
                return null;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance != null)
        {
            Debug.LogError("Game manager already exist.");
            return;
        }


        Debug.Log("Game Manager created.");
        _instance = this;
    }

    #endregion

    public GameObject box;
    public GameObject brokenBox;


}
