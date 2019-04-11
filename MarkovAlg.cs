using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApp46
{
    //класс для арлгорифма Маркова
    public class MarkovAlg
    {
        //подстановка может быть завершающей и обычной
        public enum typeSubstitution { standart, final}
        //количество подстоновок
        public int count { get; }
        //левая часть подстановки
        public string[] left { get; }
        //правая часть подстановки
        public string[] right { get; }
        //тип подстановки
        public typeSubstitution[] tsubs { get; }
        
        //конструктор 
        public MarkovAlg()
        {
            Console.WriteLine("Введите количество подстановок в схеме нормального алгорифма");
            count = int.Parse(Console.ReadLine());
            left = new string[count];
            right = new string[count];
            tsubs = new typeSubstitution[count];
            Console.WriteLine("Введите схему нормального алгорифма");
            for (var i = 0; i<count; i++)
            {
                string s = Console.ReadLine();
                var temp = s.Split();
                left[i] = temp[0];
                if (temp[1].Length == 2) tsubs[i] = typeSubstitution.standart;
                else tsubs[i] = typeSubstitution.final;
                right[i] = temp[2];
            }
        }

        //проверяет применима ли данная схема к данному слову и возвращает первую применимую подстановку
        public int isApplicable(string s)
        {
            for (var i = 0; i<count; i++)
            {
                if (s.Contains(left[i])) return i;
                if (left[i] == "eps") return i;
            }
            return -1;
        }
        //применяет первую применимую подстановку к данной строке
        public void oneSubstitution(int i, ref string s)
        {
            if (left[i] == "eps") s = string.Concat(right[i], s);
            var regex = new Regex(Regex.Escape(left[i]));
            if (right[i] != "eps") s = regex.Replace(s, right[i], 1);
            else s = regex.Replace(s, "", 1);
        }

        //применяет схему к заданному слову
        public void doAlg(string s)
        {
            Console.WriteLine(string.Concat(s, " => "));
            while (true)
            {
                var i = isApplicable(s);
                if (i == -1) break;
                else oneSubstitution(i, ref s);
                Console.WriteLine(string.Concat(s, " => "));
                if (tsubs[i] == typeSubstitution.final) break;
            }
            if (s == "") s = "eps";
            Console.WriteLine(string.Concat("Алгорифм завершился, результат: ", s));
        }   
         
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Инструкция в файле README");
            MarkovAlg test = new MarkovAlg();
            Console.WriteLine("Введите строку, к которой применится схема нормального алгорифма");
            string word = Console.ReadLine();
            Console.WriteLine("Применяем..");
            test.doAlg(word);
        }
    }
}

/* Логи
-----------------1--------------------
Введите количество подстановок в схеме нормального алгорифма
2
Введите схему нормального алгорифма
aa -> a
bb -> b
Введите строку, к которой применится схема нормального алгорифма
aaaaaabbbaab
Применяем..
aaaaaabbbaab =>
aaaaabbbaab =>
aaaabbbaab =>
aaabbbaab =>
aabbbaab =>
abbbaab =>
abbbab =>
abbab =>
abab =>
Алгорифм завершился, результат: abab

-----------------2--------------------
Введите количество подстановок в схеме нормального алгорифма
3
Введите схему нормального алгорифма
a -> eps
b -> c
eps ->. R
Введите строку, к которой применится схема нормального алгорифма
ababababab
Применяем..
ababababab =>
babababab =>
bbababab =>
bbbabab =>
bbbbab =>
bbbbb =>
cbbbb =>
ccbbb =>
cccbb =>
ccccb =>
ccccc =>
Rccccc =>
Алгорифм завершился, результат: Rccccc
-----------------3--------------------
Введите количество подстановок в схеме нормального алгорифма
3
Введите схему нормального алгорифма
ab -> a
b ->. eps
a -> b
Введите строку, к которой применится схема нормального алгорифма
bababab
Применяем..
bababab =>
baabab =>
baaab =>
baaa =>
aaa =>
Алгорифм завершился, результат: aaa

-----------------4--------------------
Введите количество подстановок в схеме нормального алгорифма
3
Введите схему нормального алгорифма
ab -> a
b -> eps
a -> b
Введите строку, к которой применится схема нормального алгорифма
bbaaab
Применяем..
bbaaab =>
bbaaa =>
baaa =>
aaa =>
baa =>
aa =>
ba =>
a =>
b =>
 =>
Алгорифм завершился, результат: eps

-----------------5--------------------
Введите количество подстановок в схеме нормального алгорифма
4
Введите схему нормального алгорифма
*11 -> 1*
*1 ->. eps
* ->. eps
eps -> *
Введите строку, к которой применится схема нормального алгорифма
11111
Применяем..
11111 =>
*11111 =>
1*111 =>
11*1 =>
11 =>
Алгорифм завершился, результат: 11
 */
