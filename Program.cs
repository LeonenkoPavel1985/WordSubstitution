/*Прочитать текст C#-программы (выбрать самостоятельно) и все слова public в объявлении полей классов заменить на слово private. 
В исходном коде в каждом слове длиннее двух символов все строчные символы заменить прописными. Также в коде программы удалить 
все «лишние» пробелы и табуляции, оставив только необходимые для разделения операторов. Записать символы каждой строки программы 
в другой файл в обратном порядке.*/

using System;
using System.IO;
using System.Linq;

namespace WordSubstitution
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileBody = string.Empty;
            using (StreamReader sr = new StreamReader("test.cs"))
            {
                fileBody = sr.ReadToEnd();
            }
            var words = fileBody.Split(" ");
            words = words.Where(w => w.Trim().Length > 0).ToArray();
            int i = 0;
            foreach(var word in words)
            {
                if(word == "public")
                {
                    int j = i;
                    bool isChange = true;
                    while(words.Length > j)
                    {
                        if(words[j] == "class" || words[j] == "(")
                        {
                            isChange = false;
                            break;
                        }
                        j++;
                    }
                    if(isChange)
                    {
                        words[i] = "private";
                    }
                }
                if(word.Length > 2)
                {
                    words[i] = word.ToUpper();
                }
                int prev = -1;
                string newWord = string.Empty;
                foreach(var c in word.ToCharArray())
                {
                    if(c == 9 || c == 32)
                    {
                        if(prev == c)
                        {
                            continue;
                        }
                        prev = c;
                    }
                    newWord += c.ToString();
                }
                words[i] = newWord;
                i++;
            }
            fileBody = string.Join(' ', words);
            var charArray = fileBody.ToCharArray();
            int count = charArray.Length-1;
            string reverse = string.Empty;
            while (count >= 0)
            {
                reverse += charArray[count].ToString();
                count--;
            }
            using (StreamWriter sw = new StreamWriter("testNew.cs", false, System.Text.Encoding.Default))
            {
                sw.Write(fileBody);
            }
            using (StreamWriter sw = new StreamWriter("reverseTest.cs", false, System.Text.Encoding.Default))
            {
                sw.Write(reverse);
            }
        }
    }
}
