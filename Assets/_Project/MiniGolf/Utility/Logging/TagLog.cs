using System;
using UnityEngine;

namespace MiniGolf.Utility.Logging
{
    public class TagLog
    {
        private readonly string _tag;
        public TagLog(string tag)
        {
            if (string.IsNullOrEmpty(tag)) 
            {
                _tag = string.Empty;
            }
            else 
            {
                _tag = '[' + tag + "] ";
            }
        }
        
#if MINI_GOLF_PROD
        [System.Diagnostics.Conditional("DUMMY_UNUSED_DEFINE")]
#endif
        [HideInCallstack]
        public void D(string msg)
        {
            Debug.unityLogger.Log(LogType.Log, _tag, msg);
        }
        
        [HideInCallstack]
        public void W(string msg)
        {
            Debug.unityLogger.Log(LogType.Warning, _tag, msg);
        }

        [HideInCallstack]
        public void E(string msg)
        {
            Debug.unityLogger.Log(LogType.Error, _tag, msg);
        }
        
        [HideInCallstack]
        public void E(Exception e)
        {
            Debug.unityLogger.Log(LogType.Exception, _tag, e.ToString());
        }
    }
}