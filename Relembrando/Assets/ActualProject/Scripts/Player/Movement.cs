using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : IAction
{
    Rigidbody player;

    public float horizontalInput;
    public float verticalInput;

    Vector3 actualPosition;

    //camangle

    float speed = 10f;
    float turnSpeed = 8f;

    float rewindSpeed;
    float rewindTurnSpeed ;

    Vector3 direction;


    public Movement(Rigidbody player, float hInput, float vInput)               //A movimentação vai ser salva em uma Lista no replay manager, aqui é o construtor que recebe da instanciação na classe player 
    {
        this.player = player;
        this.horizontalInput = hInput;
        this.verticalInput = vInput;

        actualPosition = player.transform.position;        

        direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        speed = PlayerManager.Instance.actualSpeed;                                             //Como cada parte do movimento é feito com a instanciação de um objeto de movement a velocidade pode ser pega a cada instanciação 
        turnSpeed = PlayerManager.Instance.actualTurnSpeed;                                   //no Player manager, já que essa vai se alterar quando o player estiver carregando algo ou não


        rewindSpeed = speed * 0.8f;
        rewindTurnSpeed = turnSpeed * 0.8f;

    }




    public void Execute()
    {
        if(direction.magnitude >= 0.1)
        {
            Move(direction.x, direction.z, speed);
            Turn(direction.x, direction.z, turnSpeed);

        }

    }



    public void Undo()
    {
        if(direction.magnitude >= 0.1)
        {
            Move(-direction.x, -direction.z, rewindSpeed);
            Turn(direction.x, direction.z, rewindTurnSpeed);

        }

    }



    private void Move(float directionX, float directionZ, float actualSpeed)
    {
        float dirangle = Mathf.Atan2(directionX, directionZ) * Mathf.Rad2Deg;

        Vector3 moveDirection = Quaternion.Euler(Vector3.up * dirangle) * Vector3.forward;
        Vector3 velocity = moveDirection * speed;
        Vector3 moveAmount = velocity * Time.deltaTime;

        player.MovePosition(actualPosition + moveAmount);

    }

    private void Turn(float directionX, float directionZ, float actualTurnSpeed)
    {
        float rotDir = Mathf.Atan2(directionX, directionZ) * Mathf.Rad2Deg;
        float smoothRotDir = Mathf.LerpAngle(player.transform.eulerAngles.y, rotDir, actualTurnSpeed * Time.deltaTime);

        player.MoveRotation(Quaternion.Euler(Vector3.up * smoothRotDir));

    }





}
