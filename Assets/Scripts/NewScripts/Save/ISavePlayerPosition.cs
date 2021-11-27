using System;

namespace AS
{
    public interface ISavePlayerPosition
    {
        void Save(int position);
        void Load(int position);
        public event Action<int> OnLoadHealth;
    }
}