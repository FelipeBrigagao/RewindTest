using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReplayManager : MonoBehaviour
{

    #region Singleton

    private static ReplayManager _instance;

    public static ReplayManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Replay Manager não encontrado.");
                return null;
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("Trying to make another instance of ReplayManager.");
            return;
        }

        Debug.Log("Replay manager criado");
        _instance = this;
    }

    #endregion 

    List<IAction> movements = new List<IAction>();              //Lista de ações realizadas pelo player, de movimentação ou de interação com objetos

    public bool record { get; private set; }

    private void Start()
    {
        record = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!record)
            {
                ClearMovements();
                Debug.Log("Recording.");

                record = true;

            }
            else if (record)
            {

                Debug.Log("Stop Recording.");
                record = false;

            }

        }

        if (!record && Input.GetKeyDown(KeyCode.P))
        {
            Replay();
        }

        if (!record && Input.GetKeyDown(KeyCode.U))
        {
            Rewind();
        }

    }

    public void AddMovement(IAction movement)
    {
        movements.Add(movement);
    
    }


    public void ClearMovements()
    {
        movements.Clear();
    }



    public void Replay()
    {
        Debug.Log("Replay");

        if (movements == null)
        {
            Debug.LogError("No movements saved.");
            return;
        }

        StartCoroutine(ReplayRoutine());
        
    }

    IEnumerator ReplayRoutine()
    {
        foreach (IAction mov in movements)
        {
            mov.Execute();
            yield return new WaitForFixedUpdate();
        }
    }

    public void Rewind()
    {
        Debug.Log("Rewinding");

        if(movements == null)
        {
            Debug.LogError("No movements saved.");

        }

        StartCoroutine(RewindRoutine());

    }

    IEnumerator RewindRoutine()
    {
        foreach (IAction mov in Enumerable.Reverse(movements))
        {
            mov.Undo();
            yield return new WaitForFixedUpdate();
        }
    }








}
