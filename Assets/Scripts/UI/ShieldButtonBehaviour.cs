using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShieldButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private MeshRenderer _playerMesh;
    [SerializeField] private Material _defaultPlayerMaterial;
    [SerializeField] private Material _shieldMaterial;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(StartColldownAfterDelay());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();
        TurnOffShield();
    }

    private IEnumerator StartColldownAfterDelay()
    {
        TurnOnShield();
        yield return new WaitForSeconds(2f);
        TurnOffShield();
    }

    private void TurnOnShield()
    {
        _playerMesh.material = _shieldMaterial;
        _playerBehaviour.MakeInvincible();
    }

    private void TurnOffShield()
    {
        _playerMesh.material = _defaultPlayerMaterial;
        _playerBehaviour.MakeVulnurable();
    }
}