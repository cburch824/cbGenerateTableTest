using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace cbLibrary
{
    class TextfileLineBuilder
    {
        List<string> lineBuilderString = new List<string>(0);
        List<string> inputs = new List<string>(0);
        char infileDelimiter = ' ';
        StreamReader inputFile;

        public char InfileDelimiter { get => infileDelimiter; set => infileDelimiter = value; }

        public TextfileLineBuilder(string instring, StreamReader inFile, StreamWriter outFile)
        {
            try
            {
                //inputs = inputsList;
                inputFile = inFile;

                List<List<string>> infileLists = new List<List<string>>(0);
                infileLists = populateInfileLists();

                foreach(List<string> internalList in infileLists)
                {
                    outFile.WriteLine(createNewLine(convertToLineBuilderString(instring), internalList));
                }




            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public static List<string> convertToLineBuilderString(string inputString) //working
        {
            List<string> outputLineBuilderStringList = new List<string>(0);
            string delimiter = "input";
            int occurenceIndex;
            string nullstring = null;

            while (inputString.Contains(delimiter))
            {
                occurenceIndex = inputString.IndexOf(delimiter);
                outputLineBuilderStringList.Add(inputString.Substring(0, occurenceIndex)); //adds the beginning of the string (until reaching the delimiter) as a string object to the outputLineBuilder object

                //remove the delimiter from the string
                inputString = inputString.Remove(0, occurenceIndex + delimiter.Length);

                //add a null string object to the end of the outputLineBuilderStringList object
                outputLineBuilderStringList.Add(nullstring);

            }

            //add the remaining string (after all of the inputs) as a string object to the end of the outputLineBuilderString object
            outputLineBuilderStringList.Add(inputString);


            return outputLineBuilderStringList;
        }

        private List<List<string>> populateInfileLists()
        {
            List<List<string>> inFileLists = new List<List<string>>(0);

            List<string> currentStringList = new List<string>(0);
            string currentLine;
            int delimIndex;
            

            while (!inputFile.EndOfStream)
            {
                currentLine = inputFile.ReadLine();
                currentLine = currentLine.Trim(InfileDelimiter); //trim the beginning and the end of the delimiter
                
                //search for the delimiter
                while(currentLine.Contains(infileDelimiter.ToString()))
                {
                    delimIndex = currentLine.IndexOf(infileDelimiter);

                    currentStringList.Add(currentLine.Substring(0, delimIndex));

                    currentLine = currentLine.Remove(0, delimIndex + 1);
                }
                currentStringList.Add(currentLine);

                inFileLists.Add(new List<string>(currentStringList));
                currentStringList.Clear(); //Nevermind^ that fixed it. ---this is not working because List<string> is an object reference rather than an object itself

            }

            return inFileLists;

        }


        private string createNewLine(List<string> inputLineBuilderList, List<string> infileList)
        {
            string outputLine = "";
            int currentInputIndex = 0;

            foreach (string s in inputLineBuilderList)
            {

                if(s == null) //if the current string is null, then add the next inputString 
                {
                    outputLine += infileList[currentInputIndex];
                    currentInputIndex++;
                }
                else
                {
                    outputLine += s;
                }
            }


            return outputLine;
        }



    }
}
