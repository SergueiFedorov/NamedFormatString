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

        private static bool isEscapeCharacter(string paramString, int currentPosition, char character)
        {
            if (currentPosition + 1 < paramString.Length && paramString[currentPosition + 1] == character)
            {
                return true;
            }
            return false;
        }

        /*
        private static bool isLeftEscapeCharacter(string paramString, int currentPosition)
        {
            if (currentPosition + 1 < paramString.Length && paramString[currentPosition + 1] == '{')
            {
                return true;
            }
            return false;
        }

        private static bool isRightEscapeCharacter(string paramString, int currentPosition)
        {
            if (currentPosition + 1 < paramString.Length && paramString[currentPosition + 1] == '}')
            {
                return true;
            }
            return false;
        }*/

        private static string ExtractParamWord(string paramString, int currentPosition, out int paramSize)
        {
            StringBuilder sBuilder = new StringBuilder();
            int counter = currentPosition;

            while (counter < paramString.Length)
            {
                if (paramString[counter] == '}')
                {
                    if (isEscapeCharacter(paramString, counter, '}') == false)
                    {
                        break;
                    }
                }

                sBuilder.Append(paramString[counter]);

                counter++;
            }

            if (counter == paramString.Length)
            {
                throw new FormatException();
            }

            paramSize = counter - currentPosition;

            return sBuilder.ToString();
        }

        private static string InsertArguments(string paramString, Dictionary<string, string> arguments)
        {
            StringBuilder sBuilder = new StringBuilder();

            int position = 0;
            char character = '\x0';
            int length = paramString.Length;

            while (position < paramString.Length)
            {
                character = paramString[position];

                if (character == '}')
                {
                    if (isEscapeCharacter(paramString, position, '}'))
                    {
                        sBuilder.Append(character);
                        position++;
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                else if (character == '{')
                {
                    if (isEscapeCharacter(paramString, position, '{'))
                    {
                        sBuilder.Append(character);
                        position++;
                    }
                    else
                    {
                        position++;
                        int paramterSize = 0;
                        string paramWord = ExtractParamWord(paramString, position, out paramterSize);

                        if (arguments.ContainsKey(paramWord))
                        {
                            sBuilder.Append(arguments[paramWord]);
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        position += paramterSize;
                    }
                }
                else
                {
                    sBuilder.Append(character);
                }

                position++;
            }

            return sBuilder.ToString();
        }

        public static String Format(string argString, Dictionary<string, string> words)
        {
            return InsertArguments(argString, words);
        }
    }
}
