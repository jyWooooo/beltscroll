using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MonsterInfo : UI_Scene
{
    private enum Texts
    {
        textName,
        textGrade,
        textSpeed,
        textHealth,
    }

    private enum Images
    {
        imgPortrait,
    }

    private TextMeshProUGUI _name;
    private TextMeshProUGUI _grade;
    private TextMeshProUGUI _speed;
    private TextMeshProUGUI _health;
    private Image _portrait;
    private MonsterSelector _selector;
    private CanvasGroup _canvasGroup;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindElements();
        _selector = GetComponent<MonsterSelector>();
        _selector.OnSelectedMonsterClicked += SetMonsterInfo;
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;

        return true;
    }

    private void BindElements()
    {
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        _name = GetText((int)Texts.textName);
        _grade = GetText((int)Texts.textGrade);
        _speed = GetText((int)Texts.textSpeed);
        _health = GetText((int)Texts.textHealth);
        _portrait = GetImage((int)Images.imgPortrait);
    }

    private void SetMonsterInfo(MonsterBase monster)
    {
        if (monster == null)
        {
            _canvasGroup.alpha = 0f;
            return;
        }

        _canvasGroup.alpha = 1f;
        _name.text = monster.Status.Name;
        _grade.text = monster.Status.Grade.ToString();
        _speed.text = monster.Status.Speed.ToString();
        _health.text = monster.Status.Health.ToString();
        _portrait.sprite = monster.SpriteRenderer.sprite;
        _portrait.SetNativeSize();
        _portrait.rectTransform.localPosition = Vector3.zero;
    }

    private void OnDestroy()
    {
        _selector.OnSelectedMonsterClicked -= SetMonsterInfo;
    }
}