using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace SpaceCleaner.Resources.Managers
{
    public class SoundManager
    {

        public static SoundManager Instance;

        private SoundEffect missileStart;
        private SoundEffect powerupPickup;
        private SoundEffect shipHurt;
        private SoundEffect playerHurt;
        
        public SoundManager(ContentManager content)
        {
            Instance = this;
            missileStart = content.Load<SoundEffect>("sound/laser-gun-shot");
            powerupPickup = content.Load<SoundEffect>("sound/scale-g6-89174");
            shipHurt = content.Load<SoundEffect>("sound/shooting-star-2-104073");
            playerHurt = content.Load<SoundEffect>("sound/explosionCrunch_000");
        }

        public void playMissileShot()
        {
            missileStart.CreateInstance().Play();
        }

        public void playPowerUpPickup()
        {
            powerupPickup.CreateInstance().Play();
        }

        public void playShipHurt()
        {
            shipHurt.CreateInstance().Play();
        }

        public void playPlayerHurt()
        {
            playerHurt.CreateInstance().Play();
        }
        
    }
}
