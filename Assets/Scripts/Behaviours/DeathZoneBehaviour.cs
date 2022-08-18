using UnityEngine;

public class DeathZoneBehaviour : MonoBehaviour
{
    private PlayerBehaviour _player;

    private void OnTriggerEnter(Collider other)
    {
        DestroyPlayer(other);
    }

    private void OnTriggerStay(Collider other)
    {
        DestroyPlayer(other);
    }

    private void DestroyPlayer(Collider other)
    {
        _player = other.gameObject.GetComponent<PlayerBehaviour>();

        if (_player && !_player.invincible)
            StartCoroutine(_player.DieAndRespawn());
    }
}