using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmes.UTIL.Helpers
{
    public class HideEmailHelper
    {
        public static string HideEmail(string email)
        {
            var emailsplit = email.Split('@');
            var newsplit = emailsplit[1].Split('.');
            char[] array1 = emailsplit[0].ToCharArray();
            char[] array2 = newsplit[0].ToCharArray();
            var output = "";

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1.Length > 4)
                {
                    if (i == 0 || i == array1.Length - 1 || i == array1.Length - 2)
                    {
                        output += array1[i];
                    }
                    else
                    {
                        output += "*";
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        output += array1[i];
                    }
                    else
                    {
                        output += "*";
                    }
                }
            }

            output += "@";
            for (int i = 0; i < array2.Length; i++)
            {
                //if (i % 2 == 0)
                //{
                //    output += array2[i];
                //}
                if (i < array2.Length / 3 || i > (array2.Length / 3) * 2)
                {
                    output += array2[i];
                }
                else
                {
                    output += "*";
                }
            }
            for (int i = 1; i < newsplit.Length; i++) output += "." + newsplit[i];

            return output;
        }
    }
}
