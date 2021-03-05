using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventsManager : MonoBehaviour
{
    //Todos os eventos ficarão aqui, onde tmb terá um método para chama-lo, dessa maneira qualquer classe pode chamar o evento qdo algum trigger acontecer

    #region Singleton

    private static GameEventsManager _instance;

    public static GameEventsManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Game Event Manager não encontrado.");
                return null;
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance != null)
        {
            Debug.LogError("Game Event Manager already exist.");
            return;
        }

        Debug.Log("Game Event Manager created.");
        _instance = this;
    }




    #endregion


    public event Action<int> OnCarryingSomething;
    public void CarryingSomething(int id)                 //Evento para quando o player pega um objeto do chão e o carrega
    {
        OnCarryingSomething?.Invoke(id);
    }



}
