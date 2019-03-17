using Assets.Code.Attractions;
using UnityEngine;

public class SwingLogic : AttractionLogic
{
    public GameObject Arm;

    public SwingLogic()
    {
        AttractionName = "Swing";
    }

    public override void Update()
    {
        base.Update();

        Arm.transform.rotation = Quaternion.Euler(Mathf.Sin(Time.time) * Speed * 1.3f, 0, 0);
    }
}