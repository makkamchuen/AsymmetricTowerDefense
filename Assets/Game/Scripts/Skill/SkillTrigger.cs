using UnityEngine;


public class SkillTrigger: MonoBehaviour
{
        
    public void CastSkill()
    {
        GetComponentInParent<Skill>().CastSkill();
    }
        
}
