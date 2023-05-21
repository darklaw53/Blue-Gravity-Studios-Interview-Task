using UnityEngine;

public class OutfitSwap : Singleton<OutfitSwap>
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SwapOutfit(string outfit)
    {
        animator.SetTrigger(outfit);
    }
}