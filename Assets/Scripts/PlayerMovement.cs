using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController controller;
    public float movementSpeed = 10f;

    private SpawnScript spawnScript;

    public void Initialize(SpawnScript spawnScript)
    {
        this.spawnScript = spawnScript;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowPowerUp();
        }
    }

    void ThrowPowerUp()
    {
        Vector3 powerUpPosition = transform.position + transform.forward * 1.5f;
        spawnScript.SpawnPowerUp(powerUpPosition, gameObject);
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical) * movementSpeed * Runner.DeltaTime;

        controller.Move(direction);

        if (direction != Vector3.zero) gameObject.transform.forward = direction;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (MiniMapClickHandler.instance) MiniMapClickHandler.instance.isMiniMapClicked = false;
        }
    }
}
