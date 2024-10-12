using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRightMenuController : MonoBehaviour
{

    private bool _open = false;
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMenu()
    {
        if (_open)
            Close();
        else
            Open();
    }

    void Open()
    {
        _anim.SetBool("menuOpen", true);
        _open = true;

    }

    void Close()
    {
        AnimatorStateInfo animState = _anim.GetCurrentAnimatorStateInfo(0);
        _anim.SetBool("menuOpen", false);

        if (animState.IsName("MenuOpen"))
        {
            float currentAnimTime = animState.normalizedTime;
            _anim.Play("MenuClose", 1, 1f - currentAnimTime);
        }
        _open = false;
    }

    public void ToMainMenu()
    {

    }

    public void ToDesktop()
    {
        Application.Quit();
    }

    public void ToOptions()
    {

    }
    
}
