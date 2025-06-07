
using UnityEngine;

namespace Game.GameEngine.ActorMail
{
    public sealed class Actor : MonoBehaviour
    {
        private void Start()
        {
            var mailBox = new MailBox();
            var damageMessage1 = new DamageMessage() { Damage = 10 };
            var damageMessage2 = new DamageMessage() { Damage = 15 };
            mailBox.Add(damageMessage1);
            mailBox.Add(damageMessage2);
            
            Debug.Log("First messages");
            foreach(var message in mailBox.GetMessages<DamageMessage>())
            {
                Debug.Log(message.Damage);
            }
            
            Debug.Log("Second messages");
            foreach(var message in mailBox.GetMessages<DamageMessage>())
            {
                Debug.Log(message.Damage);
            }
        }
    }
    
    
    
}