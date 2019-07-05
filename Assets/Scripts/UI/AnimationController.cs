using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetBoolOn(string name)
    {
        animator.SetBool(name, true);
    }

    public void SetBoolOff(string name)
    {
        animator.SetBool(name, false);
    }

    public void ToggleBool(string name)
    {
        animator.SetBool(name, !animator.GetBool(name));
    }
}