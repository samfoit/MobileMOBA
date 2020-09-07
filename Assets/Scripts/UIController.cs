using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject controller;
    [SerializeField] Image line;

    private RectTransform lineRectTransform;

    float distance;
    float angle;

    public Vector2 startPos;
    public Vector2 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        controller.SetActive(false);
        line.gameObject.SetActive(false);
        lineRectTransform = line.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            controller.transform.position = startPos;
            line.transform.position = controller.transform.position;
            lineRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        }
        if (Input.GetMouseButton(0))
        {
            currentPos = Input.mousePosition;
            distance = Vector2.Distance(currentPos, startPos);
            angle = Mathf.Atan2(currentPos.x - startPos.x, currentPos.y - startPos.y) * -180 / Mathf.PI;

            lineRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, distance);
            lineRectTransform.SetPositionAndRotation(controller.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));

            if (startPos.y <= 250 || EventSystem.current.IsPointerOverGameObject()) { return; }

            controller.SetActive(true);
            line.gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            controller.SetActive(false);
            line.gameObject.SetActive(false);
        }
    }
}
