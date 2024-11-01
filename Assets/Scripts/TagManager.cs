using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour
{
    // Start is called before the first frame update
    public class Tags {

    // Terreno
        public const string Ground = "Ground";

        // Player
        public const string Walk = "speed";
        public const string Jumping_Bool = "jump";

        //Enemy
        public const string PlayerTag = "Player";
        public const string EnemyTag = "Enemy";
        public const string PunchAttackTag = "PunchAttack";
        public const string KickAttackTag = "KickAttack";
        public const string HurtAttackTag = "HurtAttack";


    //Combos
        public const string PunchTrigger = "punch";
        public const string Punch2Trigger = "punch2";
        public const string KickTrigger = "kick";


    }
}
