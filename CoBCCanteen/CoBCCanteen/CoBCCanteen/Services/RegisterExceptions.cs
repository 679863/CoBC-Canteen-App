using System;
namespace CoBCCanteen.Services
{
	[Serializable]
	public class ExistingID : Exception
    {
        public ExistingID()
        {

        }
     
        public ExistingID(string id) : base($"A user with the ID: \"{ id }\" already exists!")
        {

        }
    }

    [Serializable]
    public class ExistingEmail : Exception
    {
        public ExistingEmail()
        {

        }

        public ExistingEmail(string email) : base($"A user with the email: \"{ email }\" already exists!")
        {

        }
    }
}

