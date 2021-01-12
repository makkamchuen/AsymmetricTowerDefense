using UnityEngine;
using System.Collections;
public class HeroScriptToAnimate : MonoBehaviour
{
    public Animator animator;
    public float MovementSpeed = 1;
    public bool HasWeapon = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("HasWeapon", !animator.GetBool("HasWeapon"));
        }

        if (Input.GetKeyDown(KeyCode.R) && animator.GetBool("HasWeapon"))
        {
            animator.SetTrigger("HeroAttack");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("HeroDead");
        }
    }
}