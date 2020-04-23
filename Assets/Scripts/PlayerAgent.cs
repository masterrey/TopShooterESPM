using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgent : MonoBehaviour
{

    public NavMeshAgent agent;
    public Animator anim;

    public enum States{
        Idle,
        GoingTo,
        Attack
    }
    public States myState = States.Idle;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Idle());
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                agent.SetDestination(hit.point);
            }


        }

        anim.SetFloat("Velocity",agent.velocity.magnitude);
    }

    void ChangeState(States currentState)
    {
        myState = currentState;
        StartCoroutine(currentState.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ChangeState(States.Attack);

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ChangeState(States.Idle);
        }

    }

    IEnumerator Idle()
    {
        //start


        while (myState == States.Idle)
        {
            //update
            
            
            yield return new WaitForEndOfFrame();
            //late update

            if (agent.velocity.magnitude > 0.1f)
            {
                ChangeState(States.GoingTo);
            }
        }
        //exit state

        
    }

    IEnumerator GoingTo()
    {
        //start


        while (myState == States.GoingTo)
        {
            //update


            yield return new WaitForEndOfFrame();
            //late update

            if (agent.velocity.magnitude < 0.1f)
            {
                ChangeState(States.Idle);
            }
        }
        //exit state

       
    }

    IEnumerator Attack()
    {
        //start
        anim.SetBool("Attack", true);

        while (myState == States.Attack)
        {
            //update


            yield return new WaitForEndOfFrame();
            //late update

            
        }
        //exit state
        anim.SetBool("Attack", false);

    }
}
