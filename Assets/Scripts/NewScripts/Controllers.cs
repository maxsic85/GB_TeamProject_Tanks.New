using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AS
{
    public class Controllers : IInitialisation, IExecute
    {
        private readonly List<IInitialisation> _initialisations;
        private readonly List<IExecute> _executes;

        public Controllers()
        {
            _initialisations = new List<IInitialisation>();
            _executes = new List<IExecute>();
        }

        public Controllers Add(IController controller)
        {
            if (controller is IInitialisation initController)
            {
                _initialisations.Add(initController);
            }

            if (controller is IExecute execute)
            {
                _executes.Add(execute);
            }
            return this;
        }

        public void Execute(float time)
        {
            for (var index = 0; index < _executes.Count; ++index)
            {
                _executes[index].Execute(time);
            }
        }

        public void Initialisation()
        {
            for (var index = 0; index < _initialisations.Count; ++index)
            {
                _initialisations[index].Initialisation();
            }
        }
    }
}

