using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

using TurkeyWork.Actors;
using TurkeyWork.HUD;
using TurkeyWork.Stats;
using TurkeyWork.Networking;

public class IndicatorController : MonoBehaviour {

    [AssetsOnly]
    public ResourceType resourceType;
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
        actorAttributes = targetActor.Attributes;
        resource = actorAttributes[resourceType];
        Debug.Log(resource.Type.name);
    }

    private void Reset()
    {
        Foreground = transform.Find("Foreground")?.GetComponent<Image>();
        Background = transform.Find("Background")?.GetComponent<Image>();
        IndicatorTemplate = Resources.Load<AttributeIndicatorTemplate>(resourceType.name);
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
