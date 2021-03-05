using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : IAction
{

    GameObject player;

    GameObject objeto;

    public Pickable(GameObject player, GameObject objeto)
    {
        this.player = player;
        this.objeto = objeto;
    }


    private void Interact(bool actualInteract)
    {

        //Mesma mecanica mas com uso de fixed joint
        #region FixedJoint 
        /*
        Debug.Log("Pushable interact");

        FixedJoint pushBox;  //Quando interagir procura um FixedJoint, se não tiver é porque não está interagindo ainda

        if (!objeto.TryGetComponent<FixedJoint>(out pushBox))       //Se não tiver um FixedJoint e está aqui é porque vai começar a interagir, então cria um fixed joint para pegar a caixa
        {
            pushBox = objeto.AddComponent<FixedJoint>();

        }

        if (actualInteract)                                 // Se já está interagindo, para de interagir e tira o fixed joint pra caixa n ficar presa no ar
        {
            Debug.Log("Droping object");

            pushBox.connectedBody = null;

            objeto.GetComponent<BoxCollider>().isTrigger = false;

            pushBox.breakForce = 0;

        }else if (!actualInteract)                                         //Se não está interagindo ainda um FixedJoint foi criado e vai ser ligado a um rigidbody
        {
            Debug.Log("Picking up object");

            objeto.transform.position = player.GetComponent<Player>().pickUpPoint.position;           //Coloca a caixa na posição correta para ser carregada

            objeto.GetComponent<BoxCollider>().isTrigger = true;

            pushBox.connectedBody = player.GetComponent<Rigidbody>();

        }
        
        */
        #endregion          


        if (actualInteract)
        {
            Debug.Log("Droping object.");

            if(objeto.transform.parent != null)
            {

                objeto.GetComponent<Rigidbody>().isKinematic = false;

                objeto.GetComponent<BoxCollider>().isTrigger = false;

                objeto.transform.parent = null;
            }

        }else if(!actualInteract)
        {
            Debug.Log("Picking up object.");

            if (objeto.transform.parent == null)
            {
                objeto.transform.position = player.GetComponent<Player>().pickUpPoint.position;

                objeto.GetComponent<Rigidbody>().isKinematic = true;

                objeto.GetComponent<BoxCollider>().isTrigger = true;

                objeto.transform.parent = player.transform;
            }

        }

        GameEventsManager.Instance.CarryingSomething(objeto.GetComponent<Object>().id);  //depois que uma interação acontece, mesmo no rewind, os status de interação tanto do player qto do objeto mudam

    }

    

    public void Execute()
    {
        Interact(objeto.GetComponent<Object>().interacting);

    }

    public void Undo()
    {
        Interact(objeto.GetComponent<Object>().interacting);
    
    }
    
}

