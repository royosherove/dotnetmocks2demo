using System;
using System.Collections;

namespace MyBillingProduct
{
	public class LoginManagerWithMockAndStub
	{
	    private readonly IWebService service;
	    private readonly ILogger log;
	    private Hashtable m_users = new Hashtable();


	    public LoginManagerWithMockAndStub(ILogger logger,IWebService service)
	    {
	        this.service = service;
	        log = logger;
	    }

	    public bool IsLoginOK(string user, string password)
	    {
	        try
	        {
	            log.Write("yo");
	        }
	        catch (LoggerException e)
	        {
              service.Write("got exception");
	            Console.WriteLine(e);
	        }
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
	    }

	    public void ChangePass(string user, string oldPass, string newPassword)
		{
			m_users[user]= newPassword;
		}
	}
}
