using UnityEngine;

public class ViewPoint : MonoBehaviour
{
    private IViewPointClickHandler clickHandler;
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        clickHandler = FindObjectOfType<ViewPointManager>();
        if (clickHandler == null)
        {
            Debug.LogError("No ViewPointManager found in the scene.");
        }
    }

    private void Update()
    {
        Vector3 direction = cameraTransform.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y + 180f, 0);
        }
    }

    private void OnMouseDown()
    {
        if (clickHandler != null)
        {
            clickHandler.OnViewPointClicked(this);
        }
    }
}
