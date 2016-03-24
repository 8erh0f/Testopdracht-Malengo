using System;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;

namespace MalengoPalindroom
{
    public class palindroom

    {

        // aanroep:
        //int _lengte = 5;
        //palindroom pal = new palindroom(_lengte);
        //while (!pal.laatste)
        //{
        //    pal.maakVolgendePalindroom();
        //    Console.WriteLine(pal.palindroomString);
        //} 
        public palindroom(int stringLengte)
        {
            // constructor
            _palindroomString = new string('a', stringLengte);
            _actieveRij = 0;
            _stringLengte = stringLengte;
            _eindString = new string('z', stringLengte);
        }

        private string _eindString = string.Empty;
        private int _stringLengte = 0;
        // properties
        private long _aantal = 0;
        public long aantal
        {
            get
            {
                return _aantal;
            }
        }

        private bool _laatste = false;
        public bool laatste
        {
            get
           {
                return _laatste;
            }
            set
           {
                _laatste = value;
            }
        }

        private bool _eerste = true;
        private bool eerste
        {
            get
            {
                return _eerste;
            }
            set
            {
                _eerste = value;
            }
        }

        private string _palindroomString = string.Empty;
        public string palindroomString
        {
            get
            {
                return _palindroomString;
            }
            set
            {
                _palindroomString = value;
            }
        }

        private int _actieveRij = 0;
        private int actieveRij
        {
            get
            {
                return _actieveRij;
            }
            set
            {
                _actieveRij = value;
            }
        }
        //
        private string volgendeAlfabetLetter(string oudeLetter)
        {
            string letter = "a";
            try
            {
                if (oudeLetter != "z")
                {
                    letter = ((char)(oudeLetter[0] + 1)).ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return letter;
        }
        [WebMethod]
        public void maakVolgendePalindroom()
        {
            // teller
            _aantal++;
            //
            if (eerste)
            {
                // de eerste keer niets doen
                // begin string wordt gebruikt
                eerste = false;
            }
            else
            {
                string volgendeLetter = volgendeAlfabetLetter(palindroomString.Substring(actieveRij, 1));
                // bepaal de volgende letter
                draaiLetter(volgendeLetter);
                // kijk of het de laatste is
                if (palindroomString == _eindString)
                {
                   laatste = true;
                }
                else
                {
                    // a betekent dat ie een rondje heeft gedaan
                    // nu naar de volgende rij
                    while (volgendeLetter == "a")
                    {
                        actieveRij++;
                        volgendeLetter = volgendeAlfabetLetter(palindroomString.Substring(actieveRij, 1));
                        draaiLetter(volgendeLetter);
                    }
                    actieveRij = 0;
                }
            }
        }

        [ScriptMethod, WebMethod]
        public void maakVolgendePalindroom(int stringLengte)
        {
            _palindroomString = new string('a', stringLengte);
            _actieveRij = 0;
            _stringLengte = stringLengte;
            _eindString = new string('z', stringLengte);
            // teller
            _aantal++;
            //
            if (eerste)
            {
                // de eerste keer niets doen
                // begin string wordt gebruikt
                eerste = false;
            }
            else
            {
                string volgendeLetter = volgendeAlfabetLetter(palindroomString.Substring(actieveRij, 1));
                // bepaal de volgende letter
                draaiLetter(volgendeLetter);
                // kijk of het de laatste is
                if (palindroomString == _eindString)
                {
                    laatste = true;
                }
                else
                {
                    // a betekent dat ie een rondje heeft gedaan
                    // nu naar de volgende rij
                    while (volgendeLetter == "a")
                    {
                        actieveRij++;
                        volgendeLetter = volgendeAlfabetLetter(palindroomString.Substring(actieveRij, 1));
                        draaiLetter(volgendeLetter);
                    }
                    actieveRij = 0;
                }
            }
        }

        private void draaiLetter(string volgende)
        {
            StringBuilder sb = new StringBuilder(palindroomString);
            try
            {
                //eerste letter en achterste letter vervangen
                sb.Remove(actieveRij, 1).Insert(actieveRij, volgende).Remove(_stringLengte - 1 - actieveRij, 1).Insert(_stringLengte - 1 - actieveRij, volgende);
                palindroomString = sb.ToString();
            }
            catch (Exception)
            {
                palindroomString = "FOUT"; // TODO
                throw;
            }
            finally
            {
                sb = null;
            }
        }
    }

}
