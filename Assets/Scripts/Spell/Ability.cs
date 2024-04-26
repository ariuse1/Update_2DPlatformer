using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    internal class Ability : IAbility
    {
        private float _damege;

        public Ability(float damege)
        {
            _damege = damege;
        } 


        public void Apply(Heallth health)
        {
            throw new NotImplementedException();
        }

        public float GetDamage()
        {
            throw new NotImplementedException();
        }
    }
}
