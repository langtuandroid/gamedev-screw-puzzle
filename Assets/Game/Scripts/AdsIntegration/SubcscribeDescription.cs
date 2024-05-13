using System;
using System.Collections;
using System.Threading.Tasks;
using Integration;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SubcscribeDescription : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] 
    private GDPRLinksHolder _gdprLinksHolder;
    [SerializeField] 
    private TextMeshProUGUI _descriptionText;
    
    private string _privacyLink;
    private string _termsLink;
    private bool _externalOpeningUrlDelayFlag;
    
    private void Start()
    {
        _privacyLink =  _gdprLinksHolder.PrivacyPolicy;
        _termsLink =  _gdprLinksHolder.TermsOfUse;
        RefreshDescription();
    }

    private void RefreshDescription()
    {

        string descriptionText = "Subscription Title: Disabling auto-play advertisements\n" +
                                 "Subscription Duration: Monthly/Yearly/Forever:\n" +
                                 "Subscription Price: $3.99 per month/ $12.99 per year/ $19.99 one time purchase\n" +
                                 "Description: Enjoy ad-free gaming! Subscription disables auto-play advertisements within the application.\n" +
                                 "Free Trial Information: Free trial subscription is automatically renewed unless cancelled 24 hours before the renewal\n" +
                                 "Any unused portion of a free trial period, if offered, will be forfeited when the user purchases a subscription to that publication.\n" +
                                 "Terms and Conditions: By subscribing, you agree to our <b><color=blue><u><link=\"Terms of Service\">Terms of Service</link></u></color></b> and acknowledge that your subscription will automatically\n" +
                                 "renew unless canceled at least 24 hours before the end of the current period. Payment will be charged to your iTunes Account upon confirmation of purchase.\n" +
                                 "Privacy Policy: Your privacy is important to us. Please review our <b><color=blue><u><link=\"Privacy Policy\">Privacy Policy</link></u></color></b> to understand how we collect, use, and protect your personal information.\n" +
                                 "Subscription Management: You can manage your subscription and turn off auto-renewal by going to your iTunes Account Settings after purchase.";

        _descriptionText.text = descriptionText;
    }

    
    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(_descriptionText, eventData.position, eventData.pressEventCamera);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = _descriptionText.textInfo.linkInfo[linkIndex];
            switch (linkInfo.GetLinkID())
            {
                case "Privacy Policy":
                    OpenUrl(_privacyLink);
                    break;
                case "Terms of Service":
                    OpenUrl(_termsLink);
                    break;
            }
        }
    }
    
    private async void OpenUrl(string url)
    {
        if (_externalOpeningUrlDelayFlag) return;
        _externalOpeningUrlDelayFlag = true;
        await OpenURLAsync(url);
        StartCoroutine(WaitForSeconds(1, () => _externalOpeningUrlDelayFlag = false));
    }
    
    private async Task OpenURLAsync(string url)
    {
        await Task.Delay(1);
        try
        {
            Application.OpenURL(url);
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка при открытии ссылки {url}: {e.Message}");
        }
    }

    private IEnumerator WaitForSeconds(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    } 
}
