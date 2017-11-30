using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TurkeyWork.Actors;
using TurkeyWork.HUD;
using TurkeyWork.Stats;
using TurkeyWork.Networking;

public class IndicatorController : MonoBehaviour {

    public string attributeName;
    public AttributeIndicatorTemplate IndicatorTemplate;
    public Image Foreground, Background;

    private float resourcePercentage, previousPercent;
    private ActorAttributes actorAttributes;
    private PlayerController targetActor;
    private Resource resource;

    private void Start()
    {
        Foreground.sprite = IndicatorTemplate.foreground ? IndicatorTemplate.foreground : Foreground.sprite;
        Foreground.color = IndicatorTemplate.colorGradients[0];
        Background.sprite = IndicatorTemplate.background ? IndicatorTemplate.background : Background.sprite;

        targetActor = NetworkManager.GetLocalPlayer ().PlayerEntity.GetComponent<PlayerController> ();  
        actorAttributes = targetActor?.Attributes;

        switch (attributeName.ToLower ()) {
        case "health":
            resource = actorAttributes[ResourceType.FromName ("Health")];
            break;

        case "mana":
            resource = actorAttributes[ResourceType.FromName ("Mana")];
            break;
        case "stamina":
            resource = actorAttributes[ResourceType.FromName ("Stamina")];
            break;

        default:
            break;
        }
        Debug.Log(resource);
    }

    private void Reset()
    {
        Foreground = transform.Find("Foreground")?.GetComponent<Image>();
        Background = transform.Find("Background")?.GetComponent<Image>();
        IndicatorTemplate = Resources.Load<AttributeIndicatorTemplate>(attributeName);
    }

    public void Update()
    {
        if (actorAttributes == null)
            return;

        previousPercent = resourcePercentage;
        resourcePercentage = resource.Percent;

        Debug.Log(resource.Percent);
        // Setting fillAmount evaluate according to resources percentage
        // Curve sampled in editor
        // Percentage implements time
        Foreground.fillAmount = IndicatorTemplate.animationCurve.Evaluate(resourcePercentage);
    }

    void OnActorDeath()
    {
        enabled = false;
        Foreground.fillAmount = 0;
    }
}
