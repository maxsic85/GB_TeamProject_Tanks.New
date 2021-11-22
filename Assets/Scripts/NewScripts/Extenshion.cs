using UnityEngine;

namespace AS
{
    public static class Extenshion
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.GetComponent<T>() == null) gameObject.AddComponent<T>();
            return gameObject.GetComponent<T>();
        }

        public static class CustomResources
        {
            public static T Load<T>(string path) where T : Object
            {
                return (T)Resources.Load(path, typeof(T));
            }
        }

        public static SkillType GetSkillFromEnum(int index)
        {
          SkillType  _skillType = index switch
            {
                0 => SkillType.FIRE,
                1 => SkillType.WATER,
                2 => SkillType.EARTH,
                _ => 0
            };
            return _skillType;
        }
        public static Sprite GetSpriteBySkillType(SkillType index)
        {
            Sprite sprite = index switch
            {
                SkillType.FIRE => CombatHandler.Instance._roundData.Images[0],
                SkillType.WATER => CombatHandler.Instance._roundData.Images[1],
                SkillType.EARTH => CombatHandler.Instance._roundData.Images[2],
                _ => CombatHandler.Instance._roundData.Images[0]
            };
            return sprite;
        }

    }
}