using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Ball ball;
    [SerializeField] GameObject arrow;
    [SerializeField] Image aim;
    [SerializeField] LineRenderer line;
    [SerializeField] LayerMask ballLayer;
    [SerializeField] LayerMask rayLayer;
    [SerializeField] Transform camPivot;
    [SerializeField] Camera cam;
    [SerializeField] Vector2 camSensitivity;
    [SerializeField] TMP_Text shootCountText;
    [SerializeField] float shootForce;

    Vector3 mouseLastPosition;
    Vector3 forceDir;
    int shootCount;
    bool isShooting;
    float ballDistance;
    float forceMagnitude;
    float forceFactor;

    Renderer[] arrowRends;
    public int ShootCount {get => shootCount;}

    // Start is called before the first frame update
    void Start()
    {
        ballDistance = Vector3.Distance(cam.transform.position, ball.Position);
        arrowRends = GetComponentsInChildren<Renderer>();
        arrow.SetActive(false);

        shootCountText.text = "Shot : " + shootCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(ball.IsMoving)
            return;
        
        if(this.transform.position != ball.Position)
        {
            this.transform.position = ball.Position;
            aim.gameObject.SetActive(true);
            var rect = aim.GetComponent <RectTransform> ();
            rect.anchoredPosition = cam.WorldToScreenPoint(ball.Position);
        }

        this.transform.position = ball.Position;

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("1");
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, ballDistance, ballLayer))
            {
                isShooting = true;
                arrow.SetActive(true);
            }
        }

        if(Input.GetMouseButton(0) && isShooting==true)
        {
            Debug.Log("2");
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, ballDistance*2, rayLayer))
            {
                Debug.DrawLine(ball.Position, hit.point);
                Debug.Log("2 Done " + hit.point);
                
                var forceVector = (ball.Position - hit.point);
                forceVector = new Vector3(forceVector.x, 0, forceVector.z);
                forceDir = forceVector.normalized;
                var forceMagnitude = forceVector.magnitude;
                forceMagnitude = Mathf.Clamp(forceMagnitude,0,5);
                forceFactor = forceMagnitude/5;
                Debug.Log(hit.point);
                //Debug.Log("2 Done!!");
            }

            this.transform.LookAt(this.transform.position + forceDir);
            arrow.transform.localScale = new Vector3(1 + 0.5f*forceFactor, 1 + 0.5f*forceFactor, 1 + 2*forceFactor);

            foreach (var rend in arrowRends)
            {
                rend.material.color = Color.Lerp(Color.white, Color.red, forceFactor);
            }

            var rect = aim.GetComponent <RectTransform> ();
            rect.anchoredPosition = Input.mousePosition;

            var ballScrPos = cam.WorldToScreenPoint(ball.Position);
            line.SetPositions(new Vector3[] {ballScrPos, Input.mousePosition});
        }

        if(Input.GetMouseButton(1))
        {
            var current = cam.ScreenToViewportPoint(Input.mousePosition);
            var last = cam.ScreenToViewportPoint(mouseLastPosition);
            var delta = current - last;

            camPivot.transform.RotateAround(
                ball.Position, Vector3.up, delta.x * camSensitivity.x);
            camPivot.transform.RotateAround(
                ball.Position, cam.transform.right, -delta.y*camSensitivity.y);

            var angle = Vector3.SignedAngle(Vector3.up, cam.transform.up ,cam.transform.right);
            // Debug.Log(angle);

            if(angle < 3)
                camPivot.transform.RotateAround(
                ball.Position, cam.transform.right, 3 - angle);

            if(angle > 65)
                camPivot.transform.RotateAround(
                ball.Position, cam.transform.right, 65 - angle);
        }
        
        if(Input.GetMouseButtonUp(0) && isShooting)
        {
            ball.AddForce(forceDir * shootForce * forceFactor);
            shootCount += 1;
            shootCountText.text = "Shot : " + shootCount;
            forceFactor=0;
            forceDir=Vector3.zero;
            isShooting = false;
            arrow.SetActive(false);
        }
        
        mouseLastPosition = Input.mousePosition;

    }
}
