using System;
using System.Collections;
using Step1Mocks;

namespace MyBillingProduct
{
	public class LoginManagerWithStatics
	{
        //Working effectively with legacy code
	    private Hashtable m_users = new Hashtable();

	    public bool IsLoginOK(string user, string password)
	    {
	        try
	        {
	            string text = "loginrequested";
	            StaticLogger.Write(text); // <--callLogger()
	        }
	        catch (LoggerException e)
	        {
	            string text = e.Message + Environment.MachineName;
	            StaticWebService.Write(text);
	        }
	        if (m_users[user] != null &&
	            (string) m_users[user] == password)
	        {
	            return true;
	        }
	        return false;
	    }



	    public void AddUser(string user, string password)
	    {
	        m_users[user] = password;
	    }

	    public void ChangePass(string user, string oldPass, string newPassword)
		{
			m_users[user]= newPassword;
		}
	}
}
