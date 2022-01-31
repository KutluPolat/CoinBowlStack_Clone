using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Linq;

public class Feedbacks : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private TextMeshProUGUI _generalFeedback;

    private Stack<string> _generalFeedbackStringStack = new Stack<string>();
    private string[] _generalFeedbackStringArray = new string[] { "MAGNIFICENT!", "AWESOME!", "GREAT JOB!", "SUPER!", "BOSS LIKE!", "HEAVENLY!", "SUPERIOR!", "WONDERFUL!", "TERRIFIC!" };

    private Coroutine _feedbackCoroutine;

    [Range(0.2f, 0.4f)]
    private const float FEEDBACK_INTERVAL = 0.4f;

    private bool IsGeneralFeedbackAvaible { get { return _feedbackCoroutine == null; } }

    #endregion // Variables

    #region Methods

    #region General

    public void TriggerGeneralFeedback()
    {
        if (IsGeneralFeedbackAvaible)
        {
            _feedbackCoroutine =
                StartCoroutine(SendFeedback(_generalFeedback, true, GetGeneralFeedback()));
        }
    }

    public void TriggerFeedback(TextMeshProUGUI feedback)
    {
        StartCoroutine(SendFeedback(feedback));
    }

    #endregion // General

    #region Sub Methods

    /// <summary>
    /// Punches and shrink & grows with circ pattern.
    /// </summary>
    /// <param name="feedback">TextMeshProUGUI component.</param>
    /// <param name="isCustomFeedback">True if string is going to change on TextMeshProUGUI component.</param>
    /// <param name="feedbackText">New feedback text if isCustomFeedback true.</param>
    private IEnumerator SendFeedback(TextMeshProUGUI feedback, bool isCustomFeedback = false, string feedbackText = null)
    {
        feedback.alpha = 1;
        feedback.color = new Color(Random.Range(0f, 0.25f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        if (isCustomFeedback)
        {
            if (feedbackText == null)
                Debug.LogError("Don't forget to enter a feedback text!");
            else
                feedback.text = feedbackText;
        }

        feedback.gameObject.transform.localScale = Vector3.one;
        feedback.gameObject.transform.DOScale(2, FEEDBACK_INTERVAL);
        feedback.gameObject.transform.DOPunchRotation(Vector3.one * 3, FEEDBACK_INTERVAL);

        yield return new WaitForSeconds(FEEDBACK_INTERVAL);

        feedback.gameObject.transform.DOScale(1, FEEDBACK_INTERVAL);

        yield return new WaitForSeconds(FEEDBACK_INTERVAL);

        feedback.alpha = 0;
        _feedbackCoroutine = null;
    }

    /// <summary>
    /// Punches and shrink & grows with circ pattern.
    /// </summary>    
    /// <param name="feedback">TextMeshPro component.</param>
    /// <param name="isCustomFeedback">True if string is going to change on TextMeshProUGUI component.</param>
    /// <param name="feedbackText">New feedback text if isCustomFeedback true.</param>
    private IEnumerator SendFeedback(TextMeshPro feedback, bool isCustomFeedback = false, string feedbackText = null)
    {
        feedback.alpha = 1;
        feedback.color = new Color(Random.Range(0f, 0.25f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        if (isCustomFeedback)
        {
            if (feedbackText == null)
                Debug.LogError("Don't forget to enter a feedback text!");
            else
                feedback.text = feedbackText;
        }

        feedback.gameObject.transform.localScale = Vector3.one;
        feedback.gameObject.transform.DOScale(2, FEEDBACK_INTERVAL);
        feedback.gameObject.transform.DOPunchRotation(Vector3.one * 3, FEEDBACK_INTERVAL);

        yield return new WaitForSeconds(FEEDBACK_INTERVAL);

        feedback.gameObject.transform.DOScale(1, FEEDBACK_INTERVAL);

        yield return new WaitForSeconds(FEEDBACK_INTERVAL);

        feedback.alpha = 0;
        _feedbackCoroutine = null;
    }

    private string GetGeneralFeedback()
    {
        if (_generalFeedbackStringStack.Count == 0)
            CreateGeneralFeedbackStack();

        return _generalFeedbackStringStack.Pop();
    }

    private void CreateGeneralFeedbackStack()
    {
        var customerFeedbacksWithRandomizedOrder = _generalFeedbackStringArray.OrderBy(item => Random.value > 0.5f);

        foreach (string feedback in customerFeedbacksWithRandomizedOrder) _generalFeedbackStringStack.Push(feedback);
    }

    #endregion // Sub Methods

    #endregion // Methods
}
