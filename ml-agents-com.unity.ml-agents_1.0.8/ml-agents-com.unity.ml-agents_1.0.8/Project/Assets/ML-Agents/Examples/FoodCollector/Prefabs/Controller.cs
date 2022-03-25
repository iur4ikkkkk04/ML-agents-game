using UnityEngine;
using System.Collections;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class Controller : MonoBehaviour
{
    public float speed = 10f;

    public float run = 20f;

    public CharacterController characterController;

    Vector3 move;

    Vector3 velocity;

    public float gravity = -22f;

    public Transform groundCheck;

    public float groundDistance = 0.4f;

    public LayerMask groundMask;

    bool isGrounded;

    public float jumpHight = 3f;

    public int Score;
    public int TotalScore = 5;

    private IEnumerator DashControll(float height, float vheight)
    { 
        Vector3 dash = transform.forward * vheight + transform.right * height;
        characterController.Move(dash * 0.5f);
        yield return null;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        move = transform.right * horizontal + transform.forward * vertical;

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
            StartCoroutine(DashControll(horizontal, vertical));
        else 
            characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                velocity.y = Mathf.Sqrt(jumpHight * -2 * gravity);
    }

    void OnTriggerEnter(Collider pPlayer)
    {
        if (pPlayer.gameObject.tag == "food")
        {
            Score++;
            //pPlayer.gameObject.SetActive(false);
            //Destroy(pPlayer.gameObject);
            pPlayer.gameObject.GetComponent<FoodLogic>().OnEaten();
            TotalScore--;
        }
        else if (pPlayer.gameObject.tag == "badFood")
        {
            Score--;
            //pPlayer.gameObject.SetActive(false);
            //Destroy(pPlayer.gameObject);
            pPlayer.gameObject.GetComponent<FoodLogic>().OnEaten();
        }
    }
}