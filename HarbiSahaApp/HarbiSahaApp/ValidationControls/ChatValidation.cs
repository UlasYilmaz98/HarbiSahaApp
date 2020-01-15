using System;
using System.Collections.Generic;
using System.Text;

namespace HarbiSahaApp.ValidationControls
{
    public class ChatValidation
    {
       
        public bool Check(string text)
        {

            if (String.IsNullOrWhiteSpace(text))
                return false;
            else if (text.Length > 1000)
                return false;
            return true;
                

        }


        


    }
}
