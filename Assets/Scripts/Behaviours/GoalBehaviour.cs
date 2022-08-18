using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti;
    [SerializeField] private Animator _blackScreenAnimator;

    private string triggerName = "turnOn";

    private void OnTriggerEnter(Collider other)
    {
        _confetti.Play();
        StartCoroutine(LoadNextLevelAfterDelay());
    }

    private IEnumerator LoadNextLevelAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        _blackScreenAnimator.SetTrigger(triggerName);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}