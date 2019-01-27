using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowController : MonoBehaviour
{
    [SerializeField] private PoleController startPole;
    private Animator animator;
    private IEnumerator moveEnumerator = null;
    private bool isMoveing;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        transform.position = startPole.transform.position + Vector3.up * 1.4f;
        startPole.StopCrow(this);
    }

    private void Update()
    {
        FindGirl();
    }

    private void FindGirl()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.SphereCast(transform.position + Vector3.up, 1.0f, Vector3.down * 5f,  out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Girl"))
            {
               DangerObjectManager.FearMover(hit.collider.gameObject, gameObject);
            }
        }
    }

    public void MoveToPole(PoleController pole, float delay)
    {
        if (isMoveing)
            StopCoroutine(moveEnumerator);

        moveEnumerator = MovePositon(pole, delay);
        StartCoroutine(moveEnumerator);
    }

    private IEnumerator MovePositon(PoleController pole, float delay)
    {
        isMoveing = true;
        animator.SetBool("isFrying", true);
        Vector3 oldPosition = transform.position;
        Vector3 position = pole.transform.position + Vector3.up * 1.4f;

        if (position.x - oldPosition.x <= 0) transform.eulerAngles = Vector3.zero;
        else transform.eulerAngles = Vector3.up * 180;

        float t = 0;

        do
        {
            FindGirl();
            transform.position = Vector3.Lerp(oldPosition, position, t / delay);
            t += Time.deltaTime;
            yield return null;
        }
        while (t <= delay) ;
        transform.position = position;
        isMoveing = false;
        animator.SetBool("isFrying", false);
        pole.StopCrow(this);
        yield break;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Girl"))
        {
            DangerObjectManager.FearMover(other.gameObject, gameObject);
        }
    }
}
