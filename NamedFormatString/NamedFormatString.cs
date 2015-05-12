using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF
{

    public static class NamedFormatString
    {
        private struct StartEndPair
        {
            public int start;
            public int end;
        }

        private static string StripParamterAtLocation(string parameterString, StartEndPair location)
        {
            StringBuilder aggregate = new StringBuilder();
            int counter = location.start;
            while (counter != location.end)
            {
                aggregate.Append(parameterString[counter]);
                counter++;

                if (counter == parameterString.Length)
                {
                    throw new FormatException();
                }
            }

            return aggregate.ToString();
        }

        /// <summary>
        /// Finds the inner most bracket set
        /// </summary>
        /// <param name="paramString"></param>
        /// <param name="currentLocation"></param>
        /// <returns></returns>
        private static StartEndPair FindNextBracketSet(string paramString, int currentLocation)
        {
            StartEndPair pair;

            int innerMostOpeningBracket = -1;
            int counter = currentLocation;
            while (counter < paramString.Length)
            {
                if (paramString[counter] == '{')
                {
                    innerMostOpeningBracket = counter;
                }

                if (innerMostOpeningBracket != -1 && paramString[counter] == '}')
                {
                    //Found the closing brace. Pop the top of the stack
                    //for the inner most bracket
                    pair.start = innerMostOpeningBracket;
                    pair.end = counter;

                    return pair;
                }

                counter++;
            }

            //Function did not return. It means it ran off the string
            //before finding a closing bracket
            if (innerMostOpeningBracket != -1)
            {
                throw new FormatException();
            }

            pair.start = -1;
            pair.end = -1;

            return pair;
        }

        private static Dictionary<string, int> StripOutParams(string paramString, out string resultParameterString)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            StringBuilder bracketStringBuilder = new StringBuilder();
            StringBuilder reconstructedStringAggregate = new StringBuilder();

            int parameterIndex = 0;
            int letterCounter = 0;

            StartEndPair bracketLocationPair;

            while ((bracketLocationPair = FindNextBracketSet(paramString, letterCounter)).end != -1)
            {
                //Fill in the string up to the appropriate location
                while (letterCounter < bracketLocationPair.start)
                {
                    reconstructedStringAggregate.Append(paramString[letterCounter]);
                    letterCounter++;
                }

                bracketStringBuilder.Clear();

                string resultString = StripParamterAtLocation(paramString, bracketLocationPair);

                bracketStringBuilder.Append("{");
                bracketStringBuilder.Append(parameterIndex);
                bracketStringBuilder.Append("}");

                result.Add(resultString, parameterIndex);

                parameterIndex++;

                letterCounter = bracketLocationPair.end + 1;

                reconstructedStringAggregate.Append(bracketStringBuilder.ToString());
            }
                
            //Finish off anything that is left at the end of the string
            while (letterCounter < paramString.Length)
            {
                reconstructedStringAggregate.Append(paramString[letterCounter]);
                letterCounter++;
            }

            resultParameterString = reconstructedStringAggregate.ToString();

            return result;
        }

        public static String Format(string argString, params string[] words)
        {
            string resultParamterString = null;
            Dictionary<string, int> parameters = StripOutParams(argString, out resultParamterString);

            return string.Format(resultParamterString, words);
        }

        public static String Format(string argString, Dictionary<string, string> words)
        {
            string resultParamterString = null;
            Dictionary<string, int> parameters = StripOutParams(argString, out resultParamterString);

            string[] paramArray = new string[words.Keys.Count];

            foreach (string paramName in words.Keys)
            {
                paramArray[parameters[paramName]] = words[paramName];
            }

            return string.Format(resultParamterString, paramArray);
        }
    }
}
