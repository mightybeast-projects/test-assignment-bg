using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerBehaviour : MonoBehaviour
{
    [HideInInspector] public bool invincible => _invincible;

    [SerializeField] private GameObject _cube;
    [SerializeField] private ParticleSystem _deathParticles;

    private Collider _collider;
    private List<IPathfindingNode> _path;
    private Vector3 _nextDestination => new Vector3(_path[_currentPathIndex].position.Y, 
        0, _path[_currentPathIndex].position.X);
    private bool _canMove;
    private bool _invincible;
    private int _currentPathIndex;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void Initialize(List<IPathfindingNode> path)
    {
        _path = path;
        StartCoroutine(StartMovingAfterDelay(2f));
    }

    public void MakeInvincible()
    {
        _invincible = true;
    }

    public void MakeVulnurable()
    {
        _invincible = false;
    }

    public IEnumerator DieAndRespawn()
    {
        Die();
        yield return new WaitForSeconds(2);
        Respawn();
    }

    private void Die()
    {
        _canMove = false;
        _cube.SetActive(false);
        _deathParticles.Play();
        _collider.enabled = false;
    }

    private void Respawn()
    {
        _cube.SetActive(true);
        _collider.enabled = true;
        transform.localPosition = Vector3.zero;
        _currentPathIndex = 0;

        StartCoroutine(StartMovingAfterDelay(1f));
    }

    private void FixedUpdate()
    {
        try { Move(); }
        catch (ArgumentOutOfRangeException) { _canMove = false; }
    }

    private void Move()
    {
        if (transform.localPosition == _nextDestination)
            _currentPathIndex++;
        
        if (_canMove)
                transform.localPosition = 
                    Vector3.MoveTowards(transform.localPosition, _nextDestination, 0.05f);
    }

    private IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _canMove = true;
    }
}