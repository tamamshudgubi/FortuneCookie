using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject _showPredictionPanel;
    [SerializeField] private Cookie _cookie;
    [SerializeField] private Animator _cookieAnimator;
    [SerializeField] private GameObject _prediction;
    [SerializeField] private TMP_Text _predictionText;
    [SerializeField] private Button _shareButton;
    [SerializeField] private Button _oneMoreButton;
    [SerializeField] private GameObject _triesEndedAnnouncer;
    [SerializeField] private FortuneWheel _fortuneWheel;

    private void EnableElements()
    {
        _cookie.enabled = true;
        _fortuneWheel.enabled = true;
        _showPredictionPanel.SetActive(false);
    }

    private void DisableElements()
    {
        _cookie.enabled = false;
        _fortuneWheel.enabled = false;
        _showPredictionPanel.SetActive(true);
    }

    public void ActivateAllButtons()
    {
        DisableElements();
        _oneMoreButton.gameObject.SetActive(true);
        _prediction.SetActive(true);
        _shareButton.gameObject.SetActive(true);
    }

    public void DisActivateAllButtons()
    {
        EnableElements();
        _oneMoreButton.gameObject.SetActive(false);
        _prediction.SetActive(false);
        _shareButton.gameObject.SetActive(false);
    }

    public void ActivateTriesEndedAnnouncer()
    {
        DisActivateAllButtons();
        DisableElements();
        _prediction.SetActive(true);
        _triesEndedAnnouncer.SetActive(true);
    }

    public void TriesRestored()
    {
        ActivateAllButtons();
        _predictionText.gameObject.SetActive(true);
        _shareButton.interactable = true;
        _triesEndedAnnouncer.SetActive(false);
    }

    public void OnOneMoreButtonClick()
    {
        _cookieAnimator.SetBool("IsDestroyed", false);
        DisActivateAllButtons();
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
