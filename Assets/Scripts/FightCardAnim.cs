using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCardAnim : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnMouseDown()
    {
        anim.SetTrigger("CardFightIsAnim");
    }
}
