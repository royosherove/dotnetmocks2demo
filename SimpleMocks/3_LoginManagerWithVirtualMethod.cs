using System;
using System.Collections;

namespace MyBillingProduct
{
	public class LoginManagerWithVirtualMethod
	{
	    private readonly ILogger _logger;
	    private Hashtable m_users = new Hashtable();

	    public LoginManagerWithVirtualMethod()
	    {
	    }

	    public bool IsLoginOK(string user, string password)
	    {
	        WriteToLog("yo");
	        
          if (m_users[user] != null &&
	            m_users[user] == password)
	        {
	            return true;
	        }
	        return false;
	    }


	    protected virtual void WriteToLog(string text)
	    {
	        new RealLogger().Write(text);
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
