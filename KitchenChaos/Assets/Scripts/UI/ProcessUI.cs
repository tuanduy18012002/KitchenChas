using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessUI : MonoBehaviour
{
    private const string IS_WARNING = "isWarning";

    [SerializeField] private GameObject hasProcessGameObject;
    [SerializeField] private Image barImage;
    [SerializeField] private Image warningImage;

    private Animator animator;
    private IHasProcessBar hasProcessBar;
    private bool isWarning;
    private float soundWarningTimer;
    private float soundWarningTimerMax = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hasProcessBar = hasProcessGameObject.GetComponent<IHasProcessBar>();
    }

    private void Start()
    {  
        barImage.fillAmount = 0f;
        hasProcessBar.isFried = false;
        isWarning = false;
        Hide();

        hasProcessBar.processChanged += HasProcessBar_processChanged;
    }

    private void Update()
    {
        if (isWarning)
        {
            soundWarningTimer -= Time.deltaTime;
            if (soundWarningTimer <= 0)
            {
                soundWarningTimer = soundWarningTimerMax;
                SoundManager.Instance.PlayWarningSound(transform.position);
            }
        }
    }

    private void HasProcessBar_processChanged(object sender, IHasProcessBar.ProcessChangedEventArgs e)
    {
        if (e.processPerMax <= 0 || e.processPerMax >= 1)
        {
            Hide();
            isWarning = false;

        }
        else
        {
            if (hasProcessBar.isFried && e.processPerMax > 0.5f)
            {
                isWarning = true;
                soundWarningTimer = 0f;
            }
            barImage.fillAmount = e.processPerMax;
            Show();
        }
        animator.SetBool(IS_WARNING, isWarning);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);    
    }
}
