using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TurkeyWork.Actors;
using TurkeyWork.HUD;
using TurkeyWork.Stats;

public class IndicatorController : MonoBehaviour {

    public string attributeName;
    public AttributeIndicatorTemplate IndicatorTemplate;
    public Image Foreground, Background;

    private float resourcePercentage, previousPercent;
    private ActorAttributes actorAttributes;
    private PlayerController targetActor;
    private Resource resource;

    private void Awake()
    {
        Foreground.sprite = IndicatorTemplate.foreground ? IndicatorTemplate.foreground : Foreground.sprite;
        Foreground.color = IndicatorTemplate.colorGradients[0];
        Background.sprite = IndicatorTemplate.background ? IndicatorTemplate.background : Background.sprite;

        /*if (GameManager.Instance.PlayerActor != null)
        {
            targetActor = GameManager.Instance.PlayerActor.Actor;
            actorAttributes = targetActor.GetComponent<ActorAttributes>()?.GetAttribute<ActorAttributes>(attributeName);
        }
        else
        {
            enabled = false;
            GameManager.Instance.PlayerCreated += OnPlayerCreated;
        }*/
        targetActor = FindObjectOfType<PlayerController>();
        actorAttributes = targetActor?.Attributes;

        switch (attributeName.ToLower())
        {
            case "health":
                resource = actorAttributes.Health;
                break;

            case "mana":
                resource = actorAttributes.Mana;
                break;
            case "stamina":
                resource = actorAttributes.Stamina;
                break;

            default:
                break;
        }
        
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

        // Setting fillAmount evaluate according to resources percentage
        // Curve sampled in editor
        // Percentage implements time
        Foreground.fillAmount = IndicatorTemplate.animationCurve.Evaluate(resourcePercentage);
    }

    void OnPlayerCreated(PlayerController player)
    {
        //actorAttributes = player.GetComponent<ActorAttributes>()?.GetAttribute<ActorAttributes>(attributeName);
        enabled = true;
    }

    void OnActorDeath()
    {
        enabled = false;
        Foreground.fillAmount = 0;
    }
}
