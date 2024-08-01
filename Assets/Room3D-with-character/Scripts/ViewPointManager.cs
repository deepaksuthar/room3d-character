using UnityEngine;

public class ViewPointManager : MonoBehaviour, IViewPointClickHandler
{

    public CharacterMovementController character;

    public void OnViewPointClicked(ViewPoint viewPoint)
    {
        Debug.Log("ViewPoint clicked: " + viewPoint.name);

        character.StartAutoMove(viewPoint.transform);

    }
}

public interface IViewPointClickHandler
{
    void OnViewPointClicked(ViewPoint viewPoint);
}
