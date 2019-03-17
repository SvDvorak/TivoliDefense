using Assets;
using Assets.Code.Attractions;
using TMPro;
using UnityEngine;

public class OpenModification : MonoBehaviour
{
    public AttractionLogic ModicationsLogic;
    public GameObject Window;
    public TextMeshProUGUI Title;
    
    public void Start()
    {
        Title.text = ModicationsLogic.AttractionName;
    }

    public void Update()
    {
        Window.transform.rotation = Quaternion.LookRotation(Window.transform.position - Camera.main.transform.position, Vector3.up);
    }

    public void OnMouseDown()
    {
        Gamestate.SetWindowState(this, !Window.activeSelf);
    }

    public void Open()
    {
        Window.SetActive(true);
    }

    public void Close()
    {
        Window.SetActive(false);
    }

    public void Repair()
    {
        ModicationsLogic.Repair();
    }

    public void Upgrade()
    {
        ModicationsLogic.Repair();
    }
}