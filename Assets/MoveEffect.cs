using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Naninovel;
namespace Assets
{
  public  class MoveEffect:MonoBehaviour
    {


        private void Start()
        {
            var characters = Engine.GetService<ICharacterManager>();
            var character = characters.GetActor("y");
            Test(character);
        }

        IEnumerator Test(ICharacterActor actor) 
        {

            for (int i = 0; i < 1000; i++) 
            {

                if (i % 10 == 0)
                {
                    actor.Position += Vector3.left * 0.1f;
                }
                yield return null;
            }
            
            yield return null;
        }


    }
}
