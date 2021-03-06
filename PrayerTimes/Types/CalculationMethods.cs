﻿namespace PrayerTimes.Types
{
    public enum CalculationMethods
    {

        /**
         * Moonsighting Committee
         * Uses a Fajr angle of 18 and an Isha angle of 18. Also uses seasonal adjustment values.
         */
        MOON_SIGHTING_COMMITTEE,
        /**
         * Muslim World League
         * Uses Fajr angle of 18 and an Isha angle of 17
         */
        MUSLIM_WORLD_LEAGUE,

        /**
         * Egyptian General Authority of Survey
         * Uses Fajr angle of 19.5 and an Isha angle of 17.5
         */
        EGYPTIAN,

        /**
         * University of Islamic Sciences, Karachi
         * Uses Fajr angle of 18 and an Isha angle of 18
         */
        KARACHI,

        /**
         * Umm al-Qura University, Makkah
         * Uses a Fajr angle of 18.5 and an Isha angle of 90. Note: You should add a +30 minute custom
         * adjustment of Isha during Ramadan.
         */
        UMM_AL_QURA,

        /**
         * The Gulf Region
         * Modified version of Umm al-Qura that uses a Fajr angle of 19.5.
         */
        GULF,
        /**
         * Referred to as the ISNA method
         * This method is included for completeness, but is not recommended.
         * Uses a Fajr angle of 15 and an Isha angle of 15.
         */
        NORTH_AMERICA,

        /**
         * Kuwait
         * Uses a Fajr angle of 18 and an Isha angle of 17.5
         */
        KUWAIT,

        /**
         * Qatar
         * Modified version of Umm al-Qura that uses a Fajr angle of 18.
         */
        QATAR,
        SARAJEVO,
        TEHERAN,
        JAFARI,

        /**
         * The default value for {@link CalculationParameters#method} when initializing a
         * {@link CalculationParameters} object. Sets a Fajr angle of 0 and an Isha angle of 0.
         */
        OTHER

    }
}
