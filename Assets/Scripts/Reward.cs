using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private GameObject _takenPanel;

    public void SetTaken(bool takenStatus)
    {
        _takenPanel.SetActive(takenStatus);
    }
}
