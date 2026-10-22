using UnityEngine;
using UnityEngine.InputSystem;

public class HandSelectionManager : MonoBehaviour
{
    [Header ("Hands")]
    public Transform blueHand;
    public Transform orangeHand;

    [Header ("Hand Scripts")]
    public BlueHandActions BlueHandActions;
    public OrangeHandActions OrangeHandActions;

    [Header ("Camera")]
    public CameraFollow cameraFollow;

    [Header ("Scaling")]
    public Vector3 normalScale = Vector3.one;
    public Vector3 selectedScale = new Vector3(1.3f, 1.3f, 1.3f);

    private enum SelectedHand { None, Blue, Orange }
    private SelectedHand currentSelection = SelectedHand.None;
    //Start game with none selected

    void Start()
    {
        DeselectAll();
        //make sure game starts with none selected
    }

    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                SelectBlueHand();
            }
            else if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                SelectOrangeHand();
            }
            else if(Keyboard.current.vKey.wasPressedThisFrame)
            {
                DeselectAll();
            }
        }
    }

        void SelectBlueHand()
        {
            currentSelection = SelectedHand.Blue;

            blueHand.localScale = selectedScale;
            orangeHand.localScale = normalScale;

            if (BlueHandActions != null) BlueHandActions.SetSelected (true);
            if (OrangeHandActions != null) OrangeHandActions.SetSelected (false);

            if (cameraFollow != null) cameraFollow.MoveCameraLeft();
        }

        void SelectOrangeHand()
        {
            currentSelection = SelectedHand.Orange;

            blueHand.localScale = normalScale;
            orangeHand.localScale = selectedScale;

            if (BlueHandActions != null) BlueHandActions.SetSelected (false);
            if (OrangeHandActions != null) OrangeHandActions.SetSelected (true);

            if (cameraFollow != null) cameraFollow.MoveCameraRight();
        }

        void DeselectAll()
        {
            currentSelection = SelectedHand.None;

            blueHand.localScale = normalScale;
            orangeHand.localScale = normalScale;

            if (BlueHandActions != null) BlueHandActions.SetSelected (false);
            if (OrangeHandActions != null) OrangeHandActions.SetSelected (false);
            
            if (cameraFollow != null) cameraFollow.ResetCameraSide();
        }

}
