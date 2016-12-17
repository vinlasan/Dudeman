using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(PlayerController))]
public class InputManager : MonoBehaviour {
    public bool blnJump, blnJumpFloat;
    float fltJumpPressTime, fltJumpCount;
    int playeriD;

    public enum Direction { Left, Right, Up, Down}
    public Direction throwDirection;
    public Vector3 direction, input;
    PlayerController controller;

    Rewired.Player playerCon;

    void Start()
    {
        playeriD = 0;
        playerCon = ReInput.players.GetPlayer(playeriD);
        controller = GetComponent<PlayerController>();
    }

    void Update() //Check for input here
    {
        input = new Vector3(playerCon.GetAxisRaw("Move Horizontal"), playerCon.GetAxisRaw("Move Vertical"), controller.fltZPlane);
        controller.velocity = input.normalized * controller.fltMoveSpeed;


        if(playerCon.GetAxisRaw("Move Horizontal") > 0)
        {
            throwDirection = Direction.Right;
        }
        if (playerCon.GetAxisRaw("Move Horizontal") < 0)
        {
            throwDirection = Direction.Left;
        }
        if (playerCon.GetButtonDown("Jump"))
        {
            blnJump = true;
        }
        if(playerCon.GetButtonLongPress("Jump"))
        {
            blnJumpFloat = true;
        }
    }

}
