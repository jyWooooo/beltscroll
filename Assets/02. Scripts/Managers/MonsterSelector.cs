using System;
using UnityEngine;

public class MonsterSelector
{
    private MonsterBase _selectedMonster;

    public event Action<MonsterBase> OnSelectedMonsterChanged;
    public event Action<MonsterBase> OnSelectedMonsterClicked;

    public void Update()
    {
        SelectMonster(FindRaycast());

        if (Input.GetMouseButtonDown(0))
            OnSelectedMonsterClicked?.Invoke(_selectedMonster);
    }

    private MonsterBase FindRaycast()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        var hit = Physics2D.Raycast(mousePos, Vector3.forward, float.PositiveInfinity, 1 << 7);
        if (hit)
            return hit.rigidbody.GetComponent<MonsterBase>();
        return null;
    }

    private void SelectMonster(MonsterBase select)
    {
        if (select == null)
        {
            DeselectMonster();
            return;
        }

        select.SpriteRenderer.color = Color.yellow;
        _selectedMonster = select;
        OnSelectedMonsterChanged?.Invoke(select);
    }

    private void DeselectMonster()
    {
        if (_selectedMonster == null)
            return;

        _selectedMonster.SpriteRenderer.color = Color.white;
        _selectedMonster = null;
        OnSelectedMonsterChanged?.Invoke(null);
    }
}