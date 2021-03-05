using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Vai guardar os stats do player, como se ele está carregando alguma coisa já, ou vida e outros

    #region Singleton

    private static PlayerManager _instance;

    public static PlayerManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Player Manager not found.");

                return null;
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance != null)
        {
            Debug.LogError("Player Manager already exist.");
            return;
        }

        Debug.Log("Player Manager created.");
        _instance = this;
    }

    #endregion

    public bool carrying { get; private set;}

    public int interactingWithId { get; private set; }

    public float actualSpeed { get; private set; }
    public float actualTurnSpeed { get; private set; }
    float normalSpeed = 10f;
    float normalTurnSpeed = 15f;
    float carryingSpeed = 6f;
    float carryingTurnSpeed = 10f;



    void Start()
    {
        carrying = false;

        GameEventsManager.Instance.OnCarryingSomething += ChangeCarryingStat; //adiciona o método que altera o estado de "carregando" do player qdo o mesmo pega um objeto, evento chamado na classe object

        actualSpeed = normalSpeed;
        actualTurnSpeed = normalTurnSpeed;

    }
    
    public void ChangeCarryingStat(int id)                // Altera o status de carregando do player e a velocidade do player quando está carregando alguma coisa
    {                                                                 // E para um melhor controle o id do objeto que está interagindo no momento é passado no event

        carrying = !carrying;


        if (carrying)
        {
            actualSpeed = carryingSpeed;
            actualTurnSpeed = carryingTurnSpeed;
            interactingWithId = id;
        }
        else if (!carrying)
        {
            actualSpeed = normalSpeed;
            actualTurnSpeed = normalTurnSpeed;
            interactingWithId = 0;
        }
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.OnCarryingSomething -= ChangeCarryingStat;
    }

}
