using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera playerCamera;
    public float mouseSensitivity = 50f;
    CharacterController player;
    Plane[] cameraFrustum;
    public List<GameObject> monsters = new List<GameObject>();

    float x_rotation = 0f;

    void Start()
    {
        player = this.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.timeScale == 0) { return; }
        //Application.targetFrameRate = 12;
        //Application.targetFrameRate = 85;

        //Blurry Screen
        bool set_blurry = false;
        
        if (monsters.Count > 0)
        {
            foreach (GameObject m in monsters)
            {
                
                BaseMonster monster = m.GetComponent<BaseMonster>();
                if (checkForMonster(monster.offset_side, monster.offset_forward, monster.offset_backwards, monster.height, m))
                {
                    set_blurry = true;
                    break;
                }
                
            }
            
        }
        GameObject.Find("ScaredPostProcessing").GetComponent<ScaredHandler>().setScared(set_blurry);
        ///////////////

        if(!player.GetComponent<PlayerMove>().getInInventory())
        {
            Cursor.lockState = CursorLockMode.Locked;
            float mouse_x = Input.GetAxisRaw("Mouse X") * mouseSensitivity * .5f;
            float mouse_y = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * .5f;

            x_rotation -= mouse_y;
            x_rotation = Mathf.Clamp(x_rotation, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(x_rotation, 0, 0);

            player.transform.Rotate(Vector3.up * mouse_x);
        } else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    bool checkForMonster(float offset_side, float offset_forward, float offset_backwards, float height, GameObject m)
    {
        RaycastHit player_check_one;
        RaycastHit player_check_two;
        RaycastHit player_check_three;
        RaycastHit player_check_four;
        RaycastHit player_check_five;
        RaycastHit player_check_six;
        RaycastHit player_check_seven;
        RaycastHit player_check_eight;

        
        Camera player_cam = GetComponentInChildren<Camera>();
        Debug.DrawLine(m.transform.position + (m.transform.right * (offset_side - 0.1f)) - (m.transform.forward * offset_backwards), player_cam.transform.position);
        Debug.DrawLine(m.transform.position - (m.transform.right * (offset_side - 0.1f)) - (m.transform.forward * offset_backwards), player_cam.transform.position);
        Debug.DrawLine(m.transform.position + (m.transform.right * (offset_side - 0.1f)) + (m.transform.forward * offset_forward), player_cam.transform.position);
        Debug.DrawLine(m.transform.position - (m.transform.right * (offset_side - 0.1f)) + (m.transform.forward * offset_forward), player_cam.transform.position);
        Debug.DrawLine(m.transform.position + (m.transform.up * height) + (m.transform.right * (offset_side - 0.1f)) - (m.transform.forward * offset_backwards), player_cam.transform.position);
        Debug.DrawLine(m.transform.position + (m.transform.up * height) - (m.transform.right * (offset_side - 0.1f)) - (m.transform.forward * offset_backwards), player_cam.transform.position);
        Debug.DrawLine(m.transform.position + (m.transform.up * height) + (m.transform.right * (offset_side - 0.1f)) + (m.transform.forward * offset_forward), player_cam.transform.position);
        Debug.DrawLine(m.transform.position + (m.transform.up * height) - (m.transform.right * (offset_side - 0.1f)) + (m.transform.forward * offset_forward), player_cam.transform.position);
        if (Physics.Linecast(m.transform.position + (m.transform.right * (offset_side - 0.1f)) - (m.transform.forward * offset_backwards), player_cam.transform.position, out player_check_one) &&
            Physics.Linecast(m.transform.position - (m.transform.right * (offset_side - 0.1f)) - (m.transform.forward * offset_backwards), player_cam.transform.position, out player_check_two) &&
            Physics.Linecast(m.transform.position + (m.transform.right * (offset_side - 0.1f)) + (m.transform.forward * offset_forward), player_cam.transform.position, out player_check_three) &&
            Physics.Linecast(m.transform.position - (m.transform.right * (offset_side - 0.1f)) + (m.transform.forward * offset_forward), player_cam.transform.position, out player_check_four) &&
            Physics.Linecast(m.transform.position + (m.transform.up * height) + (m.transform.right * (offset_side - 0.1f)) - (m.transform.forward * offset_backwards), player_cam.transform.position, out player_check_five) &&
            Physics.Linecast(m.transform.position + (m.transform.up * height) - (m.transform.right * (offset_side - 0.1f)) - (m.transform.forward * offset_backwards), player_cam.transform.position, out player_check_six) &&
            Physics.Linecast(m.transform.position + (m.transform.up * height) + (m.transform.right * (offset_side - 0.1f)) + (m.transform.forward * offset_forward), player_cam.transform.position, out player_check_seven) &&
            Physics.Linecast(m.transform.position + (m.transform.up * height) - (m.transform.right * (offset_side - 0.1f)) + (m.transform.forward * offset_forward), player_cam.transform.position, out player_check_eight))
        {
            if (player_check_one.transform.CompareTag("Player") ||
                player_check_two.transform.CompareTag("Player") ||
                player_check_three.transform.CompareTag("Player") ||
                player_check_four.transform.CompareTag("Player") ||
                player_check_five.transform.CompareTag("Player") ||
                player_check_six.transform.CompareTag("Player") ||
                player_check_seven.transform.CompareTag("Player") ||
                player_check_eight.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
