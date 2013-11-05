using System;
using System.Collections.Generic;
using System.Text;

namespace Step1Mocks
{
    public class SystemTime
    {
        private static DateTime _CurrentTime = DateTime.MinValue;
        public static void SetTime(DateTime dt)
        {
            _CurrentTime = dt;
        }
        public static void ResetToNormalTime()
        {
            _CurrentTime = DateTime.MinValue;
        }

        public static DateTime Now()
        {
            if (_CurrentTime==DateTime.MinValue)
            {
                return _CurrentTime;
            }

            return DateTime.Now;
        }
    }
}
