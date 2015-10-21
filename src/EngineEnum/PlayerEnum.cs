/*
 *****************************************************************
 *                     StreetEngine Project                      *
 *                                                               *
 * Author: http://github.com/greatmaes                           *
 * Project: http://github.com/greatmaes/StreetEngine-Emulator/   *
 * Chat: http://gitter.im/greatmaes/StreetEngine-Emulator/~chat# *
 *                                                               *
 * About the project:                                            *
 * StreetEngine is a non-profit server side emulator for the ga- *
 * -me StreetGears. This is mostly a project to learn how to ma- *
 * -ke your own emulator as I don't have the knowledge to fully  *
 * finish this project. Feel free to contribute. More informat-  *  
 * ations avaible on the github project page.                    *
 *                                                               *
 * Notes:                                                        *
 * All comments '//' and '///' are optional and can be removed.  *   
 * You must move every files you downloaded to your game folder  *
 * to successfully start StreetEngine.                           *
 *                                                               *
 * Contributors (in alphabetical order):                         *
 * - geekogame                                                   *
 *                                                               *
 * Credits:                                                      *
 * https://github.com/itsexe                                     *
 * https://github.com/skeezr/                                    *
 * http://www.elitepvpers.com/forum/members/4193997-k1ramox.html *
 *                                                               *
 ***************************************************************** 
*/
namespace StreetEngine.EngineEnum
{
    using System;

    public class PlayerEnum
    {
        public enum CharactersType
        {
            CHARACTER_LUNA = 0,
            CHARACTER_TIPPY = 1,
            CHARACTER_RUSH = 2,
            CHARACTER_ROOKIE = 3,
            CHARACTER_KARA = 4,
            CHARACTER_KLAUS = 5,
        }

        public enum TrickCode
        {
            GRIND = 1000,
            BACK_FLIP = 1100,
            FRONT_FLIP = 1200,
            AIR_TWIST = 1300,
            POWER_SWING = 1400,
            GRIP_TURN = 1500,
            DASH = 1600,
            BACK_SKATING = 1700,
            JUMPING_STEER = 1800,
            BUTTING = 1900,
            POWER_SLIDE = 2000,
            POWER_JUMP = 2200,
            WALL_RIDE = 5000,
        }

        public class RankInfo
        {
            public static string
            rank_a = "Admin",
            rank_b = "GameMaster",
            rank_c = "Player",
            rank_d = "Banned",
            rank_e = "Bot";
        }
    }
}
