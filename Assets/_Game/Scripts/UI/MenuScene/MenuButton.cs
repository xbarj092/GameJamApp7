using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Highlight()
    {
        _animator.SetTrigger("Highlight");
    }

    public void Unhighlight()
    {
        _animator.SetTrigger("Unhighlight");
    }
}
