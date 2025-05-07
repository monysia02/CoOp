using UnityEngine;

public class DoubleSwitchManager : MonoBehaviour
{
    public Animator doorAnimator;

    private bool switchA = false;
    private bool switchB = false;

    public int requiredPoints = 10; 

    public void SetSwitchState(int switchIndex, bool isPressed)
    {
        if (switchIndex == 0) switchA = isPressed;
        else if (switchIndex == 1) switchB = isPressed;

        TryOpenDoor();
    }

    private void TryOpenDoor()
    {
        bool enoughPoints = PointCollector.Instance.HasEnoughPoints(requiredPoints);

        if (switchA && switchB && enoughPoints)
            doorAnimator.SetBool("isOpen", true);
        else
            doorAnimator.SetBool("isOpen", false);
    }
}
