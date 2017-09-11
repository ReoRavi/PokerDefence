using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{

    [SerializeField]
    private bool touched;
    [SerializeField]
    private Transform dragbox;
    [SerializeField]
    private Vector3 startTouchPos;
    [SerializeField]
    private Vector3 currentTouchPos;

    private float xPos, yPos;

    private int xDirection, yDirection;

    private bool drag;

    // Use this for initialization
    void Start()
    {
        touched = false;
        dragbox = GameObject.Find("DragBox").GetComponent<Transform>();
        dragbox.gameObject.SetActive(false);

        xPos = 0;
        yPos = 0;

        xDirection = 0;
        yDirection = 0;

        drag = false;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Vector2 pos = Input.GetTouch(0).position;
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0.0f);

            Ray ray = Camera.main.ScreenPointToRay(theTouch);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))    // 레이저를 끝까지 쏴블자. 충돌 한넘이 있으면 return true다.
            {
                TouchObject touchObject = hit.collider.gameObject.GetComponent<TouchObject>();

                if (touchObject == null)
                    return;

                if (Input.GetTouch(0).phase == TouchPhase.Began)    // 딱 처음 터치 할때 발생한다
                {
                    Debug.Log(touchObject);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)    // 터치하고 움직이믄 발생한다.
                {
                    // 또 할거 하고
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)    // 터치 따악 떼면 발생한다.
                {
                    // 할거 해라.
                }
            }
        }
#endif

#if UNITY_EDITOR
        //if (Input.GetMouseButtonDown(0))
        //{
        //    touched = true;
        //    startTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    Vector3 size = dragbox.GetComponent<Renderer>().bounds.size;

        //    dragbox.transform.position = new Vector3(startTouchPos.x + (size.x / 2), startTouchPos.y - (size.y / 2), dragbox.position.z);

        //    //Debug.Log();
        //}
        //else if (Input.GetMouseButton(0))
        //{
        //    Debug.Log("Drag");
        //    if (!drag)
        //        drag = true;

        //    dragbox.gameObject.SetActive(true);

        //    currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    dragbox.transform.localScale = new Vector3(currentTouchPos.x - startTouchPos.x, startTouchPos.y - currentTouchPos.y, dragbox.transform.position.z);

        //    Vector3 size = dragbox.GetComponent<Renderer>().bounds.size;

        //    if (currentTouchPos.x < startTouchPos.x)
        //    {
        //        xPos = currentTouchPos.x + (size.x / 2);
        //        xDirection = 1;
        //    }
        //    else
        //    {
        //        xPos = startTouchPos.x + (size.x / 2);
        //        xDirection = -1;
        //    }

        //    if (currentTouchPos.y < startTouchPos.y)
        //    {
        //        yPos = startTouchPos.y - (size.y / 2);
        //        yDirection = 1;
        //    }
        //    else
        //    {
        //        yPos = currentTouchPos.y - (size.y / 2);
        //        yDirection = -1;
        //    }

        //    dragbox.transform.position = new Vector3(xPos, yPos, dragbox.position.z);
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    touched = false;
        //    if (drag)
        //    {
        //        dragbox.gameObject.SetActive(false);
        //        GameManager.Instance.SelectDragedTurret(dragbox.transform.position, dragbox.transform.localScale, xDirection, yDirection);
        //        drag = false;
        //    }
        //}

#endif
    }
}
