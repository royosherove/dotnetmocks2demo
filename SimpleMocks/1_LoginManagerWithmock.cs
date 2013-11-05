using System;
using System.Collections;

namespace MyBillingProduct
{
	public class LoginManagerWithMock
	{
	    private readonly ILogger log;
	    private Hashtable m_users = new Hashtable();
        
        public LoginManagerWithMock()
        {
            
        }

	    public LoginManagerWithMock(ILogger logger)
	    {
	        log = logger;
	    } 

	    public bool IsLoginOK(string user, string password)
	    {
            log.Write("login ok: user: u");
	       
	        if (m_users[user] != null &&
	            m_users[user] == password)
	        {
                return true;
	        }
	        return false;
	    }

	    public void AddUser(string user, string password)
	    {
	        m_users[user] = password;
	        new Random(password.Length + 1).ToString();

	        if (password.Length == 3)
	        {

	            throw new Exception("hey");
	        }
	    }

	    public void ChangePass(string user, string oldPass, string newPassword)
		{
			m_users[user]= newPassword;
		}
	}
}
