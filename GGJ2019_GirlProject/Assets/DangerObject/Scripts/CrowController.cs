using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowController : MonoBehaviour
{
    IEnumerator moveEnumerator = null;
    private bool isMoveing;


    private void Update()
    {
        FindGirl();
    }

    private void FindGirl()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.SphereCast(transform.position, 1.0f, Vector3.down, out hit))
        {
            GirlProvider girl = hit.collider.GetComponent<GirlProvider>();
            if (girl)
            {
                
            }
        }
    }

    public void Move(Vector3 position, float delay)
    {
        if(isMoveing)
            StopCoroutine(moveEnumerator);

        moveEnumerator = movePositon(position, delay);
        StartCoroutine(moveEnumerator);
    }

    private IEnumerator movePositon(Vector3 position, float delay)
    {
        isMoveing = true;
        Vector3 oldPosition = transform.position;
        float t = 0;

        do
        {
            transform.position = Vector3.Lerp(oldPosition, position, t / delay);
            t += Time.deltaTime;
            yield return null;
        }
        while (t <= delay) ;
        transform.position = position;
        isMoveing = false;
        yield break;
    }
}
