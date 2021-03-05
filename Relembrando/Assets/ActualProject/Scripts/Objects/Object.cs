using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    //vai ter que observar onde está o player e cria uma instancia da classe que realiza a ação de interação com o player
    //No caso vai ser pegar a caixa, então essa interação vai ter que mandar o rigidbody do player e do objeto atual, para fazer o fixed joint entre eles, melhor mandar o game object dos dois

    // faz a observação e entrada aqui e manda os dados para o pushable

    [SerializeField]
    private ObjectsDataSO objData;

    public bool interacting { get; private set; }

    public int id;

    [SerializeField]
    GameObject player;

    Pickable pick;


    private delegate void DoingAction();
    private DoingAction TheAction;


    private void Start()
    {
        interacting = false;

        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player não encontrado.");
        }

        GameEventsManager.Instance.OnCarryingSomething += ChangeInteractionStat;  //adiciona ao event de carregar alguma coisa o método que altera o estado de interagindo do objeto

    }


    protected virtual void Update()
    {
        CheckPlayerDistance();
        CheckForAction();

    }


    private void CheckPlayerDistance()
    {
        if((!PlayerManager.Instance.carrying && !this.interacting) || (PlayerManager.Instance.carrying && this.interacting && (PlayerManager.Instance.interactingWithId == this.id) ))  //Verifica se o player e o objeto atual estão 
        {                                                                                                                                                                                                                                        //interagindo, observando o interaction e o id
            
            
            if (player != null)
            {
                Vector3 playerDirection;

                RaycastHit hitInfo;

                playerDirection = ((player.transform.position - transform.position).normalized);

                if (Physics.Raycast(transform.position, playerDirection, out hitInfo, objData.interactionRadius) && Input.GetKeyDown(KeyCode.E))
                {

                    //Debug.Log("Interagindo com objeto de id: " + id  + "\nInteração do objeto: " + this.interacting + "\nInteração do player: " + PlayerManager.Instance.carrying );

                    if (hitInfo.collider.CompareTag("Player"))
                    {
                        PickUpInteraction();
                    }
                }
            }

        }


    }


    private void PickUpInteraction()
    {
        pick = new Pickable(player, this.gameObject);
        pick.Execute();

        
        if (ReplayManager.Instance.record)
        {
            ReplayManager.Instance.AddMovement(pick);

        }
    }

    //O make Action cria uma instância de pushable, que é responsável pela interação do objeto atual com o player, essa instância pode ser salva no replay manager para ser rodada no rewind
    //O pushable é uma classe que herda de IAction, que tmb é classe pai da classe Movement (responsável pelos movimentos do player)


    public void ChangeInteractionStat(int receivedId)  //Se o Id passado pela instância criada por MakeAction for igual o id do objeto atual o status de interação é alterado, isso porque esse método é chamado no event do 
    {                                                                  //GameEventManager e como todos os objetos terão essa classe todos os metodos são chamados, e esses estão inscritos no event, uma id de identificação da interação é necessária
        if (receivedId == id)
        {
            interacting = !interacting;

        }

    }


    private void CheckForAction()
    {
        if(interacting && Input.GetKeyDown(KeyCode.Q))
        {
            switch(objData.objectype)
            {
                case ObjectsDataSO.ObjectType.Breakable:
                    TheAction = BreakObject;
                    TheAction();
                    break;
                case ObjectsDataSO.ObjectType.Trowable:
                    TheAction = ThrowObject;
                    TheAction();
                    break;
                case ObjectsDataSO.ObjectType.Normal:
                    TheAction = NormalObject;
                    TheAction();
                    break;
                case ObjectsDataSO.ObjectType.Usable:
                    TheAction = UseObject;
                    TheAction();
                    break;
                default:
                    Debug.LogWarning("no object type");
                    break;
            }
        }
    }


    private void ThrowObject()
    {
        pick = new Pickable(player, this.gameObject);
        pick.Execute();

        GetComponent<Rigidbody>().AddForce((new Vector3 (0, 0.5f,0) + player.transform.forward) * 5f, ForceMode.Impulse);

        Debug.Log("Throwing.");
    }

    private void BreakObject()
    {
        pick = new Pickable(player, this.gameObject);
        pick.Execute();

        Destroy(this.gameObject);

        Instantiate(GameManager.Instance.brokenBox, transform.position, transform.rotation);

        Debug.Log("Breaking.");
    }

    private void UseObject()
    {
        Debug.Log("Using.");
    }

    private void NormalObject()
    {
        Debug.Log("Just Carrying.");
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, objData.interactionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, ((player.transform.position - transform.position).normalized) * objData.interactionRadius);

    }

    private void OnDisable()
    {
        Debug.Log("Disabling obj:" + id);
         GameEventsManager.Instance.OnCarryingSomething -= ChangeInteractionStat;
    }


}
