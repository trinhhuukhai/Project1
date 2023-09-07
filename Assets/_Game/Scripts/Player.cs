using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direct { Forward, Back, Right, Left, None}
public class Player : MonoBehaviour
{
    public float speed = 5;
    public LayerMask layerBrick;
    public Transform playerBrickPrefabs;
    public Transform brickHolder;
    public Transform playerSkin;


    private bool isMoving;
    private bool isControl;
    private Vector3 moveNextPoint;
    private Vector3 mouseDown, mouseUp;
    private List<Transform> playerBricks = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        //OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if ( GameManager.Instance.IsState(GameState.GamePlay) && !isMoving)
        {
            //an chuot xuong
            if (Input.GetMouseButtonDown(0) && !isControl)
            {
                isControl = true;
                mouseDown = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0) && isControl)
            {
                isControl = false;
                mouseUp = Input.mousePosition;

                Direct direct = GetDirect(mouseDown, mouseUp);

                if (direct != Direct.None)
                {
                    moveNextPoint = GetNextPoint(direct);
                    isMoving = true;
                }
            }
        }
        else if(isMoving)
        {
            //check lien tuc khi nao di chuyen den noi
            if (Vector3.Distance(transform.position, moveNextPoint) < 0.1f)
            {
                isMoving = false;
            }
            //di chuyen den vi tri tiep theo
            transform.position = Vector3.MoveTowards(transform.position, moveNextPoint, Time.deltaTime * speed);
        }



    }

    public void OnInit()
    {
        isMoving = false;
        isControl = false;
        ClearBrick();
        playerSkin.localPosition = transform.position;
    }

    private Vector3 GetNextPoint(Direct direct)
    {
        RaycastHit hit;// ban 1 tia tu tren xuong duoi truoc mat (vi tri goc, huong, khoang cach, layer)

        Vector3 nextPoint = transform.position;
        Vector3 dir = Vector3.zero;

        switch (direct)
        {
            case Direct.Forward:
                dir = Vector3.forward;
                break;
            case Direct.Back:
                dir = Vector3.back;

                break;
            case Direct.Right:
                dir = Vector3.right;

                break;
            case Direct.Left:
                dir = Vector3.left;

                break;
            case Direct.None:
                break;
            default:
                break;
        }
        for (int i = 1; i < 100; i++)
        {
            if (Physics.Raycast(transform.position + dir * i + Vector3.up * 2, Vector3.down, out hit, 10f, layerBrick))
            {
                nextPoint = hit.collider.transform.position;
            }
            else
            {
                break;
            }
        }
        return nextPoint;


    }



    private Direct GetDirect(Vector3 mouseDown, Vector3 mouseUp)
    {
        Direct direct = Direct.None;

        float deltaX = mouseUp.x - mouseDown.x;
        float deltaY = mouseUp.y - mouseDown.y;

        if (Vector3.Distance(mouseDown, mouseUp) < 100)
        {
            direct = Direct.None;
        }
        else
        {
            if (Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
            {
                if (deltaY > 0)
                {
                    direct = Direct.Forward;
              
                }
                else
                {
                    direct = Direct.Back;
                }
            }
            else
            {
                if (deltaX > 0)
                {
                    direct = Direct.Right;
                }
                else
                {
                    direct = Direct.Left;
                }
            }
        }
        return direct;
    }


    public void AddBrick()
    {
        int index = playerBricks.Count;
        //khoi tao ben trong player
        Transform playerBrick = Instantiate(playerBrickPrefabs, brickHolder);
        playerBrick.localPosition = Vector3.down + index * 0.25f * Vector3.up;

        playerBricks.Add(playerBrick);

        playerSkin.localPosition = playerSkin.localPosition + Vector3.up * 0.25f;
    }

    public void RemoveBrick()
    {
        int index = playerBricks.Count - 1;
      
        if (index >= 0)
        {
            Transform playerBrick = playerBricks[index];
            playerBricks.Remove(playerBrick);
            Destroy(playerBrick.gameObject);


            playerSkin.localPosition = playerSkin.localPosition - Vector3.up * 0.25f;

        }
    }

    public void ClearBrick()
    {
        for (int i = 0; i < playerBricks.Count; i++)
        {
            Destroy(playerBricks[i].gameObject);
        }
        playerBricks.Clear();
    }
}
