using UnityEngine;
using TMPro;

public class LoopingTimer : MonoBehaviour
{
    //Timer setting
    public int maxTimeValue = 4;
    private int currentTimeValue = 4;
    
    //Text refs
    public TextMeshProUGUI textTimer;
    public TextMeshProUGUI textBreatheIn;
    public TextMeshProUGUI textHoldBreathIn;
    public TextMeshProUGUI textBreatheOut;
    public TextMeshProUGUI textHoldBreathOut;
    //Text colors
    public Color activeTextColor;
    public Color inactiveTextColor;

    //Moving circle object
    public GameObject movingCircle;
    private float minY = -3.35f;
    private float maxY = 3.35f;
    private float minX = -3.35f;
    private float maxX = 3.35f;
    private Vector2 nextMove;
    private float moveSpeed;

    //internal timer
    private float internalTimer = 0f;
    private float updateRate = 1f;
    private float timeStepThisFrame = 0f;

    private bool moving = false;

    private void Start()
    {
        moveSpeed = (maxY - minY)/ maxTimeValue;
    }

    private void Update()
    {
        timeStepThisFrame = Time.deltaTime;

        if (internalTimer >= updateRate)
        {
            internalTimer = 0;
            moving = false;

            if (currentTimeValue == 1)
            {
                currentTimeValue = maxTimeValue;
            }
            else
            {
                currentTimeValue--;
            }

            textTimer.text = currentTimeValue.ToString();
        }
        else
        {
            internalTimer += timeStepThisFrame;
            moving = true;
        }

        if (movingCircle && moving)
        {
            if (Mathf.Approximately(movingCircle.transform.position.x, minX) 
                && movingCircle.transform.position.y < maxY)
            {
                //breathe in
                nextMove = Vector2.up * moveSpeed * timeStepThisFrame;
                movingCircle.transform.Translate(nextMove);

                //update text colors
                textBreatheIn.color = activeTextColor;
                textHoldBreathIn.color = inactiveTextColor;
                textBreatheOut.color = inactiveTextColor;
                textHoldBreathOut.color = inactiveTextColor;

                if (movingCircle.transform.position.y >= maxY)
                {
                    nextMove = Vector2.zero;
                    movingCircle.transform.SetPositionAndRotation(new Vector3(minX, maxY, 0), Quaternion.identity);
                }

                //Debug.Log("breathing in...");
            }
            else if (Mathf.Approximately(movingCircle.transform.position.y, maxY) 
                    && movingCircle.transform.position.x < maxX)
            {
                //hold breathe in
                nextMove = Vector2.right * moveSpeed * timeStepThisFrame;
                movingCircle.transform.Translate(nextMove);

                //update text colors
                textBreatheIn.color = inactiveTextColor;
                textHoldBreathIn.color = activeTextColor;
                textBreatheOut.color = inactiveTextColor;
                textHoldBreathOut.color = inactiveTextColor;

                if (movingCircle.transform.position.x >= maxX)
                {
                    nextMove = Vector2.zero;
                    movingCircle.transform.SetPositionAndRotation(new Vector3(maxX, maxY, 0), Quaternion.identity);
                }

                //Debug.Log("holding breathe in...");
            }
            else if (Mathf.Approximately(movingCircle.transform.position.x, maxX) 
                    && movingCircle.transform.position.y > minY)
            {
                //breathe out
                nextMove = Vector2.down * moveSpeed * timeStepThisFrame;
                movingCircle.transform.Translate(nextMove);

                //update text colors
                textBreatheIn.color = inactiveTextColor;
                textHoldBreathIn.color = inactiveTextColor;
                textBreatheOut.color = activeTextColor;
                textHoldBreathOut.color = inactiveTextColor;

                if (movingCircle.transform.position.y <= minY)
                {
                    nextMove = Vector2.zero;
                    movingCircle.transform.SetPositionAndRotation(new Vector3(maxX, minY, 0), Quaternion.identity);
                }

                //Debug.Log("breathing out...");
            }
            else if (Mathf.Approximately(movingCircle.transform.position.y, minY) 
                    && movingCircle.transform.position.x > minX)
            {
                //hold breathe in
                nextMove = Vector2.left * moveSpeed * timeStepThisFrame;
                movingCircle.transform.Translate(nextMove);

                //update text colors
                textBreatheIn.color = inactiveTextColor;
                textHoldBreathIn.color = inactiveTextColor;
                textBreatheOut.color = inactiveTextColor;
                textHoldBreathOut.color = activeTextColor;

                if (movingCircle.transform.position.x <= minX)
                {
                    nextMove = Vector2.zero;
                    movingCircle.transform.SetPositionAndRotation(new Vector3(minX, minY, 0), Quaternion.identity);
                }

                //Debug.Log("holding breathe out...");
            }
        }
    }
}
